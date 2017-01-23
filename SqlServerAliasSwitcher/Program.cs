using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlServerAliasSwitcher
{
  static class Program
  {
    private static AliasSwitcher _switcher = null; 
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      try
      {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        _switcher = new AliasSwitcher();
        Application.ApplicationExit += Application_ApplicationExit;
        _switcher.Start();
        Application.Run();
      }
      catch (Exception e)
      {
        MessageBox.Show("SqlServerAliasSwitcher raised Exception while starting: " + Environment.NewLine + e.Message);
        EventLog.WriteEntry("SqlServerAliasSwitcher", "Exception raised while starting application: " + e.Message, EventLogEntryType.Error);
      }
    }

    static void Application_ApplicationExit(object sender, EventArgs e)
    {
      if (_switcher != null)
      {
        _switcher.Stop();
        _switcher = null;
      }
    }

  }
}
