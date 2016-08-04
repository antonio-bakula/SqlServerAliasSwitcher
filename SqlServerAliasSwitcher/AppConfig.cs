using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlServerAliasSwitcher
{
  public class AliasDefinition
  {
    public string Name { get; set; }
    public string Alias { get; set; }
  }

  public class AliasConfiguration
  {
    public string Name { get; set; }
    public List<string> WindowsServices { get; set; }
    public List<AliasDefinition> Aliases { get; set; }
  }

  public class Configuration
  {
    public List<AliasConfiguration> AliasConfigurations { get; set; }
  }

  public static class ConfigurationEngine
  {
    private static Configuration _configuration = null;

    public static Configuration Configuration
    {
      get
      {
        if (_configuration == null)
        {
          var exe = new FileInfo(Application.ExecutablePath);
          string configFile = Path.Combine(exe.DirectoryName, "config.json");
          string json = File.ReadAllText(configFile);
          _configuration = JsonConvert.DeserializeObject<Configuration>(json);          
        }
        return _configuration;;
      }
    }
  }
}
