using System;

public class Inventory
{
	public List<Item> EquippedItems { get; set; }
    public List<Item> UnequippedItems { get; set; } = new List<Item>();
    public Unit Unit { get; set; }

    public Inventory(Unit unit)
    {
        EquippedItems = new List<Item>();
        UnequippedItems = new List<Item>();
        Unit = unit;

    }
    public Inventory(List<Item> items, Unit unit)
    {
        EquippedItems = items;
        Unit = unit;
    }
    public Inventory(List<Item> items, List<Item> unequippedItems, Unit unit)
    {
        EquippedItems = items;
        UnequippedItems = unequippedItems;
        Unit = unit;
    }

    public List<ItemEffect> GetActiveItemEffects(TriggerContext triggerContext)
    {
        var itemEffectList = new List<ItemEffect>();
        foreach (var item in EquippedItems)
        {
            foreach (var effect in item.ListOfEffects)
            {
                if (effect.TriggerCondition(triggerContext) && effect.TriggerType == triggerContext.TriggerType)
                {
                    itemEffectList.Add(effect);
                }
            }
        }
        return itemEffectList;
    }
}
