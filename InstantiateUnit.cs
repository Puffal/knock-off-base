using System;
using System.Diagnostics;

public class InstantiateUnit
{
	public static Unit CreatePrincess()
	{
		var flareShot = new Ability("Flare Shot", 15, 3, 1, AbilityType.Rigid, new List<IStatus>() { StatusEffect.SlowStatus });
		var flareSpread = new Ability("Flare Spread", 20, 5, 1, AbilityType.Rigid, new List<IStatus>() { StatusEffect.SlowStatus });
		return new Unit("The Princess", 100, 5, 16, .05f, 6, new List<Ability> { flareShot, flareSpread }, new List<IStatus>());
	}

	public static Unit CreatePenguin()
	{
        var snowballThrow = new Ability("Snowball Throw", 15, 4, 1, AbilityType.Targeted, new List<IStatus> { StatusEffect.SlowStatus, StatusEffect.FreezeStatus });
		return new Unit("The Penguin", 105, 4, 7, .05f, 7, new List<Ability> { snowballThrow }, new List<IStatus>());
    }
}
