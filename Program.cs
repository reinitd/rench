using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Rench.Helpers;
using Rench.Models;
using Spectre.Console;

namespace Rench;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        if (!File.Exists("./Rench.json"))
        {
            string warning =
                "Please do the following if this application opened in Command Prompt:\n";
            warning +=
                "    0. If the window you're seeing this in is blue or fancy/modern, then ignore this and re-run the application.\n\n";
            warning += "    1. Click WIN+R and run \"intl.cpl\" (without the quotes)\n";
            warning += "    2. Click the Administrative tab\n";
            warning += "    3. Click the Change system locale button\n";
            warning +=
                "    4. Check the \"Use Unicode UTF-8 for worldwide language support\" checkbox\n";
            warning += "    5. Reboot\n";

            Console.WriteLine(warning);
            RenchInfoService _ = new();
            return;
        }

        RenchInfoService ri = new();

        RenchInfo? r = ri.Info;
        Process[] allProcs = Process.GetProcesses();
        if (!allProcs.Any(p => p.ProcessName.ToLower().Replace(" ", "").Contains("googledrive")))
        {
            string warning = "Google Drive is not installed, please do the following:\n";
            warning +=
                "    0. If you haven't already, go to \"https://drive.google.com\" > shared with me > right click the shared save folder and click add shorcut; you want to add a shortcut to your drive.\n\n";
            warning +=
                "    1. Go to \"https://google.com/drive/download/\" and download Google Drive for Desktop\n";
            warning += "    2. Run the downloaded installer\n";
            warning += "    3. Do not sync any of your local files to GD (unless you want to)\n";
            warning +=
                "    4. Once GD has installed, go to your Google Drive in the file explorer (usually G:)\n";
            warning +=
                "    5. Verify that the shared drive folder is in your Google Drive, you may have to scroll down since it's not technically a folder, and wont show at the top of the file explorer like folders do.\n";
            warning +=
                "    6. If it isn't there, give GD time sync your files. If it is there, please re-run this application.\n";
            warning +=
                "\nIf you know you have Google Drive installed and all set-up, please run it (preferably set it to run on start-up) then re-run this application.\n";

            Console.WriteLine(warning);

            r.PromptedToInstallGD = true;
            r.IsGDInstalled = false;
            ri.Update(r);

            return;
        }
        else
        {
            r.PromptedToInstallGD = false;
            r.IsGDInstalled = true;
            ri.Update(r);
        }

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new Overlay());
    }
}
