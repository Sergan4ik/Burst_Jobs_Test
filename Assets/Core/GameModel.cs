using System.Collections.Generic;
using Unity.Mathematics;
using ZergRush.CodeGen;

[GenTask(GenTaskFlags.PolymorphicDataPack), GenInLocalFolder]
public class GameModel
{
    private int __idCounter = 0;
    
    public Random modelRandom;
    public List<Unit> units;

    public void Init()
    {
        modelRandom = new Random(1);
    }

    public Unit CreateUnit(UnitConfig cfg)
    {
        Unit u = new Unit()
        {
            id = __idCounter,
            config = cfg
        };
        __idCounter++;
        
        u.Init(this);
        return u;
    }

    public void Tick(float dt)
    {
        TickAbilities(dt);
    }

    private void TickAbilities(float dt)
    {
        foreach (var unit in units)
        {
            foreach (var ability in unit.config.abilities)
            {
                ability.Tick(this, unit, dt);
            }
        }
    }
}