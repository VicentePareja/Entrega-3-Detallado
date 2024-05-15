using Fire_Emblem_View;
namespace Fire_Emblem;

public class SimulatedAttack
{
    public Character Attacker { get; private set; }
    public Character Defender { get; private set; }

    public SimulatedAttack(Character attacker, Character defender)
    {
        Attacker = attacker;
        Defender = defender;
    }
    
    public void PerformAttack(string advantage)
    {
        double weaponTriangleBonus = CalculateWeaponTriangleBonusForAttack(advantage);
        int attackerAtk = Attacker.GetFirstAttackAttribute("Atk");
        int defenderDef = Defender.GetFirstAttackAttribute(Attacker.Weapon == "Magic" ? "Res" : "Def");
        
        int damage = CalculateBaseDamageForAttack(attackerAtk, defenderDef, weaponTriangleBonus);
        damage = ApplyDamageAlterationsForAttack(damage);

    }
    
    public void PerformCounterAttack(string advantage)
    {
        double weaponTriangleBonus = CalculateWeaponTriangleBonusForDefense(advantage);
        int defenderAtk = Defender.GetFirstAttackAttribute("Atk");
        int attackerDef = Attacker.GetFirstAttackAttribute(Defender.Weapon == "Magic" ? "Res" : "Def");
        int damage = CalculateBaseDamageForDefense(defenderAtk, attackerDef, weaponTriangleBonus);
        damage = ApplyDamageAlterationsForCounter(damage);

    }
    
    public void PerformFollowUpAttacker(string advantage)
    {
        double weaponTriangleBonus = CalculateWeaponTriangleBonusForAttack(advantage);
        int attackerAtk = Attacker.GetFollowUpAttribute("Atk");
        int defenderDef = Defender.GetFollowUpAttribute(Attacker.Weapon == "Magic" ? "Res" : "Def");
        int damage = CalculateBaseDamageForAttack(attackerAtk, defenderDef, weaponTriangleBonus);
        damage = ApplyDamageAlterationsForFollowUp(damage);
        
    }
    
    public void PerformFollowUpDefender(string advantage)
    {
        double weaponTriangleBonus = CalculateWeaponTriangleBonusForDefense(advantage);
        int defenderAtk = Defender.GetFollowUpAttribute("Atk");
        int attackerDef = Attacker.GetFollowUpAttribute(Defender.Weapon == "Magic" ? "Res" : "Def");

        int damage = CalculateBaseDamageForDefense(defenderAtk, attackerDef, weaponTriangleBonus);
        damage = ApplyDamageAlterationsForFollowUpDefense(damage);
        
    }
    
    private int CalculateDamage(int baseDamage, double reduction, double extraDamage, double absoluteReduction)
    {   
        
        double initialDamage = (double)baseDamage;
        double newDamage = initialDamage + Math.Floor(extraDamage);
        double damageReduced = newDamage * (100.0 - reduction) / 100.0;
        damageReduced = Math.Round(damageReduced, 9);
        return Math.Max(Convert.ToInt32(Math.Floor(damageReduced)) + Convert.ToInt32(absoluteReduction),0);
    }

    private double CalculateWeaponTriangleBonusForAttack(string advantage)
    {
        return advantage switch
        {
            "atacante" => 1.2,
            "defensor" => 0.8,
            _ => 1.0,
        };
    }
    
    private double CalculateWeaponTriangleBonusForDefense(string advantage)
    {
        return advantage switch
        {
            "defensor" => 1.2,
            "atacante" => 0.8,
            _ => 1.0,
        };
    }
    
    private int CalculateBaseDamageForAttack(int attackerAtk, int defenderDef, double weaponTriangleBonus)
    {
        return Math.Max((int)(attackerAtk * weaponTriangleBonus - defenderDef), 0);
    }
    
    private int CalculateBaseDamageForDefense(int attackValue, int defenseValue, double weaponTriangleBonus)
    {
        return Math.Max((int)(attackValue * weaponTriangleBonus - defenseValue), 0);
    }
    
    private int ApplyDamageAlterationsForAttack(int damage)
    {
        double reduction = Defender.GetFirstAttackDamageAlteration("PercentageReduction");
        double extraDamage = Attacker.GetFirstAttackDamageAlteration("ExtraDamage");
        double absoluteReduction = Defender.GetFirstAttackDamageAlteration("AbsoluteReduction");
        
        Attacker.DamageStatsFirstAttack["damage"] = damage;
        Attacker.DamageStatsFirstAttack["reduction"] = reduction;
        Attacker.DamageStatsFirstAttack["extraDamage"] = extraDamage;
        Attacker.DamageStatsFirstAttack["absoluteReduction"] = absoluteReduction;
        
        return CalculateDamage(damage, reduction, extraDamage, absoluteReduction);
    }
    
    private int ApplyDamageAlterationsForCounter(int damage)
    {
        double reduction = Attacker.GetFirstAttackDamageAlteration("PercentageReduction");
        double extraDamage = Defender.GetFirstAttackDamageAlteration("ExtraDamage");
        double absoluteReduction = Attacker.GetFirstAttackDamageAlteration("AbsoluteReduction");
        
        Defender.DamageStatsFirstAttack["damage"] = damage;
        Defender.DamageStatsFirstAttack["reduction"] = reduction;
        Defender.DamageStatsFirstAttack["extraDamage"] = extraDamage;
        Defender.DamageStatsFirstAttack["absoluteReduction"] = absoluteReduction;

        return CalculateDamage(damage, reduction, extraDamage, absoluteReduction);
    }
    
    private int ApplyDamageAlterationsForFollowUp(int damage)
    {
        double reduction = Defender.GetFollowUpDamageAlteration("PercentageReduction");
        double extraDamage = Attacker.GetFollowUpDamageAlteration("ExtraDamage");
        double absoluteReduction = Defender.GetFollowUpDamageAlteration("AbsoluteReduction");
        
        Attacker.DamageStatsFollowUp["damage"] = damage;
        Attacker.DamageStatsFollowUp["reduction"] = reduction;
        Attacker.DamageStatsFollowUp["extraDamage"] = extraDamage;
        Attacker.DamageStatsFollowUp["absoluteReduction"] = absoluteReduction;

        return CalculateDamage(damage, reduction, extraDamage, absoluteReduction);
    }
    
    private int ApplyDamageAlterationsForFollowUpDefense(int damage)
    {
        double reduction = Attacker.GetFollowUpDamageAlteration("PercentageReduction");
        double extraDamage = Defender.GetFollowUpDamageAlteration("ExtraDamage");
        double absoluteReduction = Attacker.GetFollowUpDamageAlteration("AbsoluteReduction");
        
        Defender.DamageStatsFollowUp["damage"] = damage;
        Defender.DamageStatsFollowUp["reduction"] = reduction;
        Defender.DamageStatsFollowUp["extraDamage"] = extraDamage;
        Defender.DamageStatsFollowUp["absoluteReduction"] = absoluteReduction;

        return CalculateDamage(damage, reduction, extraDamage, absoluteReduction);
    }
    
}