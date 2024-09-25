using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;
using PhotinoNET;
using RenchGui.Models;

namespace RenchGui.Actions;

public class GetRealms(PhotinoWindow window, string configPath) : IAction
{
    public string ActionName { get; } = "get_realms";
    private readonly PhotinoWindow _window = window;
    private readonly string _configPath = configPath;

    public PhotinoWindow Window => _window;

    private void Send(Message response, Result? result) {
        response.Value = JsonConvert.SerializeObject(result);
        _window.SendWebMessage(JsonConvert.SerializeObject(response));
    }

    public void Handle(Message message)
    {
        Result result = new(false, "An unknown error occured.");
        Message response = new(){
            Action = "get_realms_response",
            Value = null
        };

        Config? cfg = JsonConvert.DeserializeObject<Config>(File.ReadAllText(_configPath));
        if (cfg == null) {
            result.Message = "Unable to deserialize config.";
            Send(response, result);
            return;
        }

        if (cfg.GDRealmPath == null) {
            result.Message = "Please go to the settings tab and set the path to your Google Drive realm folder.";
            Send(response, result);
            return;
        }

        Send(response, result);
    }

}
