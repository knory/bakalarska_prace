using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public static class Constants
{
    // resources path constants
    private static string _resourcesPath = "res://resources/";
    private static string _resourcesTeammatesPath = _resourcesPath + "teammates/";
    private static string _resourcesProgressBarPath = _resourcesPath + "progress_bar/";

    // resources constants
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

    // Multiple Select component constants
    public const int MULTIPLE_SELECT_VALUES_COUNT = 3;
    
    // Teammates component constants
    public const int POSSIBLE_TEAMMATES_COUNT = 3;
    public const int ADDED_TEAMMATES_COUNT = 5;

    // Double Dropdown component constants
    public const int DOUBLE_DROPDOWN_OPTIONS_COUNT = 12;

    // Side Scroll Select List component constants
    public static int[] VALUES_PER_LIST = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    public static string[] MONTH_NAMES = new string[12] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    public static string[] DAY_NAMES = new string[7] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

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