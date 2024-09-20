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

                    ResponseManager.Handle(window, message);
                }
            )
            .Load("wwwroot/index.html");

        
        window.WaitForClose();
    }
}
