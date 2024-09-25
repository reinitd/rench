using System;
using Newtonsoft.Json;
using PhotinoNET;
using RenchGui.Models;
using RenchGui.Actions;

namespace RenchGui;

public static class ResponseManager {

    public static void Handle(PhotinoWindow window, string msg, string configPath) {
        Message? resp = JsonConvert.DeserializeObject<Message>(msg);
        if (resp == null) {
            Console.WriteLine("Resp is null.");
            return;
        }

        switch (resp.Action.ToLower()) {
            case "exit":
                Thread.Sleep(100); // Just enough time for the select audio to play.
                Environment.Exit(0);
                break;
            case "set_window_size":
                SetWindowSize sws = new(window);
                sws.Handle(resp);
                break;
            case "get_realms":
                GetRealms gr = new(window, configPath);
                gr.Handle(resp);
                break;
            default:
                Console.WriteLine($"Unknown action: {resp.Action.ToLower()}");
                break;
        }
    }
}