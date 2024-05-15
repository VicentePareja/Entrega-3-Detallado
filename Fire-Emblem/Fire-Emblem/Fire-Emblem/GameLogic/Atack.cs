using Fire_Emblem_View;
namespace Fire_Emblem;

public class Attack
{
    public Character Attacker { get; private set; }
    public Character Defender { get; private set; }
    
    private View _view;

    public Attack(Character attacker, Character defender, View view)
    {
        Attacker = attacker;
        Defender = defender;
        _view = view;
    }
    
    public void PerformAttack(string advantage)
    {
        double weaponTriangleBonus = CalculateWeaponTriangleBonusForAttack(advantage);
        int attackerAtk = Attacker.GetFirstAttackAttribute("Atk");
        int defenderDef = Defender.GetFirstAttackAttribute(Attacker.Weapon == "Magic" ? "Res" : "Def");
        
        int damage = CalculateBaseDamageForAttack(attackerAtk, defenderDef, weaponTriangleBonus);
        damage = ApplyDamageAlterationsForAttack(damage);
        _view.WriteLine($"{Attacker.Name} ataca a {Defender.Name} con {damage} de daño");
        Defender.CurrentHP -= damage;
    }
    
    public void PerformCounterAttack(string advantage)
    {
        double weaponTriangleBonus = CalculateWeaponTriangleBonusForDefense(advantage);
        int defenderAtk = Defender.GetFirstAttackAttribute("Atk");
        int attackerDef = Attacker.GetFirstAttackAttribute(Defender.Weapon == "Magic" ? "Res" : "Def");

        int damage = CalculateBaseDamageForDefense(defenderAtk, attackerDef, weaponTriangleBonus);
        damage = ApplyDamageAlterationsForCounter(damage);
        _view.WriteLine($"{Defender.Name} ataca a {Attacker.Name} con {damage} de daño");
        Attacker.CurrentHP -= damage;
    }
    
    public void PerformFollowUpAttacker(string advantage)
    {
        double weaponTriangleBonus = CalculateWeaponTriangleBonusForAttack(advantage);
        int attackerAtk = Attacker.GetFollowUpAttribute("Atk");
        int defenderDef = Defender.GetFollowUpAttribute(Attacker.Weapon == "Magic" ? "Res" : "Def");

        int damage = CalculateBaseDamageForAttack(attackerAtk, defenderDef, weaponTriangleBonus);
        damage = ApplyDamageAlterationsForFollowUp(damage);
        _view.WriteLine($"{Attacker.Name} ataca a {Defender.Name} con {damage} de daño");

        Defender.CurrentHP -= damage;
    }
    
    public void PerformFollowUpDefender(string advantage)
    {
        double weaponTriangleBonus = CalculateWeaponTriangleBonusForDefense(advantage);
        int defenderAtk = Defender.GetFollowUpAttribute("Atk");
        int attackerDef = Attacker.GetFollowUpAttribute(Defender.Weapon == "Magic" ? "Res" : "Def");

        int damage = CalculateBaseDamageForDefense(defenderAtk, attackerDef, weaponTriangleBonus);
        damage = ApplyDamageAlterationsForDefense(damage);
        _view.WriteLine($"{Defender.Name} ataca a {Attacker.Name} con {damage} de daño");

        Attacker.CurrentHP -= damage;
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

        return CalculateDamage(damage, reduction, extraDamage, absoluteReduction);
    }
    
    private int ApplyDamageAlterationsForFollowUp(int damage)
    {
        double reduction = Defender.GetFollowUpDamageAlteration("PercentageReduction");
        double extraDamage = Attacker.GetFollowUpDamageAlteration("ExtraDamage");
        double absoluteReduction = Defender.GetFollowUpDamageAlteration("AbsoluteReduction");

        return CalculateDamage(damage, reduction, extraDamage, absoluteReduction);
    }
    
    private int ApplyDamageAlterationsForCounter(int damage)
    {
        double reduction = Attacker.GetFirstAttackDamageAlteration("PercentageReduction");
        double extraDamage = Defender.GetFirstAttackDamageAlteration("ExtraDamage");
        double absoluteReduction = Attacker.GetFirstAttackDamageAlteration("AbsoluteReduction");

        return CalculateDamage(damage, reduction, extraDamage, absoluteReduction);
    }
    
    private int ApplyDamageAlterationsForDefense(int damage)
    {
        double reduction = Attacker.GetFollowUpDamageAlteration("PercentageReduction");
        double extraDamage = Defender.GetFollowUpDamageAlteration("ExtraDamage");
        double absoluteReduction = Attacker.GetFollowUpDamageAlteration("AbsoluteReduction");

        return CalculateDamage(damage, reduction, extraDamage, absoluteReduction);
    }
    
}