using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameTask
{
    public List<int> MultipleSelectValues { get; set; } = new List<int>();
    public int SingleSelectValue { get; set; }
    public bool SwitchValue { get; set; }
    public HashSet<int> TeammatesValues { get; set; } = new HashSet<int>();

    public GameTask()
    {
        for (int i = 1; i <= Constants.MULTIPLE_SELECT_VALUES_COUNT; i++)
        {
            if (Constants.RANDOM.Next() % 3 == 0) continue;

            MultipleSelectValues.Add(i);
        }

        for (int i = 1; i <= Constants.TeammateResources.Length; i++)
        {
            if (Constants.RANDOM.Next() % 2 == 0) continue;

            TeammatesValues.Add(i);
        }

        SingleSelectValue = Constants.RANDOM.Next(1, 3 + 1);

        SwitchValue = true;
    }
}