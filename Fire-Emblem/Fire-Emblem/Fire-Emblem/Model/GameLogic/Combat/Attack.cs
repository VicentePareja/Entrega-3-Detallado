using Fire_Emblem_View;
namespace Fire_Emblem;

public class Attack
{
    private readonly Character _attacker;
    private readonly Character _defender;
    private readonly CombatInterface _combatInterface;
    private int _damage;
    private double _weaponTriangleBonus;
    private double _reduction;
    private double _extraDamage;
    private double _absoluteReduction;

    public Attack(Character attacker, Character defender, CombatInterface view)
    {
        _attacker = attacker;
        _defender = defender;
        _combatInterface = view;
    }
    
    public void PerformAttack(string advantage)
    {
        _weaponTriangleBonus = CalculateWeaponTriangleBonusForAttack(advantage);
        int attackerAtk = _attacker.GetFirstAttackAttribute("Atk");
        int defenderDef = _defender.GetFirstAttackAttribute(_attacker.Weapon == "Magic" ? "Res" : "Def");
        
        _damage = CalculateBaseDamageForAttack(attackerAtk, defenderDef);
        _damage = ApplyDamageAlterationsForAttack();
        _combatInterface.PrintAttack(_attacker, _defender, _damage);
        _defender.CurrentHP -= _damage;
    }
    
    public void PerformCounterAttack(string advantage)
    {
        _weaponTriangleBonus = CalculateWeaponTriangleBonusForDefense(advantage);
        int defenderAtk = _defender.GetFirstAttackAttribute("Atk");
        int attackerDef = _attacker.GetFirstAttackAttribute(_defender.Weapon == "Magic" ? "Res" : "Def");

        _damage = CalculateBaseDamageForDefense(defenderAtk, attackerDef);
        _damage = ApplyDamageAlterationsForCounter();
        _combatInterface.PrintAttack(_defender,_attacker , _damage);
        _attacker.CurrentHP -= _damage;
    }
    
    public void PerformFollowUpAttacker(string advantage)
    {
        _weaponTriangleBonus = CalculateWeaponTriangleBonusForAttack(advantage);
        int attackerAtk = _attacker.GetFollowUpAttribute("Atk");
        int defenderDef = _defender.GetFollowUpAttribute(_attacker.Weapon == "Magic" ? "Res" : "Def");

        _damage = CalculateBaseDamageForAttack(attackerAtk, defenderDef);
        _damage = ApplyDamageAlterationsForFollowUp();
        _combatInterface.PrintAttack(_attacker, _defender, _damage);

        _defender.CurrentHP -= _damage;
    }
    
    public void PerformFollowUpDefender(string advantage)
    {
        _weaponTriangleBonus = CalculateWeaponTriangleBonusForDefense(advantage);
        int defenderAtk = _defender.GetFollowUpAttribute("Atk");
        int attackerDef = _attacker.GetFollowUpAttribute(_defender.Weapon == "Magic" ? "Res" : "Def");

        _damage = CalculateBaseDamageForDefense(defenderAtk, attackerDef);
        _damage = ApplyDamageAlterationsForDefense();
        _combatInterface.PrintAttack(_defender,_attacker , _damage);

        _attacker.CurrentHP -= _damage;
    }
    
    private int CalculateDamage()
    {   
        double initialDamage = (double)_damage;
        double newDamage = initialDamage + Math.Floor(_extraDamage);
        double damageReduced = newDamage * (100.0 - _reduction) / 100.0;
        damageReduced = Math.Round(damageReduced, 9);
        return Math.Max(Convert.ToInt32(Math.Floor(damageReduced)) + Convert.ToInt32(_absoluteReduction),0);
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
    
    private int CalculateBaseDamageForAttack(int attackerAtk, int defenderDef)
    {
        return Math.Max((int)(attackerAtk * _weaponTriangleBonus - defenderDef), 0);
    }
    
    private int CalculateBaseDamageForDefense(int attackValue, int defenseValue)
    {
        return Math.Max((int)(attackValue * _weaponTriangleBonus - defenseValue), 0);
    }
    
    private int ApplyDamageAlterationsForAttack()
    {
        _reduction = _defender.GetFirstAttackDamageAlteration("PercentageReduction");
        _extraDamage = _attacker.GetFirstAttackDamageAlteration("ExtraDamage");
        _absoluteReduction = _defender.GetFirstAttackDamageAlteration("AbsoluteReduction");

        return CalculateDamage();
    }
    
    private int ApplyDamageAlterationsForFollowUp()
    {
        _reduction = _defender.GetFollowUpDamageAlteration("PercentageReduction");
        _extraDamage = _attacker.GetFollowUpDamageAlteration("ExtraDamage");
        _absoluteReduction = _defender.GetFollowUpDamageAlteration("AbsoluteReduction");

        return CalculateDamage();
    }
    
    private int ApplyDamageAlterationsForCounter()
    {
        _reduction = _attacker.GetFirstAttackDamageAlteration("PercentageReduction");
        _extraDamage = _defender.GetFirstAttackDamageAlteration("ExtraDamage");
        _absoluteReduction = _attacker.GetFirstAttackDamageAlteration("AbsoluteReduction");

        return CalculateDamage();
    }
    
    private int ApplyDamageAlterationsForDefense()
    {
        _reduction = _attacker.GetFollowUpDamageAlteration("PercentageReduction");
        _extraDamage = _defender.GetFollowUpDamageAlteration("ExtraDamage");
        _absoluteReduction = _attacker.GetFollowUpDamageAlteration("AbsoluteReduction");

        return CalculateDamage();
    }
    
}