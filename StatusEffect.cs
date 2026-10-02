using System;

public class StatusEffect
{

	public static MultiStatus FreezeStatus { get; private set;} = new MultiStatus("Freeze", 1, new List<StatusPart> { new StatusPart(Stat.Movement, OperatorHandler.Multiply, 0f), new StatusPart(Stat.DamageModifier, OperatorHandler.Multiply, 0f) });
	public static SingleStatus SlowStatus { get; private set; } = new SingleStatus("Slow", 1, Stat.Movement, OperatorHandler.Multiply, .5f);
    public static SingleStatus SilenceStatus { get; private set; } = new SingleStatus("Silence", 1, Stat.DamageModifier, OperatorHandler.Multiply, 0f);
	public static SingleStatus VulnerableStatus { get; private set; } = new SingleStatus("Vulnerable", 1, Stat.DamageReduction, OperatorHandler.Multiply, 0.5f);
	public static SingleStatus RootStatus { get; private set; } = new SingleStatus("Rooted", 1, Stat.Movement, OperatorHandler.Multiply, 0f);


    public StatusEffect()
	{
	}
}
