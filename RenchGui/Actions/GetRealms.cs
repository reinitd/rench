using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;
using PhotinoNET;
using RenchGui.Models;
using RenchGui.Helpers;

namespace RenchGui.Actions;

public class GetRealms(PhotinoWindow window, string configPath) : IAction
{
    public string ActionName { get; } = "get_realms";
    private readonly PhotinoWindow _window = window;
    private readonly string _configPath = configPath;
    private readonly Communication _com = new(window);
    public PhotinoWindow Window => _window;

    public void Handle(Message message)
    {
        Result<string[]?> result = new(false, "An unknown error occured.", null);
        Message response = new(){
            Action = "get_realms_response",
            Value = null
        };

        Config? cfg = JsonConvert.DeserializeObject<Config>(File.ReadAllText(_configPath));
        if (cfg == null) {
            result.Message = "Unable to deserialize config.";
            _com.Send(response, result);
            return;
        }

        if (cfg.GDRealmPath == null) {
            result.Message = "Please go to the settings tab and set the path to your Google Drive realm folder.";
            _com.Send(response, result);
            return;
        }

        if (cfg.WrenchSavePath == null) {
            result.Message = "Please go to the settings tab and set the path for your Wrench save folder.";
            _com.Send(response, result);
            return;
        }

        _com.Send(response, result);
    }

}
