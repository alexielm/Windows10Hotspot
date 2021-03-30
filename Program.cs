using System;
using System.Diagnostics;

namespace Windows10Hotspot
{
    class Program
    {
        static string magicCode = "[Windows.Networking.NetworkOperators.NetworkOperatorTetheringManager, Windows.Networking.NetworkOperators, ContentType=WindowsRuntime]::CreateFromConnectionProfile([Windows.Networking.Connectivity.NetworkInformation, Windows.Networking.Connectivity, ContentType=WindowsRuntime]::GetInternetConnectionProfile())";

        static void Main(string[] args)
        {
            var setToON = String.Join(" ", args).ToUpper() switch
            {
                "ON" => true,
                "OFF" => false,
                _ => throw new Exception("Invalid parameter. Please specify ON or OFF")
            };

            var Process = new Process();
            Process.StartInfo = new ProcessStartInfo
            {
                FileName = "Powershell",
                Arguments = $"{magicCode}. {(setToON ? "Start" : "Stop")}TetheringAsync()",
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = false,
            };
            Process.Start();
            Process.WaitForExit();
            if (Process.ExitCode == 0)
            {
                Console.WriteLine($"Hostpot should be {(setToON ? "ON" : "OFF")} now");
            }
            else
            {
                Console.WriteLine("Error. Exit code: " + Process.ExitCode);
            }
        }
    }
}
