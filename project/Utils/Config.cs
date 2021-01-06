using Godot;
using System;

namespace Utils
{
    public class Config
    {
        /// <summary>
        /// Length of perfect task streak to increase the combo modifier.
        /// </summary>
        public int ComboStreak { get; set; }

        /// <summary>
        /// Maximal combo modifier.
        /// </summary>
        public int MaxComboModifier { get; set; }

        /// <summary>
        /// Length of faulty task streak to reset the combo modifier.
        /// </summary>
        public int ComboBreakStreak { get; set; }

        /// <summary>
        /// Number of points gained per correct component.
        /// </summary>
        public int PointsPerCorrectComponent { get; set; }

        /// <summary>
        /// Number of bonus points for perfect task.
        /// </summary>
        public int PerfectTaskBonusPoints { get; set; }

        /// <summary>
        /// Bonus points per unused second of task time limit.
        /// </summary>
        public int UnusedTimeTaskBonus { get; set; }

        /// <summary>
        /// Bonus points per unused second of game time limit.
        /// </summary>
        public int UnusedTimeGameBonus { get; set; }

        /// <summary>
        /// Time limit per task in seconds.
        /// </summary>
        public int TimePerTask { get; set; }

        /// <summary>
        /// Time limit per game in seconds.
        /// </summary>
        public int TimePerGame { get; set; }

        /// <summary>
        /// Number of tasks per game.
        /// </summary>
        public int TasksPerGame { get; set; }

        /// <summary>
        /// Game type.
        /// </summary>
        public GameType GameType { get; set; }

        /// <summary>
        /// Feedback type.
        /// </summary>
        public FeedbackType FeedbackType { get; set; }

        public Config()
        { }
    }

    public enum GameType
    {
        Gamified,
        Nongamified
    }

    public enum FeedbackType
    {
        Simple,
        Quality
    }
}