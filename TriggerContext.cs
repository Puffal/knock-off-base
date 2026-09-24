using System;

public class TriggerContext	
{
	public Unit Source { get; private set; }
	public Unit Target { get; private set; }
	public Ability? AbilityUsed;
	public TriggerType TriggerType { get; private set; }

	public TriggerContext(Unit source, Unit target, TriggerType triggerType)
    {
        Source = source;
		Target = target;
        TriggerType = triggerType;
    }
	
	public  TriggerContext(Unit source, TriggerType triggerType) 
	{
		Source = source;
		Target = source;
		TriggerType = triggerType;

	}

}
