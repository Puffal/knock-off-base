using System;

public class TargetedStatus: IStatus
{
    public string Name { get; set; }
    public int Duration { get; set; }
    public Unit UnitOfInterest { get; set; }

    public TargetedStatus(string name, int duration )
    {
        Name = name;
        Duration = duration;
    }

    public List<StatusPart> GetActiveStatusParts() => new List<StatusPart>();

}
