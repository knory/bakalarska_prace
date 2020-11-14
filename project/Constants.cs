using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public static class Constants
{
    private static string _resourcesPath = "res://resources/";
    private static string _resourcesTeammatesPath = _resourcesPath + "teammates/";
    private static string _resourcesProgressBarPath = _resourcesPath + "progress_bar/";

    public static string[] SpriteNames = {"blue_node.png", "green_node.png", "purple_node.png", "red_node.png", "yellow_node.png"};
    public static Dictionary<string, Texture> TeammateActionIcons = new Dictionary<string, Texture>
    { 
        {"plus", (Texture)GD.Load($"{_resourcesTeammatesPath}plus.png") },
        {"minus", (Texture)GD.Load($"{_resourcesTeammatesPath}minus.png") }
    };
    public static TeammateResource[] TeammateResources = new TeammateResource[]
    {
        new TeammateResource{ Id = 1, Name = "Evžen", TexturePath = $"{_resourcesTeammatesPath}evzen.png" },
        new TeammateResource{ Id = 2, Name = "Jonáš", TexturePath = $"{_resourcesTeammatesPath}jonas.png" },
        new TeammateResource{ Id = 3, Name = "Katka", TexturePath = $"{_resourcesTeammatesPath}katka.png" },
        new TeammateResource{ Id = 4, Name = "Lukáš", TexturePath = $"{_resourcesTeammatesPath}lukas.png" },
        new TeammateResource{ Id = 5, Name = "Tereza", TexturePath = $"{_resourcesTeammatesPath}tereza.png" }
    };
    public static ProgressBarResource[] ProgressBarResources = new ProgressBarResource[]
    {
        new ProgressBarResource{ Id = 1, TexturePath = $"{_resourcesProgressBarPath}one.png"},
        new ProgressBarResource{ Id = 2, TexturePath = $"{_resourcesProgressBarPath}two.png"},
        new ProgressBarResource{ Id = 3, TexturePath = $"{_resourcesProgressBarPath}three.png"},
        new ProgressBarResource{ Id = 4, TexturePath = $"{_resourcesProgressBarPath}four.png"},
        new ProgressBarResource{ Id = 5, TexturePath = $"{_resourcesProgressBarPath}five.png"},
    };
    public const int MULTIPLE_SELECT_VALUES_COUNT = 3;
    
    public const int POSSIBLE_TEAMMATES_COUNT = 3;
    public const int ADDED_TEAMMATES_COUNT = 5;

    public static Random RANDOM = new Random();
}

public struct ResourceStrings
{
    public static string CompletedTasks => "Completed tasks";
    public static string CurrentCombo => "Current combo";
    public static string GameOver => "Game Over";
    public static string GameStartsIn => "Game starts in";
    public static string PerfectTasks => "Perfect tasks";
    public static string Score => "Score";
    public static string StartGame => "Start Game";
    public static string TaskTimeLeft => "Task time left";
    public static string TotalTimeLeft => "Total time left";
}