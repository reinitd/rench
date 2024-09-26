using System;
using Newtonsoft.Json;
using PhotinoNET;
using RenchGui.Models;

namespace RenchGui.Helpers;

public class Communication(PhotinoWindow window) {
    private readonly PhotinoWindow _window = window;
    public void Send(Message response, Result? result) {
        response.Value = JsonConvert.SerializeObject(result);
        _window.SendWebMessage(JsonConvert.SerializeObject(response));
    }

    public string Receive() {
        throw new NotImplementedException();
    }
}
