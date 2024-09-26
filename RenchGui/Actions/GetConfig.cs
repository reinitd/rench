using System.Drawing;
using PhotinoNET;
using RenchGui.Models;
using RenchGui.Helpers;
using Newtonsoft.Json;

namespace RenchGui.Actions;

public class GetConfig(PhotinoWindow window, string configPath) : IAction
{
    public string ActionName { get; } = "get_config";
    private readonly PhotinoWindow _window = window;
    private readonly string _configPath = configPath;
    private readonly Communication _com = new(window);
    public PhotinoWindow Window => _window;

    public void Handle(Message message)
    {
        Result<Config?> result = new(false, "An unknown error occured.", null);
        Message response = new()
        {
            Action = "get_config_response",
            Value = null
        };

        Config? cfg = JsonConvert.DeserializeObject<Config>(File.ReadAllText(_configPath));
        if (cfg == null) {
            result.Message = "Unable to deserialize config.";
            _com.Send(response, result);
            return;
        }


        result.Success = true;
        result.Message = "OK";
        result.Value = cfg;

        _com.Send(response, result);
    }

}
