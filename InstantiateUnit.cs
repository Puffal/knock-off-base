using System;
using System.Diagnostics;

public class InstantiateUnit
{
	public static Unit CreatePrincess()
	{
        var flareShot = new Ability("Flare Shot", 15, 3, 1, AbilityType.Rigid, new List<AbilityEffect> { new AbilityEffect("ImpactShot", 15, 3, StatusEffect.SlowStatus), new AbilityEffect("Flare Spread", 20, 5, StatusEffect.SlowStatus) }) { Statuses = new List<IStatus> { StatusEffect.SlowStatus }, };

    }
}
