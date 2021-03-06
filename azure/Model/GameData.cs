using System;

namespace xknor.Functions.Model
{
    public class GameData
    {
        public int DataId { get; set; }
        public DateTime TimeAdded { get; set; }
        public string Username { get; set; }
        public string GameConfig { get; set; }
        public int GainedPoints { get; set; }
        public int LongestPerfectStreak { get; set; }
        public int CorrectActions { get; set; }
        public int TotalActions { get; set; }
        public int CorrectSequences { get; set; }
        public int TotalSequences { get; set; }
        public float TimeSpent { get; set; }
        public int TimeLimit { get; set; }
        public int SequencesButton { get; set; }
        public int CorrectSequencesButton { get; set; }
        public int SequencesTimeLimit { get; set; }
        public int CorrectSequencesTimeLimit { get; set; }
    }
}