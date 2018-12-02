using System;
using System.Collections.Generic;
using System.Text;
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

        public void RunCommand(string command)
        {
            Commands.Add(command);
        }
    }
}
