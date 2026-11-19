using System;

public class StatusPart
{
    public int Duration { get; set; }
    public Stat StatModified { get; set; } = Stat.None;
    public OperatorHandler OperatorSign { get; set; } = OperatorHandler.Add;
    public float ModificationNumber { get; set; } = 0;

    public StatusPart(int duration, Stat statModified, OperatorHandler operatorSign, float modificationNumber)
	{
        Duration = duration;
		StatModified = statModified;
        OperatorSign = operatorSign;
        ModificationNumber = modificationNumber;
	}
}
