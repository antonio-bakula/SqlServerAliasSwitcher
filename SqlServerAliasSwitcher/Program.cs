using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlServerAliasSwitcher
{
  static class Program
  {
    private static Switcher _switcher = null; 
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      _switcher = new Switcher();
      Application.ApplicationExit += Application_ApplicationExit;
      _switcher.Start();
      Application.Run();
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
