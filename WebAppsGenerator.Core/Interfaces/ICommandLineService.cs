using System.Diagnostics;

namespace WebAppsGenerator.Core.Interfaces
{
    public interface ICommandLineService
    {
        int RunCommand(string command, params string[] inputLines);
    }
}