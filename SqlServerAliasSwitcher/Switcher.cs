using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlServerAliasSwitcher
{
  public class AliasSwitcher
  {
    private NotifyIcon notifyIcon = null;
    private ContextMenu menu = null;

    public void Start()
    {
      this.menu = new ContextMenu();
      string activeAliasConfig = GetActiveAliasConfiguration();
      // switch itens
      foreach (var conf in ConfigurationEngine.Configuration.AliasConfigurations)
      {
        var mi = new MenuItem();
        mi.Text = conf.Name;
        mi.Name = "conf_" + conf.Name;
        mi.Click += AliasConfigMenuItemClick;
        mi.Checked = conf.Name == activeAliasConfig;
        this.menu.MenuItems.Add(mi);
      }

      if (!string.IsNullOrEmpty(ConfigurationEngine.Configuration.ConnectionString) && ConfigurationEngine.Configuration.DataCommands.Any())
      {
        this.menu.MenuItems.Add("-");

        foreach (var dataCmd in ConfigurationEngine.Configuration.DataCommands)
        {
          var menuItemCmd = new MenuItem();
          menuItemCmd.Text = dataCmd.Name;
          menuItemCmd.Tag = dataCmd;
          menuItemCmd.Click += menuItemCmd_Click;
          this.menu.MenuItems.Add(menuItemCmd);
        }

      }

      this.menu.MenuItems.Add("-");
      // exit
      var menuItemExit = new MenuItem();
      menuItemExit.Text = "&Exit";
      menuItemExit.Click += MenuItemExit_Click;
      this.menu.MenuItems.Add(menuItemExit);

      this.notifyIcon = new NotifyIcon();
      this.notifyIcon.Icon = Properties.Resources.TrayIcon;
      this.notifyIcon.Click += notifyIcon_Click;
      this.notifyIcon.ContextMenu = this.menu;
      this.notifyIcon.Visible = true;
    }

    void menuItemCmd_Click(object sender, EventArgs e)
    {
      var menuItem = (MenuItem)sender;
      var cmd = (SwitcherCommand)menuItem.Tag;
      if (cmd != null)
      {
        var fsd = new FormShowData();
        fsd.SetCommand(cmd);
        fsd.ShowDialog();
      }
    }

    private string GetActiveAliasConfiguration()
    {
      var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\MSSQLServer\Client\ConnectTo");
      if (key == null)
      {
        return "";
      }

      string active = "";
      try
      {
        var aliasValueNames = key.GetValueNames();
        foreach (string name in aliasValueNames)
        {
          var objValue = key.GetValue(name, "", RegistryValueOptions.None);
          if (objValue != null)
          {
            string value = objValue.ToString().Trim().ToLower();
            foreach (var conf in ConfigurationEngine.Configuration.AliasConfigurations)
            {
              var alias = conf.Aliases.FirstOrDefault(al => al.Name.ToLower() == name.ToLower() && value.EndsWith(al.Alias.ToLower()));
              if (alias != null)
              {
                if (!string.IsNullOrEmpty(active) && active != conf.Name)
                {
                  // u dosadašnjim keyevima je bila druga confa
                  active = "";
                }
                else
                {
                  active = conf.Name;
                }
                break;
              }
            }

            if (string.IsNullOrEmpty(active))
              break;
          }
        }
      }
      finally
      {
        key.Close();
      }
      return active;
    }

    private void SetActiveAliasConfiguration(string activate)
    {
      if (string.IsNullOrEmpty(activate))
        return;

      var activateConf = ConfigurationEngine.Configuration.AliasConfigurations.FirstOrDefault(ac => ac.Name == activate);
      if (activateConf != null)
      {
        string current = GetActiveAliasConfiguration();
        var currentConf = ConfigurationEngine.Configuration.AliasConfigurations.FirstOrDefault(ac => ac.Name == current);
        if (currentConf != null)
        {
          StopService(currentConf.WindowsServices);
        }

        StartService(activateConf.WindowsServices);
        WriteConfigurationToRegistry(activateConf);

        string activeAfterChange = GetActiveAliasConfiguration();
        foreach (var mitem in this.menu.MenuItems.Cast<MenuItem>().Where(mi => mi.Name.StartsWith("conf_")))
        {
          mitem.Checked = mitem.Text == activeAfterChange;
        }
        if (activate == activeAfterChange)
        {
          this.notifyIcon.ShowBalloonTip(1500, "SQL Server Alias Switcher", "Configuration successfully changed to: " + activate, ToolTipIcon.Info);
        }
        else
        {
          this.notifyIcon.ShowBalloonTip(1500, "SQL Server Alias Switcher", "Error ! Couldn't change configuration to: " + activate, ToolTipIcon.Error);
        }
      }
    }

    private void WriteConfigurationToRegistry(AliasConfiguration conf)
    {
      var keys = GetRegistry32And64BitSubKeys(RegistryHive.LocalMachine, @"SOFTWARE\Microsoft\MSSQLServer\Client\ConnectTo", true);
      foreach (var key in keys)
      {
        try
        {
          var aliasValueNames = key.GetValueNames().ToList();
          foreach (var alias in conf.Aliases)
          {
            key.SetValue(alias.Name, "DBMSSOCN," + alias.Alias, RegistryValueKind.String);
          }
        }
        finally
        {
          key.Close();
        }
      }
    }
    
    private List<RegistryKey> GetRegistry32And64BitSubKeys(RegistryHive hive, string path, bool writeable)
    {
      var key32Bit = GetRegistrySubKey(hive, path, writeable, false);
      var key64Bit = GetRegistrySubKey(hive, path, writeable, true);
      var result = new List<RegistryKey>();
      if (key32Bit != null)
      {
        result.Add(key32Bit);
      }
      if (key64Bit != null)
      {
        result.Add(key64Bit);
      }
      return result;
    }

    private RegistryKey GetRegistrySubKey(RegistryHive hive, string path, bool writeable, bool regKey64Bit)
    {
      var key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, regKey64Bit ? RegistryView.Registry64 : RegistryView.Registry32);
      var subKey = key.OpenSubKey(path, writeable);
      if (subKey == null)
      {
        subKey = key.CreateSubKey(path);
      }
      return subKey;
    }

    private void StartService(List<string> services)
    {
      string errorsStartingServices = "";
      foreach (string serviceName in services)
      {
        var controller = new ServiceController(serviceName);
        if (controller.StartType == ServiceStartMode.Disabled)
        {
          if (!string.IsNullOrEmpty(errorsStartingServices))
          {
            errorsStartingServices += Environment.NewLine;
          }
          errorsStartingServices += string.Format("Cannot start service {0}. Service is disabled.", serviceName);
        }
        else if (controller.Status != ServiceControllerStatus.Running)
        {
          controller.Start();
        }
      }
      if (!string.IsNullOrEmpty(errorsStartingServices))
      {
        MessageBox.Show(errorsStartingServices, "SqlServerAliasSwitcher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    private void StopService(List<string> services)
    {
      foreach (string serviceName in services)
      {
        var controller = new ServiceController(serviceName);
        if (controller.Status != ServiceControllerStatus.Stopped)
          controller.Stop();
      }
    }

    void AliasConfigMenuItemClick(object sender, EventArgs e)
    {
      SetActiveAliasConfiguration(((MenuItem)sender).Text);
    }

    void MenuItemExit_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    public void Stop()
    {
      this.notifyIcon.Visible = false;
      this.notifyIcon = null;
    }

    void notifyIcon_Click(object sender, EventArgs e)
    {
      var mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
      mi.Invoke(this.notifyIcon, null);
    }
  }
}
