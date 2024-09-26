using System;
using System.Drawing;
using System.IO;
using System.Text;
using PhotinoNET;

namespace RenchGui;

class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        string configPath = "missing";
        foreach (var arg in args)
        {
            if (arg.StartsWith("--config="))
            {
                string configValue = arg.Split('=')[1];
                configPath = configValue;

            }
        }

        if (configPath == "missing") {
            Console.WriteLine("Please pass in the config path.\n\n  Ex: --config=../rench.json");
            return;
        }

        if (!File.Exists(configPath)) {
            Console.WriteLine("Config file at the specified path does not exist!");
            return;
        }

        string windowTitle = "RenchGui";

        var window = new PhotinoWindow()
            .SetTitle(windowTitle)
            .SetUseOsDefaultSize(false)
            .SetSize(new Size(300, 250))
            .SetChromeless(true)
            .Center()
            .SetResizable(false)
            .SetContextMenuEnabled(false)
            // Most event handlers can be registered after the
            // PhotinoWindow was instantiated by calling a registration
            // method like the following RegisterWebMessageReceivedHandler.
            // This could be added in the PhotinoWindowOptions if preferred.
            .RegisterWebMessageReceivedHandler(
                (object sender, string message) =>
                {
                    var window = (PhotinoWindow)sender;

                    // The message argument is coming in from sendMessage.
                    // "window.external.sendMessage(message: string)"
                    // string response = $"Received message: \"{message}\"";

                    // Send a message back the to JavaScript event handler.
                    // "window.external.receiveMessage(callback: Function)"
                    // window.SendWebMessage(response);

                    ResponseManager.Handle(window, message, configPath);
                }
            )
            .Load("wwwroot/index.html");

        // TODO: Find a fix for MacOS not being centered vertically.
        // Point pt = window.Location;
        // window.SetLocation(new Point(pt.X, pt.Y - 1000));

        window.WaitForClose();
    }
}
