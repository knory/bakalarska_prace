using Godot;
using System;

public static class Variables
{
    public static string[] SpriteNames = {"blue_node.png", "green_node.png", "purple_node.png", "red_node.png", "yellow_node.png"};
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