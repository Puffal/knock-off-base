using System;

public class SingleStatus: IStatus
{
	public string Name { get; set; }
	public int Duration { get; set; }
	public Stat StatModified { get; set; } = Stat.None;
	public OperatorHandler OperatorSign { get; set; } = OperatorHandler.Add;
	public float ModificationNumber { get; set; } = 0;

	public SingleStatus(string name, int duration, Stat statModified, OperatorHandler operatorSign, float modificationNumber)
	{
		Name = name;
		Duration = duration;
		StatModified = statModified;
		OperatorSign = operatorSign;
		ModificationNumber = modificationNumber;
	}

	public List<StatusPart> GetActiveStatusParts() => new List<StatusPart>() { new StatusPart(Duration, StatModified, OperatorSign, ModificationNumber) };
	



}
