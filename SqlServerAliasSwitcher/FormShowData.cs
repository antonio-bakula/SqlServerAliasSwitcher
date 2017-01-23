using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlServerAliasSwitcher
{
  public partial class FormShowData : Form
  {

    public SwitcherCommand Command { get; private set; }

    public FormShowData()
    {
      InitializeComponent();
      this.Command = null;
    }

    public void SetCommand(SwitcherCommand cmd)
    {
      this.Command = cmd;

      var result = new DataTable();
      using (var conn = new SqlConnection(ConfigurationEngine.Configuration.ConnectionString))
      {
        conn.Open();
        var sqlCmd = new SqlCommand(this.Command.Sql, conn);
        var adapter = new SqlDataAdapter(sqlCmd);
        adapter.Fill(result);
        conn.Close();
      }

      if (this.Command.PopupHeight > 120)
      {
        this.Height = this.Command.PopupHeight;
      }

      if (this.Command.PopupWidth > 30)
      {
        this.Width = this.Command.PopupWidth;
      }

      this.Text = this.Command.Name.Replace("&", "");
      this.gridViewCmdData.DataSource = result;
    }
  }
}
