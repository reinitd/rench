using System;
using Newtonsoft.Json;
using PhotinoNET;
using RenchGui.Models;

namespace RenchGui.Actions;

public interface IAction {
    string ActionName { get; }
    PhotinoWindow Window { get; }
    void Handle(Message message);
}