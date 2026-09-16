
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        var freezeStatus = new MultiStatus("Freeze", 1, new List<StatusPart> { new StatusPart(Stat.Movement, OperatorHandler.Multiply, 0f), new StatusPart(Stat.DamageModifier, OperatorHandler.Multiply, 0f) });
        var slowStatus = new SingleStatus("Slow", 1, Stat.Movement, OperatorHandler.Multiply, .5f);
        var silenceStatus = new SingleStatus("Silence", 2, Stat.DamageModifier, OperatorHandler.Multiply, 0f);

        var snowballThrow = new Ability("Snowball Throw", 15, 4, 1, AbilityType.Targeted, null) { Statuses = new List<IStatus> { slowStatus, freezeStatus }, };

        var flareShot = new Ability("Flare Shot", 15, 3, 1, AbilityType.Rigid, new List<AbilityEffect> { new AbilityEffect("ImpactShot", 15, 3, slowStatus), new AbilityEffect("Flare Spread", 20, 5, slowStatus) }) { Statuses = new List<IStatus> { slowStatus }, };

        //This is functionally equivalent to list.Add(flareshot)
        var princessAbilities = new List<Ability> { flareShot };
        var placeholderAbilities = new List<Ability>();


        var princessStatuses = new List<IStatus>() { slowStatus };
        var placeholderStatuses = new List<IStatus>();

        var princessUnit = new Unit("The Princess", 100, 5, 16, .05f, 6, princessAbilities, princessStatuses);
        var heroUnit = new Unit("The Hero", 140, 5, 10, .05f, 4, placeholderAbilities, placeholderStatuses);
        var saviorUnit = new Unit("The Savior", 70, 4, 12, .10f, 3, placeholderAbilities, placeholderStatuses);
        var mayorUnit = new Unit("The Mayor", 80, 4, 9, .05f, 5, placeholderAbilities, placeholderStatuses);
        var feederUnit = new Unit("The Feeder", 100, 4, 13, .05f, 6, placeholderAbilities, placeholderStatuses);
        var penguinUnit = new Unit("The Penguin", 105, 4, 7, .05f, 7, placeholderAbilities, placeholderStatuses);
        var manicUnit = new Unit("The Manic!", 70, 4, 6, .05f, 6, placeholderAbilities, placeholderStatuses);
        

        var unitList = new List<Unit>() { princessUnit, heroUnit, saviorUnit, mayorUnit, feederUnit, penguinUnit, manicUnit };

        var currentUnit = princessUnit;
        var targetedUnit = heroUnit;


        var damageCore = new Item("Damage Core", ItemRarity.Common, new List<ItemEffect> { new ItemEffect(1.1f, OperatorHandler.Multiply, Stat.DamageModifier) });
        var sacsPizza = new Item("Sacs Pizza", ItemRarity.Uncommon, new List<ItemEffect> { new ItemEffect(2f, OperatorHandler.Add, Stat.Energy) { TriggerType = TriggerType.OnEquip } });
        var sniperScope = new Item("Sniper Scope", ItemRarity.Uncommon, new List<ItemEffect> { new ItemEffect(.1f, OperatorHandler.Add, Stat.CritChance) });
        var selerity = new Item("Selerity", ItemRarity.Uncommon,new List<ItemEffect> { new ItemEffect(1f, OperatorHandler.Add, Stat.Movement) { TriggerType = TriggerType.OnEquip } });
        var gen1Mech = new Item("Generation 1 Mech", ItemRarity.Uncommon, new List<ItemEffect> { new ItemEffect((Convert.ToSingle(currentUnit.EffectiveSpeed) / 100), OperatorHandler.Add, Stat.CritChance) });
        var armorGames = new Item("ArmorGames", ItemRarity.Common, new List<ItemEffect> { new ItemEffect(.85f, OperatorHandler.Multiply, Stat.DamageReduction) });
        var cardinalOrnament = new Item("Cardinal Ornament", ItemRarity.Common, new List<ItemEffect> { new ItemEffect(1.15f, OperatorHandler.Multiply, Stat.DamageModifier) { TriggerCondition = ctx => ctx.AbilityUsed?.AbilityType == AbilityType.Rigid } });
        var amyr = new Item("Amyr", ItemRarity.Rare, new List<ItemEffect> { new ItemEffect(1.2f, OperatorHandler.Multiply, Stat.DamageModifier) { TriggerCondition = ctx => ctx.AbilityUsed?.AbilityType == AbilityType.Melee && ctx.Target?.EffectiveDamageReduction <= 1.0f }, new ItemEffect(1.5f, OperatorHandler.Multiply, Stat.DamageModifier) { TriggerCondition = ctx => ctx.Target?.EffectiveDamageReduction > 1.0f && ctx.AbilityUsed?.AbilityType == AbilityType.Melee } });  
        var gielinorCrest = new Item("GielinorCrest", ItemRarity.Uncommon, new List<ItemEffect> { new ItemEffect(currentUnit.CurrentHP / currentUnit.EffectiveMaxHP +.5f, OperatorHandler.Multiply, Stat.DamageModifier) });
        var crowbar = new Item("Crowbar", ItemRarity.Common, new List<ItemEffect> { new ItemEffect(2f, OperatorHandler.Multiply, Stat.DamageModifier) { TriggerCondition = ctx => ctx.Target.CurrentHP / ctx.Target.EffectiveMaxHP > 90f / 100f } });
        var maidenlessEdge = new Item("Maidenless Edge", ItemRarity.Common, new List<ItemEffect> { new ItemEffect(1.25f, OperatorHandler.Multiply, Stat.DamageModifier) { TriggerCondition = ctx => ctx.AbilityUsed?.AbilityType == AbilityType.Melee } });
        var puttPuttItem = new Item("How2Play PuttPutt Walkthrough HD", ItemRarity.Common, new List<ItemEffect> { new ItemEffect(1.15f, OperatorHandler.Multiply, Stat.DamageModifier) { TriggerCondition = ctx => ctx.AbilityUsed?.AbilityType == AbilityType.Targeted } });
        var radiantKnightWard = new Item("Radiant Knight Ward", ItemRarity.Rare, new List<ItemEffect> { new ItemEffect(1.2f, OperatorHandler.Multiply, Stat.DamageReduction), new ItemEffect(1f, OperatorHandler.Add, Stat.Speed) { TriggerType = TriggerType.OnLevelUp } });
        var berryHP = new Item("Berry that triggers when you get to low hp", ItemRarity.Uncommon, new List<ItemEffect> { new ItemEffect(2f, OperatorHandler.Add, Stat.Movement) { TriggerCondition = ctx => ctx.Source.CurrentHP / ctx.Source.EffectiveMaxHP < 15f / 100f }, new ItemEffect(1.5f, OperatorHandler.Multiply, Stat.Speed) { TriggerCondition = ctx => ctx.Source.CurrentHP / ctx.Source.EffectiveMaxHP < 15f / 100f }, new ItemEffect(1.2f, OperatorHandler.Multiply, Stat.DamageModifier) { TriggerCondition = ctx => ctx.Source.CurrentHP / ctx.Source.EffectiveMaxHP < 15f / 100f }, new ItemEffect(.15f, OperatorHandler.Add, Stat.CritChance) { TriggerCondition = ctx => ctx.Source.CurrentHP / ctx.Source.EffectiveMaxHP < 15f / 100f } });
        var highRoller = new Item("High Roller", ItemRarity.Common, new List<ItemEffect> { new ItemEffect(1f, OperatorHandler.Add, Stat.Energy) { TriggerType = TriggerType.OnCrit } });
        var hausRebuttal = new Item("Haus' Rebuttal", ItemRarity.Uncommon, new List<ItemEffect> { new ItemEffect(0f, OperatorHandler.Add, Stat.DamageModifier) { Status = slowStatus } });
        var smallAntVision = new Item("A Small Ant's Vision", ItemRarity.Mystical, new List<ItemEffect> { new ItemEffect(2.0f, OperatorHandler.Multiply, Stat.DamageModifier) });

        var listOfAllItems = new List<Item> { damageCore , sacsPizza , sniperScope , selerity , gen1Mech , armorGames , cardinalOrnament , amyr , gielinorCrest , crowbar , maidenlessEdge , puttPuttItem , radiantKnightWard , berryHP , highRoller , hausRebuttal , smallAntVision};

        var testUnequippedItemsList = new List<Item> { crowbar, berryHP };
        var currentUnitItemList = new List<Item> { damageCore, sacsPizza, sniperScope };
        currentUnit.Inventory = new Inventory(currentUnitItemList, testUnequippedItemsList ,currentUnit);

        var targetedUnitItemList = new List<Item> { armorGames };
        targetedUnit.Inventory = new Inventory(targetedUnitItemList, targetedUnit);

        princessUnit = currentUnit;
        heroUnit = targetedUnit;

        //var testRosterList allows for easier testing without having to create a new roster
        var testRosterList = new List<List<Unit>> { new List<Unit> { heroUnit, princessUnit, saviorUnit }, new List<Unit> { princessUnit, saviorUnit, feederUnit}, new List<Unit> { princessUnit, manicUnit, penguinUnit}, new List<Unit> { saviorUnit, mayorUnit, princessUnit} };
        //var rosterList = InitialRosterSelection(unitList);
        UnitAttack(testRosterList);
        //FullTurnRunthrough(rosterList);
        //OpenChest(listOfAllItems, rosterList);

    }

    
    public static void UpdateStatusEffect(Unit unit)
    {
        var concateStatus = "";
        for (int i = 0; i < unit.Statuses.Count; i++)
        {
            unit.Statuses[i].Duration -= 1;
            concateStatus += "(" + unit.Statuses[i].Name + ", " + unit.Statuses[i].Duration.ToString() + " turns)";
        }
        Console.WriteLine($"- {unit.Name} [{unit.CurrentHP}/{unit.EffectiveMaxHP}] {concateStatus} ");
    }

    public static void DamageCalculation(Unit attackingUnit, Unit defendingUnit, Ability abilityUsed)
    {
        var contextTrigger = new TriggerContext(attackingUnit, TriggerType.Combat) { Target = defendingUnit, AbilityUsed = abilityUsed };
        attackingUnit.CalculateEffectiveStats(contextTrigger);
        defendingUnit.CalculateEffectiveStats(contextTrigger);
        var attackingDamage = abilityUsed.Damage * attackingUnit.EffectiveDamageModifier;
        attackingDamage = attackingDamage * defendingUnit.EffectiveDamageReduction;
        var attackingDamageInt = (int)MathF.Round(attackingDamage);
        defendingUnit.CurrentHP -= attackingDamageInt;

        attackingUnit.TimesAttacked++; defendingUnit.TimesDefended++;
        var activeAttackingEffects = attackingUnit.Inventory.GetActiveItemEffects(contextTrigger);

        foreach (var itemEffect in activeAttackingEffects)
        {
            if (itemEffect.Status != null)
            {
                abilityUsed.Statuses?.Add(itemEffect.Status);
            }
        }
        foreach(var status in abilityUsed.Statuses)
        {
            defendingUnit.Statuses.Add(status);
        }
    }

    public static Unit UnitAttack(List<List<Unit>> listOfRosters)
    {
        var concatenatedListOfActiveUnits = ListConcatenation(listOfRosters);

        Console.WriteLine("Which unit is attacking?");
        DisplayFullRoster(listOfRosters);
        var attackingUnitInput = Console.ReadLine();
        var attackingUnit = concatenatedListOfActiveUnits[Int32.Parse(attackingUnitInput)];

        Console.WriteLine("Which unit is being hit?");
        DisplayFullRoster(listOfRosters);
        var defendingUnitInput = Console.ReadLine();
        var defendingUnit = concatenatedListOfActiveUnits[Int32.Parse(defendingUnitInput)];

        Console.WriteLine("What Ability is being used?");
        int optionInt = 0;
        foreach (var ability in attackingUnit.Abilities)
        {
            Console.WriteLine($" {optionInt}: {ability.AbilityName}");
            optionInt++;
        }
        var abilityUsedInput = Console.ReadLine();
        var abilityUsed = attackingUnit.Abilities[Int32.Parse(abilityUsedInput)];

        DamageCalculation(attackingUnit, defendingUnit, abilityUsed);

        return defendingUnit;
    }

    public static void ItemThrow(List<List<Unit>> listOfRosters)
    {
        var concatenatedListOfActiveUnits = ListConcatenation(listOfRosters);

        DisplayFullRoster(listOfRosters);
        Console.WriteLine("Which unit is throwing?");
        var throwingUnitInput = Console.ReadLine();
        var throwingUnit = concatenatedListOfActiveUnits[Int32.Parse(throwingUnitInput)];

        if (throwingUnit.Inventory.UnequippedItems.Count == 0)
        {
            Console.WriteLine("Error: No unequipped items to throw");
            return;
        }

        DisplayFullRoster(listOfRosters);
        Console.WriteLine("Which unit is being thrown to?");
        var throwntoUnitInput = Console.ReadLine();
        var throwntoUnit = concatenatedListOfActiveUnits[Int32.Parse(throwntoUnitInput)];
        


        Console.WriteLine("Which Item is being thrown?");
        var optionInt = 0;
        foreach(var item in throwingUnit.Inventory.UnequippedItems)
        {
            Console.WriteLine($" {optionInt}: {item.Name}");
            optionInt++;
        }
        var thrownItemInput = Console.ReadLine();
        var thrownItem = throwingUnit.Inventory.UnequippedItems[Int32.Parse(thrownItemInput)];


        throwntoUnit.Inventory.UnequippedItems.Add(thrownItem);
        throwingUnit.Inventory.UnequippedItems.Remove(thrownItem);

        foreach(var item in throwntoUnit.Inventory.UnequippedItems)
        {
            Console.WriteLine($"{throwntoUnit.Name}: {item.Name}");
        }
        foreach (var item in throwingUnit.Inventory.UnequippedItems)
        {
            Console.WriteLine($"{throwingUnit.Name}: {item.Name}");
        }
    }

    public static void ItemThrow(Unit throwingUnit, List<List<Unit>> listOfRosters)
    {   
        if (throwingUnit.Inventory.UnequippedItems.Count == 0) 
        {
            Console.WriteLine("Error: No unequipped items to throw");
            return; 
        }

        var concatenatedListOfActiveUnits = ListConcatenation(listOfRosters);

        DisplayFullRoster(listOfRosters);
        Console.WriteLine("\nWhich unit is being thrown to?");
        var throwntoUnitInput = Console.ReadLine();
        var throwntoUnit = concatenatedListOfActiveUnits[Int32.Parse(throwntoUnitInput)];

        Console.WriteLine("Which Item is being thrown?");
        var optionInt = 0;
        foreach (var item in throwingUnit.Inventory.UnequippedItems)
        {
            Console.WriteLine($" {optionInt}: {item.Name}");
            optionInt++;
        }
        var thrownItemInput = Console.ReadLine();
        var thrownItem = throwingUnit.Inventory.UnequippedItems[Int32.Parse(thrownItemInput)];


        throwntoUnit.Inventory.UnequippedItems.Add(thrownItem);
        throwingUnit.Inventory.UnequippedItems.Remove(thrownItem);

        foreach (var item in throwntoUnit.Inventory.UnequippedItems)
        {
            Console.WriteLine($"{throwntoUnit.Name}: {item.Name}");
        }
        foreach (var item in throwingUnit.Inventory.UnequippedItems)
        {
            Console.WriteLine($"{throwingUnit.Name}: {item.Name}");
        }
    }

    public static Item AddRandomItem(List<Item> listOfAllItems, Unit unit)
    {

        Random rnd = new Random();
        int itemIndex = rnd.Next(1, 101);
        ItemRarity itemRarity = ItemRarity.Placeholder;
        if (itemIndex <= 45) { itemRarity = ItemRarity.Common; }
        if (itemIndex > 45 && itemIndex <= 80) { itemRarity = ItemRarity.Uncommon; }
        if (itemIndex > 80 && itemIndex <= 94) { itemRarity = ItemRarity.Rare; }
        if (itemIndex > 94) { itemRarity = ItemRarity.Mystical; }
        var itemPool = new List<Item>();

        foreach (var item in listOfAllItems)
        {
            if (item.Rarity == itemRarity)
            {
                itemPool.Add(item);
            }
        }
        var itemID = rnd.Next(0, itemPool.Count);

        var chestItem = itemPool[itemID];
        unit.Inventory.UnequippedItems.Add(chestItem);
        return chestItem;
    }

    public static void OpenChest(List<Item> listOfAllItems, List<List<Unit>> listOfRosters)
    {
        var concatenatedListOfActiveUnits = ListConcatenation(listOfRosters);

        DisplayFullRoster(listOfRosters);
        Console.WriteLine("\nWhich unit is opening a Chest?");
        var chestUnitInput = Console.ReadLine();
        var chestUnit = concatenatedListOfActiveUnits[Int32.Parse(chestUnitInput)];
        var chestItem = AddRandomItem(listOfAllItems, chestUnit);
        Console.WriteLine($"\n\n'{chestItem.Name}' was added to {chestUnit}'s Inventory\n");

        DisplayInfo.CheckInventory(chestUnit);
    }

    public static void EquipItem(List<Unit> unitList)
    {
        Console.WriteLine("\nWhich unit is Equipping an Item?\n");
        var optionInt = 0;
        foreach (var unit in unitList)
        {
            Console.WriteLine($" {optionInt}: {unit.Name}");
            optionInt++;
        }
        var equippingUnitInput = Console.ReadLine();
        var equippingUnit = unitList[Int32.Parse(equippingUnitInput)];

        while (equippingUnit.Inventory.EquippedItems.Count >= 4)
        {
            Console.WriteLine("Too many Items: Please Unequip Item of your choice");
            UnEquipItem(equippingUnit);
        }

        Console.WriteLine($"\nWhich Item is {equippingUnit.Name} Equipping?\n");
        optionInt = 0;
        foreach(var item in equippingUnit.Inventory.UnequippedItems)
        {
            Console.WriteLine($" {optionInt}: {item.Name}");
            optionInt++;
        }

        var itemToEquipInput = Console.ReadLine();
        var itemToEquip = equippingUnit.Inventory.UnequippedItems[Int32.Parse(itemToEquipInput)];
        equippingUnit.Inventory.UnequippedItems.Remove(itemToEquip);
        equippingUnit.Inventory.EquippedItems.Add(itemToEquip);


        Console.WriteLine($"\n\n'{itemToEquip.Name}' was Equipped\n");
        foreach (var item in equippingUnit.Inventory.UnequippedItems)
        {
            Console.WriteLine($"unequipped Item: {item.Name}");
        }
        foreach (var item in equippingUnit.Inventory.EquippedItems)
        {
            Console.WriteLine($"equipped Item: {item.Name}");
        }

    }

    public static void UnEquipItem(Unit unit)
    {
        Console.WriteLine("Which Item is being Unequipped?");
        var optionInt = 0;
        foreach(var item in unit.Inventory.EquippedItems)
        {
            Console.WriteLine($" {optionInt}: {item.Name}");
            optionInt++;
        }

        var itemToUnEquipInput = Console.ReadLine();
        var itemToUnEquip = unit.Inventory.EquippedItems[Int32.Parse(itemToUnEquipInput)];
        unit.Inventory.EquippedItems.Remove(itemToUnEquip);
        unit.Inventory.UnequippedItems.Add (itemToUnEquip);
    }

    public static void DisplayTurnOrder(List<Unit> unitList)
    {
        List<Unit> turnOrderList = unitList.OrderByDescending(unit=>unit.EffectiveSpeed).ToList();

        Console.WriteLine("\n\nTurn Order: \n\n");
        
        foreach (var unit in turnOrderList)
        {
            var unitSumText = DisplayInfo.CheckUnitSummary(unit);
            Console.WriteLine($" Spd: {unit.EffectiveSpeed} {unitSumText}");
        }
    }

    public static List<List<Unit>> InitialRosterSelection(List<Unit> unitList)
    {
        Console.WriteLine("Welcome to Project: Knockoffs! Thank you for loading in. How many characters will be on each person's lineup?");
        var rosterSizeInput = Console.ReadLine();
        var rosterSize = Int32.Parse(rosterSizeInput);

        var playerLists = new List<List<Unit>>();
        var playerCount = 4;

        for (int p = 0; p < playerCount; p++)
        {
            playerLists.Add(new List<Unit>());

            Console.WriteLine($"\n\n Player {p + 1}: Your turn to select your roster: \n");

            for (int i = 0; i < rosterSize; i++)
            {
                Console.WriteLine($"Player {p + 1}: Select Unit #{i + 1} ");
                var optionInt = 0;
                foreach (var unit in unitList)
                {
                    Console.WriteLine($"{optionInt}: {unit.Name}");
                    optionInt++;
                }
                var selectedUnitInput = Console.ReadLine();
                var selectedUnit = Int32.Parse(selectedUnitInput);
                playerLists[p].Add(unitList[selectedUnit]);
                Console.WriteLine($"\n{unitList[selectedUnit].Name} was added to Player {p + 1}'s Roster. {rosterSize - i - 1} left\n");
            }
            Console.WriteLine($"\nPlayer {p + 1}'s Roster: \n");
            foreach (var unit in playerLists[p])
            {
                Console.WriteLine($" {unit.Name}");
            }
        }
        return playerLists;
    }

    public static List<Unit> ListConcatenation(List<List<Unit>> listOfRosters)
    {
        var concatenatedListOfAllActiveUnits = new List<Unit>();

        foreach (var roster in listOfRosters)
        {
            foreach (var unit in roster)
            {
                concatenatedListOfAllActiveUnits.Add(unit);
            }
        }
        return concatenatedListOfAllActiveUnits;
    }

    public static void DisplayFullRoster(List<List<Unit>> listOfRosters)
    {
        var optionInt = 0;
        var rosterCount = 0;

        foreach (var roster in listOfRosters)
        {
            Console.WriteLine($"\nRoster {rosterCount + 1}: \n");
            foreach (var unit in roster)
            {
                Console.WriteLine($"{optionInt}: {unit.Name} ");
                optionInt++;
            }
            rosterCount++;
        }
    }

    public static void FullTurnRunthrough(List<List<Unit>> listOfRosters)
    {
        var concatenatedListOfUnits = ListConcatenation(listOfRosters);
        DisplayFullRoster(listOfRosters);
        Console.WriteLine("\nWhich Unit's turn is next?\n");

        var selectedUnitInput = Console.ReadLine();
        var selectedUnit = concatenatedListOfUnits[Int32.Parse(selectedUnitInput)];
        

        Console.WriteLine($"Is {selectedUnit.Name} throwing an item? (y/n)");
        var isUnitThrowingInput = Console.ReadLine();
        if (isUnitThrowingInput == "y") { ItemThrow(selectedUnit, listOfRosters); }
        
    }

}