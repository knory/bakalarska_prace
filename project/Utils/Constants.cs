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
        private static readonly string _resourcesPath = "res://Resources/";
        private static readonly string _resourcesTeammatesPath = _resourcesPath + "Teammates/";
        private static readonly string _resourcesProgressBarPath = _resourcesPath + "ProgressBar/";
        private static readonly string _resourcesLabelWithBackgroundPath = _resourcesPath + "LabelWithBackground/";

        // resources constants
        public static string[] SpriteNames { get; } = {$"{_resourcesPath}blue_node.png", $"{_resourcesPath}green_node.png", $"{_resourcesPath}purple_node.png", $"{_resourcesPath}red_node.png", $"{_resourcesPath}yellow_node.png"};
        public static ProgressBarResource[] ProgressBarResources { get; } = new ProgressBarResource[]
        {
            new ProgressBarResource{ Id = 1, TexturePath = $"{_resourcesProgressBarPath}one.png"},
            new ProgressBarResource{ Id = 2, TexturePath = $"{_resourcesProgressBarPath}two.png"},
            new ProgressBarResource{ Id = 3, TexturePath = $"{_resourcesProgressBarPath}three.png"},
            new ProgressBarResource{ Id = 4, TexturePath = $"{_resourcesProgressBarPath}four.png"},
            new ProgressBarResource{ Id = 5, TexturePath = $"{_resourcesProgressBarPath}five.png"},
        };

        // Multiple Select component constants
        public static int MULTIPLE_SELECT_VALUES_COUNT { get; } = 3;
        
        // Teammates component constants
        public static int POSSIBLE_TEAMMATES_COUNT { get; } = 3;
        public static int ADDED_TEAMMATES_COUNT { get; } = 5;

        // Double Dropdown component constants
        public static int DOUBLE_DROPDOWN_OPTIONS_COUNT { get; } = 12;

        // Side Scroll Select List component constants
        public static int[] VALUES_PER_LIST { get; } = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public static string[] MONTH_NAMES { get; } = new string[12] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        public static string[] DAY_NAMES { get; } = new string[7] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

        // Side Scroll Button component constants
        public static LabelWithButtonResource[] LABEL_WITH_BUTTON_RESOURCES { get; } = new LabelWithButtonResource[] 
        {
            new LabelWithButtonResource { Text = "Kava zdarma", BackgroundImage = (Texture)GD.Load($"{_resourcesLabelWithBackgroundPath}kava_zdarma.png") },
            new LabelWithButtonResource { Text = "Aj lav ju mucho grande", BackgroundImage = (Texture)GD.Load($"{_resourcesLabelWithBackgroundPath}maj_lav.png") }
        };

        // Rating component constants
        public static int RATING_POSSIBLE_VALUES { get; } = 5;

        public static Random RANDOM { get; } = new Random();
    }

    public static class Resources
    {
        private static readonly string _resourcesPath = "res://Resources/";
        private static readonly string _resourcesTeammatesPath = _resourcesPath + "Teammates/";
        private static readonly string _resourcesSharedPath = _resourcesPath + "Shared/";

        public static ResourceObject Nongamified = new ResourceObject
        {
            TeammateResources = new TeammateResource[]
            {
                new TeammateResource 
                { 
                    Id = 1, 
                    Name = "Evžen", 
                    BigTexturePath = $"{_resourcesTeammatesPath}evzen_big.png",
                    SmallTexturePath = $"{_resourcesTeammatesPath}evzen_small.png" 
                },
                new TeammateResource
                {
                    Id = 2,
                    Name = "Jonáš",
                    BigTexturePath = $"{_resourcesTeammatesPath}jonas_big.png",
                    SmallTexturePath = $"{_resourcesTeammatesPath}jonas_small.png"
                },
                new TeammateResource
                {
                    Id = 3,
                    Name = "Katka",
                    BigTexturePath = $"{_resourcesTeammatesPath}katka_big.png",
                    SmallTexturePath = $"{_resourcesTeammatesPath}katka_small.png"
                },
                new TeammateResource
                {
                    Id = 4,
                    Name = "Lukáš",
                    BigTexturePath = $"{_resourcesTeammatesPath}lukas_big.png",
                    SmallTexturePath = $"{_resourcesTeammatesPath}lukas_small.png"
                },
                new TeammateResource
                {
                    Id = 5,
                    Name = "Milena",
                    BigTexturePath = $"{_resourcesTeammatesPath}milena_big.png",
                    SmallTexturePath = $"{_resourcesTeammatesPath}milena_small.png"
                },
                new TeammateResource
                {
                    Id = 6,
                    Name = "Pavel",
                    BigTexturePath = $"{_resourcesTeammatesPath}pavel_big.png",
                    SmallTexturePath = $"{_resourcesTeammatesPath}pavel_small.png"
                },
                new TeammateResource
                {
                    Id = 7,
                    Name = "Renata",
                    BigTexturePath = $"{_resourcesTeammatesPath}renata_big.png",
                    SmallTexturePath = $"{_resourcesTeammatesPath}renata_small.png"
                },
                new TeammateResource
                {
                    Id = 8,
                    Name = "Tereza",
                    BigTexturePath = $"{_resourcesTeammatesPath}tereza_big.png",
                    SmallTexturePath = $"{_resourcesTeammatesPath}tereza_small.png"
                }
            },
            TeammateActionIcons = new Dictionary<string, Texture>
            {
                {"plus", (Texture)GD.Load($"{_resourcesTeammatesPath}plus_button.png") },
                {"minus", (Texture)GD.Load($"{_resourcesTeammatesPath}minus_button.png") }
            },
            SideButtonTextures = new Dictionary<string, Texture>
            {
                {"left", (Texture)GD.Load($"{_resourcesSharedPath}left_arrow.png")},
                {"right", (Texture)GD.Load($"{_resourcesSharedPath}right_arrow.png")}
            }
        };
    }

    public class ResourceObject
    {
        public TeammateResource[] TeammateResources { get; set; }
        public Dictionary<string, Texture> TeammateActionIcons { get; set; }
        public Dictionary<string, Texture> SideButtonTextures { get; set; }
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