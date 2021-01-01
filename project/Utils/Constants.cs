namespace Utils
{
    public static class Constants
    {
        public static readonly string ApiHost = "https://xknor-gamification.azurewebsites.net";
        public static readonly string ApiDataCollectorUrl = "/api/GameDataCollector";

        public static readonly string ResourcesPath = "res://Resources/";
        public static readonly string NongamifiedResourcesPath = ResourcesPath + "Nongamified/";
        public static readonly string GamifiedResourcesPath = ResourcesPath + "Gamified/";

        public static int NUMBER_OF_ASSIGNMENTS_PER_TASK { get; } = 5;
        public static int GAME_COUNTDOWN_WAIT_TIME { get; } = 5;
        public static int STREAK_NOTIFICATION_TIME { get; } = 5;
        public static int MINIMAL_STREAK_NOTIFICATION { get; } = 3;

        // Advertisement (or equivalent) component constants
        public static int ADVERTISEMENT_NUMBER_OF_ITEMS { get; } = 3;

        // Calendar (or equivalent) component constants
        public static int[] CALENDAR_VALUES_PER_LIST { get; } = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        // Devices (or equivalent) component constants
        public static int DEVICES_NUMBER_OF_ITEMS { get; } = 3;

        // Priority (or equivalent) component constants
        public static int PRIORITY_NUMBER_OF_ITEMS { get; } = 3;

        // Rating (or equivalent) component constants
        public static int RATING_NUMBER_OF_ITEMS { get; } = 5;

        // Teammates (or equivalent) component constants
        public static int TEAMMATES_POSSIBLE_COUNT { get; } = 3;
        public static int TEAMMATES_ADDED_COUNT { get; } = 5;
        public static int TEAMMATES_NUMBER_OF_ITEMS { get; } = 8;

        // Time (or equivalent) component constants
        public static int TIME_OPTIONS_COUNT { get; } = 12;

        // Topics (or equivalent) component constants
        public static int TOPICS_OPTIONS_COUNT { get; } = 6;

        // Volume (or equivalent) component constants
        public static int VOLUME_OPTIONS_COUNT { get; } = 3;
    }
}