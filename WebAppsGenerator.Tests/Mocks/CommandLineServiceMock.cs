using System;
using System.Collections.Generic;
using System.Text;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Services;

namespace WebAppsGenerator.Tests.Mocks
{
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
