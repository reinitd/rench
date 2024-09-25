using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;
using PhotinoNET;
using RenchGui.Models;

namespace RenchGui.Actions;

public class SetWindowSize(PhotinoWindow window) : IAction
{
    public string ActionName { get; } = "set_window_size";
    private readonly PhotinoWindow _window = window;

    public PhotinoWindow Window => _window;

    public void Handle(Message message)
    {
        string[] s = message.Value.Split("x");
        Size size = new(int.Parse(s[0]), int.Parse(s[1]));
        Console.WriteLine(size.ToString());
        _window.SetSize(size);
        _window.Center();
    }

}
