using System;
using System.Diagnostics;

public class InstantiateUnit
{
    public static Unit CreatePrincess()
    {
        var flareShot = new Ability("Flare Shot", 15, 3, 1, AbilityType.Rigid, new List<IStatus>() { StatusEffect.SlowStatus });
        var flareSpread = new Ability("Flare Spread", 20, 5, 1, AbilityType.Rigid, new List<IStatus>() { StatusEffect.SlowStatus });
        var firemorph = new Ability("Firemorph", 0, 3, 1, AbilityType.Targeted, new List<IStatus>() { StatusEffect.VulnerableStatus, StatusEffect.SilenceStatus});
        var flameWall = new Ability("Flame Wall", 5, 6, 1, AbilityType.Flexible, new List<IStatus>() { StatusEffect.SlowStatus });
        //Needs:
        // -way to keep track of duration
        // -way to proc when it activates 10 damage + burn for entering wall
        var princessCrush = new Ability("Princess Crush", 30, 2, 1, AbilityType.RigidMovelinked, new List<IStatus> { StatusEffect.SlowStatus });
        //Needs:
        // -to keep track of duration
        // -check if enemy has burn to activate special burn explosion
        return new Unit("The Princess", 100, 5, 16, .05f, 6, new List<Ability> { flareShot, flareSpread, firemorph }, new List<IStatus>());
    }

    public static Unit CreatePenguin()
    {
        var snowballThrow = new Ability("Snowball Throw", 15, 4, 1, AbilityType.Targeted, new List<IStatus> { StatusEffect.SlowStatus, StatusEffect.FreezeStatus });
        //Needs:
        // -probability check for status effects

        return new Unit("The Penguin", 105, 4, 7, .05f, 7, new List<Ability> { snowballThrow }, new List<IStatus>());
    }

    public static Unit CreateHero()
    {
        var axeSwing = new Ability("Axe Swing", 20, 1, 1, AbilityType.Melee, new List<IStatus>());
        //Needs:
        // -add rage status
        var waveBuster = new Ability("Wave Buster", 12, 3, 1, AbilityType.Targeted, new List<IStatus>());
        var tsunamiBuster = new Ability("Tsunami Buster", 20, 5, 1, AbilityType.Targeted, new List<IStatus>());
        //Needs:
        // -add rage status
        var sussyIdea = new Ability("Sussy Idea", 15, 4, 1, AbilityType.TargetedMovelinked, new List<IStatus>());
        var dualBuster = new Ability("Dual Buster", 25, 5, 2, AbilityType.Targeted, new List<IStatus>());
        // Needs:
        // -add Parry Status to self

        return new Unit("The Hero", 140, 5, 10, .05f, 4, new List<Ability> { waveBuster, sussyIdea }, new List<IStatus>());
    }

    public static Unit CreateSavior()
    {
        var light = new Ability("Light", 12, 2, 2, AbilityType.Targeted, new List<IStatus>());
        //Needs:
        // -Way to change multihits if unit is faster than others
        //var sacrifice = new Ability("Sacrifice, ")
        //NEEDS A LOT TO IMPLEMENT IN CURRENT STATE
        var summonAllyRogue = new Ability("Summon Ally (Rogue Chosen Randomly)", 14, 1, 2, AbilityType.Melee, new List<IStatus>());
        //Needs:
        // -Way to distinguish whether status is applied to unit attacking or unit defending
        // -apply stealing item (circle back once probability is implemented)
        // -implement probability
        var summonAllyHope = new Ability("Summon Ally (Hope Chosen Randomly)", 22, 1, 2, AbilityType.Melee, new List<IStatus>());
        //Needs:
        // -Way to distinguish whether status is applied to unit attacking or unit defending
        // -implement probability
        var teneuVengeance = new Ability("Teneu's Vengeance", 30, 4, 1, AbilityType.Targeted, new List<IStatus>());
        //Needs:
        // -increase damage based off of allies remaining

        return new Unit("The Savior", 70, 4, 12, .10f, 3, new List<Ability> { }, new List<IStatus>());
    }

    public static Unit CreateMayor()
    {
       
        return new Unit("The Mayor", 80, 4, 9, .05f, 5, new List<Ability> { }, new List<IStatus>());
    }

    public static Unit CreateFeeder()
    {
        var spearStab = new Ability("Spear Stab", 14, 1, 1, AbilityType.Melee, new List<IStatus>());
        var shakeAndBake = new Ability("Shake And Bake", 11, 2, 2, AbilityType.Melee, new List<IStatus>());
        var riftAlign = new Ability("Rift Align", 5, 1, 1, AbilityType.Rigid, new List<IStatus>());

        return new Unit("The Feeder", 100, 4, 13, .05f, 6, new List<Ability> { spearStab, shakeAndBake, riftAlign }, new List<IStatus>());
    }

    public static Unit CreateManic()
    {
        var zaplingTap = new Ability("Zapling Tap!", 8, 3, 1, AbilityType.Targeted, new List<IStatus>());
        //Needs:
        // -increase of speed when attack is used
        // -increase of multihit whenever speed is increased
        var shockets = new Ability("Shockets!!", 15, 5, 1, AbilityType.Rigid, new List<IStatus>() { StatusEffect.SlowStatus});
        //Needs:
        // -need way to increase damage if used in succession (attack chains onto nearby enemies)
        var shockticSpikes = new Ability("Shocktic Spikes", 5, 3, 1, AbilityType.Flexible, new List<IStatus>() { StatusEffect.RootStatus });
        var nimbus = new Ability("Nimbus", 30, 99, 1, AbilityType.Targeted, new List<IStatus>());
        //Needs:
        // -increase in damage based off of how far enemy is, needs an input from calculator Operator (me)

        return new Unit("The Manic!", 70, 4, 6, .05f, 6, new List<Ability> { shockticSpikes}, new List<IStatus>());
    }

    public static Unit CreateBadOmen()
    {
        var classic = new Ability("A Classic Pistol", 4, 2, 3, AbilityType.Targeted, new List<IStatus>());
        var smoke = new Ability("A Sphere of Smoke", 0, 99, 1, AbilityType.Targeted, new List<IStatus>());
        //Needs:
        // -ability to check if ranged attack succeeds
        var rifle = new Ability("A Vandal's Rifle", 7, 4, 4, AbilityType.Rigid, new List<IStatus>());
        //Needs:
        // -ability to alter crit damage
        var blackop = new Ability("A Black Operation", 40, 10, 1, AbilityType.Rigid, new List<IStatus>());
        //Needs:
        //-alter damage when hits another foe
        //-set SniperScope item to active when this is in use

        return new Unit("The Bad Omen", 80, 4, 6, .05f, 6, new List<Ability> { classic }, new List<IStatus>());
    }

}
