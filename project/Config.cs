using Godot;
using System;

public class Config
{
    public int IncrementComboStreak { get; set; }
    public int MaxComboModifier { get; set; }
    public int ComboBreakStreak { get; set; }
    public int PerfectTaskBonusPoints { get; set; } //DONE
    public int UnusedTimeTaskBonus { get; set; } //DONE
    public int UnusedTimeGameBonus { get; set; }
    public int TimePerTask { get; set; } //DONE
    public int TimePerGame { get; set; } //DONE
    public int TasksPerGame { get; set; } //DONE
    public SuccessRating SuccessRatingType { get; set; }
    public GameControls GameControlsType { get; set; }

    public Config()
    { }
}

public enum SuccessRating 
{
    CompletedTasks,
    GainedPoints
}

public enum GameControls 
{
    MouseClick,
    Keyboard
}