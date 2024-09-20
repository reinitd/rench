using System;
using System.Diagnostics;
using System.Linq;
using NAudio.Wave;
using NAudio.Vorbis;
using Newtonsoft.Json;
using PhotinoNET;
using RenchGui.Models;

namespace RenchGui.Actions;

public class PlayAudio : IAction
{
    public string ActionName { get; } = "play_audio";
    private PhotinoWindow _window;

    public PhotinoWindow Window => _window;

    public PlayAudio(PhotinoWindow window)
    {
        _window = window;
    }

    public void Handle(Message message)
{
    string audioName = message.Value;
    string? audio = null;

    switch (audioName)
    {
        case "hover":
            audio = "tap-toothy.ogg";
            break;
        case "select":
            audio = "tap-mellow.ogg";
            break;
        default:
            break;
    }

    if (audio == null)
    {
        return;
    }

    Console.WriteLine($"AUDIO: {audio}");

    Task.Run(() =>
    {
        using (var vorbisReader = new VorbisWaveReader($"Resources/audio/{audio}"))
        using (var outputDevice = new WaveOutEvent())
        {
            outputDevice.Init(vorbisReader);
            outputDevice.Play();

            while (outputDevice.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(100); // ms
            }
        }
    });
}
}
