using System.Runtime.InteropServices;
using Core;
using Unity.Burst;
using Unity.Mathematics;
using ZergRush.CodeGen;

[GenTask(GenTaskFlags.PolymorphicDataPack), GenInLocalFolder]
public class AbilityInstance
{
    public Random random;
    public float cooldownTimer;
    public float cooldown;
    public AbilityAttitude attitude;

    
    public bool isReady => cooldownTimer >= cooldown;
    
    public void Tick(GameModel model, Unit owner, float dt)
    {
        if (cooldownTimer < cooldown)
        {
            cooldownTimer += dt;
        }
        
        TickAbilityInternal(model, owner, dt);
    }
    
    protected virtual void TickAbilityInternal(GameModel model, Unit owner, float dt) { }

    public virtual void UseAbility(GameModel model, Unit owner, Unit unit)
    {
        if (isReady && CanBeTarget(model, owner, unit))
        {
            cooldownTimer = 0;
        }
    }
    
    public virtual bool CanBeTarget(GameModel model, Unit owner, Unit unit)
    {
        bool attitudeOk = (attitude == AbilityAttitude.Aggressive && unit.IsAlly(owner))
                          || (attitude == AbilityAttitude.Support && unit.IsEnemy(owner));
        
        return attitudeOk;
    }
}

public static class AbilityTicks
{
    public static FunctionPointer<AbilityTicks.TickAbilityDelegate> fPointer =
            BurstCompiler.CompileFunctionPointer<AbilityTicks.TickAbilityDelegate>(AbilityTicks.TickDefaultAttack);
    
    public delegate void TickAbilityDelegate(AbilityInstance abilityInstance, GameModel model, Unit unit, float dt);
    
    [BurstCompile]
    public static void TickDefaultAttack(AbilityInstance abilityInstance, GameModel model, Unit unit, float dt)
    {
        
    }
    
    [BurstCompile]
    public static void TickDOT(AbilityInstance abilityInstance, GameModel model, Unit unit, float dt)
    {
        
    }
}

public enum AbilityAttitude
{
    Aggressive,
    Support,
}

public struct Ability1
{
    private int damage;
    private short range;
}

public struct Ability2
{
    private float dps;
    private short range;
}

[StructLayout(LayoutKind.Explicit)]
public struct CustomAbilityData
{
    [FieldOffset(0)] public Ability1 Ability1;
    [FieldOffset(0)] public Ability2 Ability2;
}