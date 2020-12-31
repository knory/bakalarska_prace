namespace Utils
{
    public static class Constants
    {
        public static readonly string ApiHost = "http://xknor-gamification.azurewebsites.net";
        public static readonly string ApiDataCollectorUrl = "/api/GameDataCollector";

        public static readonly string ResourcesPath = "res://Resources/";
        public static readonly string NongamifiedResourcesPath = ResourcesPath + "Nongamified/";

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

    public static class ResourceStrings
    {
        public static readonly string CompletedTasksCount = "počet sekvencí: ";
        public static readonly string TotalActions = "hotové úkony: ";
        public static readonly string CorrectActions = "správně: ";
        public static readonly string StreakNotification = "x v řadě!";

        // Nongamified task completed popup constants
        public static readonly string CorrectActionsInSequence = "Správné úkony v sekvenci";
        public static readonly string CorrectActionsStreak = "Úkony bez chyby v řadě";
        public static readonly string TaskTimeLeft = "Ušetřený čas v sekvenci";
        public static readonly string CurrentPerformance = "Aktuální výkon";
        public static readonly string PreviousAverage = "Průměr předchozích";
        public static readonly string PopupHeadline = "Zkontrolovali jste $ORDER$. sekvenci funkcí!\nDobrá práce!";

        public static class Nongamified
        {
            // Advertisement task constants
            public static readonly string ADVERTISEMENT_TASK_BASE = "Přidej ke schůzce akci";
            public static string[] ADVERTISEMENT_VALUES = 
            { 
                "káva zdarma", 
                "wellness pobyt", 
                "let balonem" 
            };

            // Calendar task constants
            public static readonly string CALENDAR_TASK_BASE = "Nastav datum schůzky na";
            public static string[] CALENDAR_VALUES = 
            { 
                "leden", "únor", "březen", 
                "duben", "květen", "červen", 
                "červenec", "srpen", "září", 
                "říjen", "listopad", "prosinec" 
            };

            // Devices task constants
            public static readonly string DEVICES_TASK_BASE = "Zatrhni, že je potřeba mít";
            public static string[] DEVICES_VALUES = 
            { 
                "sluchátka", 
                "mikrofon", 
                "kameru" 
            };

            // Position task constants
            public static readonly string POSITION_VALUE = "Nastav uživatele na administrátora";

            // Priority task constants
            public static readonly string PRIORITY_TASK_BASE = "Zvol pro schůzku";
            public static string[] PRIORITY_VALUES = 
            { 
                "nízkou prioritu",
                "střední prioritu",
                "vysokou prioritu"
            };

            // Rating task constants
            public static readonly string RATING_TASK_BASE = "Ohodnoť týmové řešení";
            public static string[] RATING_VALUES =
            {
                "1 hvězdičkou",
                "2 hvězdičkami",
                "3 hvězdičkami",
                "4 hvězdičkami",
                "5 hvězdičkami"
            };

            // Teammates task constants
            public static readonly string TEAMMATES_TASK_BASE = "Přidej do týmu na schůzku:";
            public static string[] TEAMMATES_VALUES =
            {
                "Evžen", "Jonáš", "Katka", "Lukáš",
                "Milena", "Pavel", "Renata", "Tereza",
            };

            // Theme task constants
            public static readonly string THEME_VALUE = "Změň motiv aplikace ze šedé na červenou";

            // Time task constants
            public static readonly string TIME_TASK_BASE = "Nastav čas schůzky na";
            public static string[] TIME_HOUR_VALUES =
            {
                "0 hodin", "2 hodiny", "4 hodiny", "6 hodin", 
                "8 hodin", "10 hodin", "12 hodin", "14 hodin",
                "16 hodin", "18 hodin", "20 hodin", "22 hodin"
            };
            public static string[] TIME_MINUTE_VALUES =
            {
                "00 minut", "5 minut", "10 minut", "15 minut",
                "20 minut", "25 minut", "30 minut", "35 minut",
                "40 minut", "45 minut", "50 minut", "55 minut"
            };

            // Topics task constants
            public static readonly string TOPICS_TASK_BASE = "Nastav téma schůzky na";
            public static string[] TOPICS_VALUES =
            {
                "Finance",
                "Statistiky",
                "Zdravotnictví",
                "Vědu",
                "Sport",
                "Jiné"
            };

            // Volume task constants
            public static readonly string VOLUME_TASK_BASE = "Nastav hlasitost zvuku aplikace na";
            public static string[] VOLUME_VALUES =
            {
                "nízkou",
                "střední",
                "vysokou"
            };
        }
    }
}