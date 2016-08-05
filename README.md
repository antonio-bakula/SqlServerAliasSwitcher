# SqlServerAliasSwitcher
Windows application that set MS SQL Server alias(es) with predefined groups.

Little application that solves problem connecting to SQL server for development. I got tired of constantly changing config
file that holds connection string (web.config in my case) and that file was under source control so I have to be carefull not
commit config file with temporary changes. Reason for this to happenig is a fact that I work on my laptop and dock it at home and at office and on both places I have SQL server on separate machines.

Configuration is done trough config.json file:

{
  "AliasConfigurations": [
    {
      "Name": "Home",
      "WindowsServices": [ ],
      "Aliases": [
        { "Name": "TESTER", "Alias": "BAKULA-SERVER\\SQLExpress" },
        { "Name": "TESTER\\SQLExpress", "Alias": "BAKULA-SERVER\\SQLExpress" }
      ]
    },
    {
      "Name": "Work",
      "WindowsServices": [ ],
      "Aliases": [
        { "Name": "TESTER", "Alias": "tester.wem.local" },
        { "Name": "TESTER\\SQLExpress", "Alias": "tester.wem.local\\SQLExpress" }
      ]
    },
    {
      "Name": "Local",
      "WindowsServices": ["MSSQL$SQLEXPRESS", "SQLBrowser"],
      "Aliases": [
        { "Name": "TESTER", "Alias": "ABAKULA-LAPTOP\\SQLEXPRESS" },
        { "Name": "TESTER\\SQLExpress", "Alias": "ABAKULA-LAPTOP\\SQLEXPRESS" }
      ]
    }
  ]
}

In this I have 3 separate configs that changes Aliases: TESTER, TESTER\SQLExpress. Windows services is a list with name of windows services that should be started when this configuration is set as active, this is for local SQL server on my laptop.

[not finished]
