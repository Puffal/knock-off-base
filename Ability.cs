using System;

public class Ability
{
	public string AbilityName { get; set; }
    public int Damage { get; set; }
    public int Range { get; set; }
    public int Multihits { get; set; }
    public AbilityType AbilityType { get; set; }
	public List<IStatus> Statuses { get; set; } = new List<IStatus>();

	

	public Ability(string abilityName, int damage, int range, int multihits, AbilityType abilityType, List<IStatus> statuses )
	{
		AbilityName = abilityName;
		Damage = damage;
		Range = range;
		Multihits = multihits;
		AbilityType = abilityType;
		Statuses = statuses;
		
	}
}
