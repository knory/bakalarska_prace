namespace xknor.Functions.Model
{
    public class Config
    {
        public int ComboStreak { get; set; }
        public int MaxComboModifier { get; set; }
        public int ComboBreakStreak { get; set; }
        public int PointsPerCorrectComponent { get; set; }
        public int PerfectTaskBonusPoints { get; set; }
        public int UnusedTimeTaskBonus { get; set; }
        public int UnusedTimeGameBonus { get; set; }
        public int TimePerTask { get; set; }
        public int TimePerGame { get; set; }
        public int TasksPerGame { get; set; }
        public GameType GameType { get; set; }
        public FeedbackType FeedbackType { get; set; }
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