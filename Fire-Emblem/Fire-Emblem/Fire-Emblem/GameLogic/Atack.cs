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
    
    private int CalculateDamage(int baseDamage, double reduction, double extraDamage, double absoluteReduction)
    {   
        baseDamage += Convert.ToInt32(extraDamage);
        double damageReduced = baseDamage * (100.0 - reduction) / 100.0;
        damageReduced = Math.Round(damageReduced, 9);
        return Math.Max(Convert.ToInt32(Math.Floor(damageReduced)) + Convert.ToInt32(absoluteReduction),0);
    }

    public void PerformAttack(string advantage)
    {
        double weaponTriangleBonus = advantage == "atacante" ? 1.2 : advantage == "defensor" ? 0.8 : 1.0;
        int attackerAtk = Attacker.GetFirstAttackAttribute("Atk");
        int defenderDef = Defender.GetFirstAttackAttribute(Attacker.Weapon == "Magic" ? "Res" : "Def");
        
        int damage = Math.Max((int)((attackerAtk * weaponTriangleBonus) - defenderDef),0);
        double reduction = Defender.GetFirstAttackDamageAlteration("PercentageReduction");
        double extraDamage = Attacker.GetFirstAttackDamageAlteration("ExtraDamage");
        double absoluteReduction = Defender.GetFirstAttackDamageAlteration("AbsoluteReduction");
        
        damage = CalculateDamage(damage, reduction, extraDamage, absoluteReduction);
        _view.WriteLine($"{Attacker.Name} ataca a {Defender.Name} con {damage} de daño");
        Defender.CurrentHP -= damage;
    }

    public void PerformCounterAttack(string advantage)
    {
        double weaponTriangleBonus = advantage == "defensor" ? 1.2 : advantage == "atacante" ? 0.8 : 1.0;
        
        int defenderAtk = Defender.GetFirstAttackAttribute("Atk");
        int attackerDef = Attacker.GetFirstAttackAttribute(Defender.Weapon == "Magic" ? "Res" : "Def");

        int damage = Math.Max((int)((defenderAtk * weaponTriangleBonus) - attackerDef),0);
        double reduction = Attacker.GetFirstAttackDamageAlteration("PercentageReduction");
        double extraDamage = Defender.GetFirstAttackDamageAlteration("ExtraDamage");
        double absoluteReduction = Attacker.GetFirstAttackDamageAlteration("AbsoluteReduction");
        damage = CalculateDamage(damage, reduction, extraDamage, absoluteReduction);

        _view.WriteLine($"{Defender.Name} ataca a {Attacker.Name} con {damage} de daño");

        Attacker.CurrentHP -= damage;
    }
    
    public void PerformFollowUpAtacker(string advantage)
    {
        double weaponTriangleBonus = advantage == "atacante" ? 1.2 : advantage == "defensor" ? 0.8 : 1.0;
        
        int attackerAtk = Attacker.GetFollowUpAttribute("Atk");
        int defenderDef = Defender.GetFollowUpAttribute(Attacker.Weapon == "Magic" ? "Res" : "Def");

        int damage = (int)((attackerAtk * weaponTriangleBonus) - defenderDef);
        damage = Math.Max(damage, 0);
        
        double reduction = Defender.TemporaryDamageAlterations.ContainsKey("PercentageReduction") ?
            Defender.TemporaryDamageAlterations["PercentageReduction"] : 0;
        damage = CalculateDamage(damage, reduction, 0.0, 0.0);

        _view.WriteLine($"{Attacker.Name} ataca a {Defender.Name} con {damage} de daño");

        Defender.CurrentHP -= damage;
    }
    
    public void PerformFollowUpDefender(string advantage)
    {
        double weaponTriangleBonus = advantage == "defensor" ? 1.2 : advantage == "atacante" ? 0.8 : 1.0;
        
        int defenderAtk = Defender.GetFollowUpAttribute("Atk");
        int attackerDef = Attacker.GetFollowUpAttribute(Defender.Weapon == "Magic" ? "Res" : "Def");

        int damage = (int)((defenderAtk * weaponTriangleBonus) - attackerDef);
        damage = Math.Max(damage, 0);
        
        double reduction = Attacker.TemporaryDamageAlterations.ContainsKey("PercentageReduction") ?
            Attacker.TemporaryDamageAlterations["PercentageReduction"] : 0;
        damage = CalculateDamage(damage, reduction, 0.0, 0.0);

        _view.WriteLine($"{Defender.Name} ataca a {Attacker.Name} con {damage} de daño");

        Attacker.CurrentHP -= damage;
    }
    
}