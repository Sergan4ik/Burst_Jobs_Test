using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZergRush.CodeGen;

public enum StatType
{
    Health,
    Mana,
    Attack,
    Defense,
    MAX
}

[GenTask(GenTaskFlags.SimpleDataPack), GenInLocalFolder]
public class StatContainer
{
    public List<Stat> data;

    public Stat dummyStat => data.Last();

    public StatContainer()
    {
        data = new List<Stat>();
        for (int i = 0; i <= (int)StatType.MAX; i++)
        {
            data.Add(new Stat());
        }
    }

    public Stat this[StatType key]
    {
        get => GetRef(key);
        set => SetValue(key, value);
    }

    public Stat GetRef(StatType key)
    {
        if (key < 0 || key >= StatType.MAX)
        {
            Debug.LogError("Invalid StatType: " + key);
            return dummyStat;
        }

        return data[(int)key];
    }

    public void SetValue(StatType key, Stat value)
    {
        if (key < 0 || key >= StatType.MAX)
        {
            Debug.LogError("Invalid StatType: " + key);
            return;
        }

        data[(int)key] = value;
    }
}

[GenTask(GenTaskFlags.SimpleDataPack), GenInLocalFolder]
public struct Stat
{
    public int value;
    public int maxValue;
    public int minValue;
}