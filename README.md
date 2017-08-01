# SqlServerAliasSwitcher
Windows application that set MS SQL Server alias(es) with predefined groups.

Little application that solves problem connecting to SQL server for development. I got tired of constantly changing config
file that holds connection string (web.config in my case) and that file was under source control so I have to be carefull not
commit config file with temporary changes. Reason for this changes is a fact that I work on my laptop and dock it at home and at office and on both places I have SQL server on separate machines.

Binaries download at [http://www.antoniob.com/sql-server-alias-switcher.html](http://www.antoniob.com/sql-server-alias-switcher.html)

### Configuration
Configuration is done trough config.json file, example config [SqlServerAliasSwitcher/SqlServerAliasSwitcher/config.json](https://github.com/antonio-bakula/SqlServerAliasSwitcher/blob/master/SqlServerAliasSwitcher/config.json) sets 3 different configurations, each one suitable for different SQL server locations in different places / local network. In example file there are 3 locations: work, home, laptop. Each configuration defines two Aliases: TESTER, TESTER\SQLExpress.

#### Configuration file json nodes description:
*AliasConfigurations* - Alias configurations for one place (office, home, laptop etc.)

##### AliasConfigurations members:
*Name* - Alias configuration Name, will be used for caption of tray menu item.  
*WindowsServices* - List of  windows service names that should be started when this configuration is set as active (e.g. local SQL server on laptop).  
*Aliases* - Array of alias informations

##### Alias array members:
*Name* - SQL Server Alias name  
*Alias* - Address of real SQL Server that will be used for this alias

