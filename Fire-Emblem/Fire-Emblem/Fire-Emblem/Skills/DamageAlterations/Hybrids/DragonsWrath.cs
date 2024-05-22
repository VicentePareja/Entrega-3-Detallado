namespace Fire_Emblem {
    public class DragonsWrath : DamageAlterationSkill {
        public DragonsWrath(string name, string description) : base(name, description) {
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.CurrentCombat;
            Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;

            owner.MultiplyFirstAttackDamageAlterations("PercentageReduction", 25);

            if (owner.GetEffectiveAttribute("Atk") > opponent.GetEffectiveAttribute("Res")) {
                double extraDamage = 0.25 * (owner.Atk - opponent.Res);
                owner.AddFirstAttackDamageAlteration("ExtraDamage", extraDamage);
            }
        }
    }
}