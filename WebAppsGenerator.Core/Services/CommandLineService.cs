using System;
using System.Diagnostics;
using WebAppsGenerator.Core.Interfaces;

namespace WebAppsGenerator.Core.Services
{
    public class CommandLineService : ICommandLineService
    {
        private const string Cmd = "cmd.exe";
        private const string Bash = "/bin/bash";
        private static PlatformID CurrentPlatformId => Environment.OSVersion.Platform;

        private Action<string> _outputAction;
        private Action<string> _errorAction;


        public CommandLineService()
        {
            _outputAction = Console.WriteLine;
            _errorAction = Console.WriteLine;
        }
        public CommandLineService(Action<string> outputAction, Action<string> errorAction)
        {
            _outputAction = outputAction;
            _errorAction = errorAction;
        }

        public int RunCommand(string command)
        {
            Process process = CreateProcessForCommand(command);

            process.WaitForExit();
            var exitCode = process.ExitCode;

            process.Close();

            return exitCode;
        }

        public Process CreateProcessForCommand(string command, bool createNewWindow = false)
        {
            var shell = GetShell();
            var arguments = GetArguments(command);

            var processInfo = new ProcessStartInfo(shell, arguments)
            {
                CreateNoWindow = !createNewWindow,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            var process = Process.Start(processInfo);

            process.OutputDataReceived += (sender, e) => _outputAction(e.Data);
            process.ErrorDataReceived += (sender, e) => _errorAction(e.Data);
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            return process;
        }

        private static string GetArguments(string command)
        {
            if (CurrentPlatformId == PlatformID.Unix || CurrentPlatformId == PlatformID.MacOSX)
                return $"-c \"{command}\"";
            return $"/c \"{command}\"";
        }

        private static string GetShell()
        {
            if (CurrentPlatformId == PlatformID.Unix || CurrentPlatformId == PlatformID.MacOSX)
                return Bash;
            return Cmd;
        }
    }
}
