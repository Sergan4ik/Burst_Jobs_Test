using System.Collections.Generic;
using ZergRush.CodeGen;

[GenTask(GenTaskFlags.PolymorphicDataPack), GenInLocalFolder]
public class Unit
{
    public UnitConfig config;
    
    public int id;
    public int teamId;
    
    public StatContainer statsRuntime = new StatContainer();

    public void Init(GameModel model)
    {
        for (var i = 0; i < config.statsConfig.data.Count; i++)
        {
            var stat = config.statsConfig.data[i];
            statsRuntime.data[i] = stat;
        }
    }
}

[GenTask(GenTaskFlags.SimpleDataPack), GenInLocalFolder]
public class UnitConfig
{
    public List<AbilityInstance> abilities;
    public StatContainer statsConfig;

    public static UnitConfig GetDefault()
    {
        UnitConfig cfg = new UnitConfig()
        {
            abilities = new List<AbilityInstance>(),
            statsConfig = new StatContainer()
        };
        cfg.statsConfig[StatType.Health] = new Stat { value = 100, maxValue = 100, minValue = 0 };
        cfg.statsConfig[StatType.Mana] = new Stat { value = 100, maxValue = 100, minValue = 0 };
        cfg.statsConfig[StatType.Attack] = new Stat { value = 10, maxValue = 100, minValue = 0 };
        cfg.statsConfig[StatType.Defense] = new Stat { value = 0, maxValue = 10, minValue = 0 };

        return cfg;
    }
}