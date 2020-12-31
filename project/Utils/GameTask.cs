using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public class GameTask
    {
        public HashSet<int> Advertisement { get; private set; } = new HashSet<int>();
        public (int, int) Calendar { get; private set; } = (-1, -1);
        public HashSet<int> Devices { get; private set; } = new HashSet<int>();
        public bool Position { get; private set; } = false;
        public int Priority { get; private set; } = -1;
        public int Rating { get; private set; } = -1;
        public HashSet<int> Teammates { get; private set; } = new HashSet<int>();
        public bool Theme { get; private set; } = false;
        public (int, int) Time { get; private set; } = (-1, -1);
        public int Topics { get; private set; } = -1;
        public int Volume { get; private set; } = 0;

        public List<string> TaskAssignments;

        private GameType _gameType;
        private readonly int _randomRollMax = 100;
        private readonly int _maxAssignments = 5;
        private Random _random;
        private List<Action> _generators;

        public GameTask(GameType gameType)
        {
            _gameType = gameType;
            TaskAssignments = new List<string>();
            _random = new Random();
            _generators = new List<Action>
            {
                GenerateAdvertisementAssignment,
                GenerateCalendarAssignment,
                GenerateDevicesAssignment,
                GeneratePositionAssignment,
                GeneratePriorityAssignment,
                GenerateRatingAssignment,
                GenerateTeammatesAssignment,
                GenerateThemeAssignment,
                GenerateTimeAssignment,
                GenerateTopicsAssignment,
                GenerateVolumeAssignment,
            };

            _generators.Shuffle();

            for (int i = 0; i < _maxAssignments; i++)
            {
                _generators[i].Invoke();
            }

            GenerateTaskAssignmentTexts();
        }

        private void GenerateAdvertisementAssignment()
        {
            for (int i = 0; i < Constants.ADVERTISEMENT_NUMBER_OF_ITEMS; i++)
            {
                if (_random.Next(_randomRollMax) % 3 == 0)
                {
                    Advertisement.Add(i);
                }
            }

            if (Advertisement.Count == 0)
            {
                Advertisement.Add(_random.Next(Constants.ADVERTISEMENT_NUMBER_OF_ITEMS));
            }
        }

        private void GenerateCalendarAssignment()
        {
            var month = _random.Next(Constants.CALENDAR_VALUES_PER_LIST.Length);
            var day = _random.Next(Constants.CALENDAR_VALUES_PER_LIST[month]);

            Calendar = (month, day);
        }

        private void GenerateDevicesAssignment()
        {
            for (int i = 0; i < Constants.DEVICES_NUMBER_OF_ITEMS; i++)
            {
                if (_random.Next(_randomRollMax) % 3 == 0)
                {
                    Devices.Add(i);
                }
            }

            if (Devices.Count == 0)
            {
                Devices.Add(_random.Next(Constants.DEVICES_NUMBER_OF_ITEMS));
            }
        }

        private void GeneratePositionAssignment()
        {
            Position = true;
        }

        private void GeneratePriorityAssignment()
        {
            Priority = _random.Next(Constants.PRIORITY_NUMBER_OF_ITEMS);
        }

        private void GenerateRatingAssignment()
        {
            Rating = _random.Next(Constants.RATING_NUMBER_OF_ITEMS);
        }

        private void GenerateTeammatesAssignment()
        {
            for (int i = 0; i < Constants.TEAMMATES_NUMBER_OF_ITEMS; i++)
            {
                if (Teammates.Count < Constants.TEAMMATES_ADDED_COUNT && _random.Next(_randomRollMax) % 3 == 0)
                {
                    Teammates.Add(i);
                }
            }

            if (Teammates.Count == 0)
            {
                Teammates.Add(_random.Next(Constants.TEAMMATES_NUMBER_OF_ITEMS));
            }
        }

        private void GenerateThemeAssignment()
        {
            Theme = true;
        }

        private void GenerateTimeAssignment()
        {
            Time = (_random.Next(Constants.TIME_OPTIONS_COUNT), _random.Next(Constants.TIME_OPTIONS_COUNT));
        }

        private void GenerateTopicsAssignment()
        {
            Topics = _random.Next(Constants.TOPICS_OPTIONS_COUNT);
        }

        private void GenerateVolumeAssignment()
        {
            Volume = _random.Next(1, Constants.VOLUME_OPTIONS_COUNT);
        }

        private void GenerateTaskAssignmentTexts()
        {
            switch (_gameType)
            {
                case GameType.Nongamified:
                    GenerateTaskAssignmentsNongamified();
                    return;
                default:
                    throw new ArgumentOutOfRangeException("Specified game type does not exist.");
            }
        }

        private void GenerateTaskAssignmentsNongamified()
        {
            if (Advertisement.Count > 0)
            {
                var valuesJoined = GetMultipleValuesJoined(Advertisement, ResourceStrings.Nongamified.ADVERTISEMENT_VALUES);
                TaskAssignments.Add($"{ResourceStrings.Nongamified.ADVERTISEMENT_TASK_BASE} {valuesJoined}.");
            }

            if (Calendar != (-1, -1))
            {
                TaskAssignments.Add($"{ResourceStrings.Nongamified.CALENDAR_TASK_BASE} {Calendar.Item2 + 1}. {ResourceStrings.Nongamified.CALENDAR_VALUES[Calendar.Item1]}.");
            }

            if (Devices.Count > 0)
            {
                var valuesJoined = GetMultipleValuesJoined(Devices, ResourceStrings.Nongamified.DEVICES_VALUES);
                TaskAssignments.Add($"{ResourceStrings.Nongamified.DEVICES_TASK_BASE} {valuesJoined}.");
            }

            if (Position)
            {
                TaskAssignments.Add($"{ResourceStrings.Nongamified.POSITION_VALUE}.");
            }

            if (Priority != -1)
            {
                TaskAssignments.Add($"{ResourceStrings.Nongamified.PRIORITY_TASK_BASE} {ResourceStrings.Nongamified.PRIORITY_VALUES[Priority]}.");
            }

            if (Rating != -1)
            {
                TaskAssignments.Add($"{ResourceStrings.Nongamified.RATING_TASK_BASE} {ResourceStrings.Nongamified.RATING_VALUES[Rating]}.");
            }

            if (Teammates.Count > 0)
            {
                var valuesJoined = GetMultipleValuesJoined(Teammates, ResourceStrings.Nongamified.TEAMMATES_VALUES);
                TaskAssignments.Add($"{ResourceStrings.Nongamified.TEAMMATES_TASK_BASE} {valuesJoined}.");
            }

            if (Theme)
            {
                TaskAssignments.Add($"{ResourceStrings.Nongamified.THEME_VALUE}.");
            }

            if (Time != (-1, -1))
            {
                TaskAssignments.Add($"{ResourceStrings.Nongamified.TIME_TASK_BASE} {ResourceStrings.Nongamified.TIME_HOUR_VALUES[Time.Item1]} {ResourceStrings.Nongamified.TIME_MINUTE_VALUES[Time.Item2]}.");
            }

            if (Topics != -1)
            {
                TaskAssignments.Add($"{ResourceStrings.Nongamified.TOPICS_TASK_BASE} {ResourceStrings.Nongamified.TOPICS_VALUES[Topics]}.");
            }

            if (Volume != 0)
            {
                TaskAssignments.Add($"{ResourceStrings.Nongamified.VOLUME_TASK_BASE} {ResourceStrings.Nongamified.VOLUME_VALUES[Volume]}.");
            }
        }

        private string GetMultipleValuesJoined(IEnumerable<int> selectedValues, IList<string> resourceStrings)
        {
            var values = new List<string>();
            foreach (var item in selectedValues)
            {
                values.Add(resourceStrings[item]);
            }
            values.Shuffle();
            return values.Aggregate((a, b) => $"{a}, {b}");
        }
    }
}