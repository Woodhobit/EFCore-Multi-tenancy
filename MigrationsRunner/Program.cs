using DbUp;
using DbUp.Engine;
using DbUp.Helpers;
using DbUp.ScriptProviders;
using System;
using System.IO;

namespace MigrationsRunner
{
    class Program
    {
        private static string connectionString = "Data Source=.;Initial Catalog=DemoDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static string scriptsPath = @"./scripts/";
        private static string migrationScripts = "Migrations";
        private static string preDeploymentScripts = "PreDeployment";
        private static string postDeploymentScripts = "PostDeployment";
        private static string schema = "dbo";
        private static string table = "MigrationsJournal";

        static int Main(string[] args)
        {
            // if parameters are not passed, use the default parameters
            if (args.Length == 2)
            {
                connectionString = args[0];
                scriptsPath = args[1];
            }

            // Check if the target database exist if not it will create the database and then run scripts
            EnsureDatabase.For.SqlDatabase(connectionString);

            // execut predeploy scripts
            var result = RunPreDeployScripts(scriptsPath, connectionString);
            if (!result.Successful)
            {
                return ReturnError(result.Error.ToString());
            }

            ShowSuccess();

            // execut migration scripts
            result = RunMigrations(scriptsPath, connectionString);
            if (!result.Successful)
            {
                return ReturnError(result.Error.ToString());
            }

            ShowSuccess();

            // execut postdeploy scripts
            result = RunPostDeployScripts(scriptsPath, connectionString);
            if (!result.Successful)
            {
                return ReturnError(result.Error.ToString());
            }

            ShowSuccess();

            return 0;
        }

        private static void ShowSuccess()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
        }

        private static int ReturnError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
            return -1;
        }

        private static DatabaseUpgradeResult RunMigrations(string rootScriptsPath, string connectionString)
        {
            Console.WriteLine("Start executing migration scripts...");

            var migrationScriptsPath = Path.Combine(rootScriptsPath, migrationScripts);

            return PerformUpgrade(migrationScriptsPath, connectionString, true);
        }

        private static DatabaseUpgradeResult RunPreDeployScripts(string rootScriptsPath, string connectionString)
        {
            Console.WriteLine("Start executing predeployment scripts...");

            var preDeploymentScriptsPath = Path.Combine(rootScriptsPath, preDeploymentScripts);

            return PerformUpgrade(preDeploymentScriptsPath, connectionString, false);
        }

        private static DatabaseUpgradeResult RunPostDeployScripts(string rootScriptsPath, string connectionString)
        {
            Console.WriteLine("Start executing postdeployment scripts...");

            var postdeploymentScriptsPath = Path.Combine(rootScriptsPath, postDeploymentScripts);

            return PerformUpgrade(postdeploymentScriptsPath, connectionString, false);
        }

        private static DatabaseUpgradeResult PerformUpgrade(string scriptsPath, string connectionString, bool isJournalling)
        {
            var builder = DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsFromFileSystem(scriptsPath, new FileSystemScriptOptions
                {
                    IncludeSubDirectories = true
                })
                .WithTransactionPerScript()
                .LogToConsole();

            if (isJournalling)
            {
                builder = builder.JournalToSqlTable(schema, table);
            }
            else 
            {
                builder = builder.JournalTo(new NullJournal());
            }

            return builder.Build().PerformUpgrade();
        }
    }
}
