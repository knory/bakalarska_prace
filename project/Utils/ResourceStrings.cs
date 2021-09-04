using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class ResourceStrings
    {
        public static readonly string TotalActions = "hotové úkony: ";
        public static readonly string CorrectActions = "správně: ";

        public static class Gamified
        {
            public static readonly string GainedPoints = "body:";
            public static readonly string CompleteSequence = "Vydej se do boje!";
            public static readonly string ComboNotification = "X combo";

            // Task completed popup constants
            public static readonly string Award = "Ocenění";
            public static readonly string Points = "Body";
            public static readonly string EquipmentQuality = "Kvalita výbavy";
            public static readonly string PerfectTaskBonusPoints = "Bonus za bezchybné přípravy";
            public static readonly string SavedTime = "Ušetřený čas";
            public static readonly string TotalPoints = "Celkem";
            public static string[] PopupHeadlines =
            {
                "Vypravili jste $ORDER$. výpravu a část jí nepřežila útok $MONSTER$.\nPříští to zvládne!",
                "Vypravili jste $ORDER$. výpravu, která zdržuje $MONSTER$!\nPošlete pomoc!",
                "Vypravili jste $ORDER$. výpravu a zpomalili jste $MONSTER$!\nDobrá práce!",
                "Vypravili jste $ORDER$. výpravu a zahnali jste dočasně $MONSTER$!\nSkvěle!",
                "Vypravili jste $ORDER$. výpravu a zahnali jste natrvalo $MONSTER$!\nVýborně!",
                "Vypravili jste $ORDER$. výpravu a zabili jste $MONSTER$.\nHurá!",
            };
            public static string[] PopupMonsters =
            {
                "draka", "trolla", "vlkodlaka", "baziliška", "golema", "ghúla"
            };

            // Animals task constants
            public static readonly string ANIMALS_TASK_BASE = "Sežeň si ze stáje na cestu";
            public static string[] ANIMALS_VALUES =
            {
                "slona",
                "velblouda",
                "osla"
            };

            // Armor task constants
            public static readonly string ARMOR_TASK_BASE = "Vezmi si $ARMOR$ zbroj a potři ji $OIL$ oleji";
            public static string[] ARMOR_VALUES =
            {
                "božskou", "falérovou", "koženou", "kroužkovou",
                "lamelovou", "magickou", "plátovou", "prošívanou",
                "šupinovou", "titánskou", "vlněnou", "žádnou"
            };

            // Candles task constants
            public static readonly string CANDLES_TASK_BASE = "Zapal u oltáře pro štěstí $CANDLES$";
            public static string[] CANDLES_VALUES =
            {
                "1 svíčku", "2 svíčky", "3 svíčky", "4 svíčky", "5 svíček"
            };

            // Chest task constants
            public static readonly string CHEST_TASK_BASE = "Vyber kliknutím pokladnici";

            // Daytime task constants
            public static readonly string DAYTIME_TASK_BASE = "Nastav dobu, kdy vyrazíš na výpravu, na $TIME$ hodin";
            public static string[] DAYTIME_VALUES =
            {
                "5:00", "12:00", "19:00"
            };

            // Energy Booster task constants
            public static readonly string ENERGY_BOOSTER_TASK_BASE = "Zapni posilovač energie na vnímání";

            // Fighters task constants
            public static readonly string FIGHTERS_TASK_BASE = "Vezmi s sebou na výpravu";
            public static string[] FIGHTERS_VALUES =
            {
                "kobolda", "trollku", "skřítku", "půlorku",
                "ogryni", "gnóma", "zlobra", "džina"
            };

            // Merchant task constants
            public static readonly string MERCHANT_TASK_BASE = "Nakup $FOOD$ jídla a $WATER$ vody";
            public static string[] MERCHANT_FOOD_VALUES =
            {
                "1 balíček", "2 balíčky", "3 balíčky", "4 balíčky",
                "5 balíčků", "6 balíčků", "7 balíčků", "8 balíčků",
                "9 balíčků", "10 balíčků", "11 balíčků", "12 balíčků"
            };
            public static string[] MERCHANT_WATER_VALUES =
            {
                "1 lahev", "2 lahve", "3 lahve", "4 lahve",
                "5 lahví", "6 lahví", "7 lahví", "8 lahví",
                "9 lahví", "10 lahví", "11 lahví", "12 lahví"
            };

            // Potions task constants
            public static readonly string POTIONS_TASK_BASE = "Pro $ATTRIBUTE$ pořiď $COLOR$ lektvar";
            public static string[] POTIONS_ATTRIBUTES =
            {
                "hbitost", "sílu", "důvtip"
            };
            public static string[] POTIONS_COLORS =
            {
                "zelený", "rudý", "modrý"
            };

            // Superpowers task constants
            public static readonly string SUPERPOWERS_TASK_BASE = "Posil se schopností ovládat";
            public static string[] SUPERPOWERS_VALUES =
            {
                "oheň", "vodu", "zemi"
            };

            // Weapons task constants
            public static readonly string WEAPONS_TASK_BASE = "Vyzbroj se na výpravu";
            public static string[] WEAPONS_VALUES =
            {
                "mečem", "prakem", "sekerou", "lukem", "palcátem", "nunčaky"
            };
        }

        public static class Nongamified
        {
            public static readonly string StreakNotification = "x v řadě!";
            public static readonly string CompleteSequenceNongamified = "Sjednej schůzku.";
            public static readonly string CompletedTasksCount = "počet sekvencí: ";

            // Task completed popup constants
            public static readonly string CorrectActionsInSequence = "Správné úkony v sekvenci";
            public static readonly string CorrectActionsStreak = "Úkony bez chyby v řadě";
            public static readonly string TaskTimeLeft = "Ušetřený čas v sekvenci";
            public static readonly string CurrentPerformance = "Aktuální výkon";
            public static readonly string PreviousAverage = "Průměr předchozích";
            public static readonly string PopupHeadline = "Zkontrolovali jste $ORDER$. sekvenci funkcí!\nDobrá práce!";

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
            public static readonly string TEAMMATES_TASK_BASE = "Přidej do týmu na schůzku";
            public static string[] TEAMMATES_VALUES =
            {
                "Evžena", "Jonáše", "Katku", "Lukáše",
                "Milenu", "Pavla", "Renatu", "Terezu",
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
