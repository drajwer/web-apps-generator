using System.Collections.Generic;
using WebAppsGenerator.Core.Interfaces;

namespace WebAppsGenerator.Tests.Mocks
{
    /// <summary>
    /// Stores commands in a list
    /// </summary>
    public class CommandLineServiceMock : ICommandLineService
    {
        public List<string> Commands;

        public CommandLineServiceMock()
        {
            Commands = new List<string>();
        }

        public int RunCommand(string command, params string[] inputLines)
        {
            Commands.Add(command);
            return 0;
        }
    }
}
