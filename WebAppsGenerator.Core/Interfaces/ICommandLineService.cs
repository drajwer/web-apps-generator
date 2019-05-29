using System.Diagnostics;

namespace WebAppsGenerator.Core.Interfaces
{
    public interface ICommandLineService
    {
        int RunCommand(string command);
        Process CreateProcessForCommand(string command, bool createNewWindow = false);
    }
}