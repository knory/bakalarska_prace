using Godot;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Utils
{
    public static class Constants
    {
        // resources path constants
        private static string _resourcesPath = "res://Resources/";
        private static string _resourcesTeammatesPath = _resourcesPath + "Teammates/";
        private static string _resourcesProgressBarPath = _resourcesPath + "ProgressBar/";
        private static string _resourcesLabelWithBackgroundPath = _resourcesPath + "LabelWithBackground/";

        // resources constants
        public static string[] SpriteNames = {$"{_resourcesPath}blue_node.png", $"{_resourcesPath}green_node.png", $"{_resourcesPath}purple_node.png", $"{_resourcesPath}red_node.png", $"{_resourcesPath}yellow_node.png"};
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

        // Side Scroll Button component constants
        public static LabelWithButtonResource[] LABEL_WITH_BUTTON_RESOURCES = new LabelWithButtonResource[] 
        {
            new LabelWithButtonResource { Text = "Kava zdarma", BackgroundImage = (Texture)GD.Load($"{_resourcesLabelWithBackgroundPath}kava_zdarma.png") },
            new LabelWithButtonResource { Text = "Aj lav ju mucho grande", BackgroundImage = (Texture)GD.Load($"{_resourcesLabelWithBackgroundPath}maj_lav.png") }
        };

        // Rating component constants
        public const int RATING_POSSIBLE_VALUES = 5;

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
}