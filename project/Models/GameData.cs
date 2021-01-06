using System;

namespace Models
{
    public class GameData
    {
        /// <summary>
        /// The time when the game data record was added.
        /// </summary>
        public DateTime TimeAdded { get; set; }

        /// <summary>
        /// Player's username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Game config relevant for this data object.
        /// </summary>
        public string GameConfig { get; set; }

        /// <summary>
        /// Points gained in the game.
        /// </summary>
        public int GainedPoints { get; set; }

        /// <summary>
        /// Longest streak of consecutive tasks completed without a fault.
        /// </summary>
        public int LongestPerfectStreak { get; set; }

        /// <summary>
        /// Number of correct actions in the game.
        /// </summary>
        public int CorrectActions { get; set; }

        /// <summary>
        /// Number of total actions in the game.
        /// </summary>
        public int TotalActions { get; set; }

        /// <summary>
        /// Number of correct sequences in the game.
        /// </summary>
        public int CorrectSequences { get; set; }

        /// <summary>
        /// Number of total sequences in the game.
        /// </summary>
        public int TotalSequences { get; set; }

        /// <summary>
        /// Time spent in-game in seconds.
        /// </summary>
        public float TimeSpent { get; set; }

        /// <summary>
        /// Time limit of the game in seconds.
        /// </summary>
        public int TimeLimit { get; set; }

        /// <summary>
        /// Number of sequences completed by the confirm button.
        /// </summary>
        public int SequencesButton { get; set; }

        /// <summary>
        /// Number of correct sequences completed by the confirm button.
        /// </summary>
        public int CorrectSequencesButton { get; set; }

        /// <summary>
        /// Number of sequences completed by the task time limit running out.
        /// </summary>
        public int SequencesTimeLimit { get; set; }

        /// <summary>
        /// Number of correct sequences completed by the task time limit running out.
        /// </summary>
        public int CorrectSequencesTimeLimit { get; set; }
    }
}