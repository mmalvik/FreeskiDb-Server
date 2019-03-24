using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace FreeskiDb.Persistence.CosmosDb
{
    public static class CosmosEmulator
    {
        /// <summary>
        /// Verifies that the CosmosDB Emulator is installed and is currently running.
        /// </summary>
        public static void Verify()
        {
            VerifyEmulatorInstalled();
            VerifyEmulatorRunning();
        }

        /// <summary>
        /// Verifies that the CosmosDB Emulator is actually installed.
        /// </summary>
        private static void VerifyEmulatorInstalled()
        {
            var noEmulatorMessage = new StringBuilder("This requires CosmosDB Emulator!");
            noEmulatorMessage.Append("Go to https://aka.ms/documentdb-emulator-docs to download and install");

            var emulatorPath = Environment.ExpandEnvironmentVariables(@"%ProgramFiles%\Azure Cosmos DB Emulator\CosmosDB.Emulator.exe");
            if (!File.Exists(emulatorPath))
            {
                throw new ApplicationException(noEmulatorMessage.ToString());
            }
        }

        /// <summary>
        /// Verifies that the CosmosDB Emulator is currently running.
        /// </summary>
        private static void VerifyEmulatorRunning()
        {
            var processes = Process.GetProcessesByName("CosmosDB.Emulator");
            if (!processes.Any())
            {
                throw new ApplicationException("The CosmosDb Emulator is not running, please start it...");
            }
        }
    }
}