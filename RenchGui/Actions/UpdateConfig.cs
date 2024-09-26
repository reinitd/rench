using System.Drawing;
using PhotinoNET;
using RenchGui.Models;
using RenchGui.Helpers;
using Newtonsoft.Json;

namespace RenchGui.Actions;

public class UpdateConfig(PhotinoWindow window, string configPath) : IAction
{
    public string ActionName { get; } = "update_config";
    private readonly PhotinoWindow _window = window;
    private readonly string _configPath = configPath;
    private readonly Communication _com = new(window);
    public PhotinoWindow Window => _window;

    public void Handle(Message message)
    {
        Result<Config?> result = new(false, "An unknown error occured.", null);
        Message response = new()
        {
            Action = "update_config_response",
            Value = null
        };

        Config? updatedCfg = JsonConvert.DeserializeObject<Config>(message.Value);
        if (updatedCfg == null) {
            result.Message = "Unable to deserialize updated config.";
            _com.Send(response, result);
            return;
        }

        Config? cfg = JsonConvert.DeserializeObject<Config>(File.ReadAllText(_configPath));
        if (cfg == null) {
            result.Message = "Unable to deserialize original config.";
            _com.Send(response, result);
            return;
        }

        if (cfg.FirstOpenTime != updatedCfg.FirstOpenTime) {
            result.Message = "You cannot change the first opened time!";
            _com.Send(response, result);
            return;
        }

        if (cfg.PromptedToInstallGD != updatedCfg.PromptedToInstallGD) {
            result.Message = "You cannot change prompted to install Google Drive!";
            _com.Send(response, result);
            return;
        }

        if (cfg.IsGDInstalled != updatedCfg.IsGDInstalled) {
            result.Message = "You cannot change is Google Drive installed!";
            _com.Send(response, result);
            return;
        }

        File.WriteAllText(_configPath, JsonConvert.SerializeObject(updatedCfg));


        result.Success = true;
        result.Message = "OK";
        result.Value = updatedCfg;

        _com.Send(response, result);
    }

}
