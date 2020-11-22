using Godot;
using System;

namespace Utils
{
    public class Config
    {
        public int ComboStreak { get; set; } //DONE
        public int MaxComboModifier { get; set; } //DONE
        public int ComboBreakStreak { get; set; } //DONE
        public int PerfectTaskBonusPoints { get; set; } //DONE
        public int UnusedTimeTaskBonus { get; set; } //DONE
        public int UnusedTimeGameBonus { get; set; }
        public int TimePerTask { get; set; } //DONE
        public int TimePerGame { get; set; } //DONE
        public int TasksPerGame { get; set; } //DONE
        public SuccessRating SuccessRatingType { get; set; }

        public Config()
        { }
    }

    public enum SuccessRating 
    {
        CompletedTasks,
        GainedPoints
    }
}