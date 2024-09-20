using System;
using Newtonsoft.Json;
using PhotinoNET;
using RenchGui.Models;
using RenchGui.Actions;

namespace RenchGui;

public static class ResponseManager {

    public static void Handle(PhotinoWindow window, string msg) {
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
            default:
                Console.WriteLine($"Unknown action: {resp.Action.ToLower()}");
                break;
        }
    }
}