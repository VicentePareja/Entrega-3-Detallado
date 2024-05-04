namespace Fire_Emblem {
    public class DragonsWrath : Skill {
        public DragonsWrath(string name, string description) : base(name, description) {
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;
            Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;

            owner.MultiplyFirstAttackDamageAlterations("PercentageReduction", 25);

            if (owner.Atk > opponent.Res) {
                double extraDamage = 0.25 * (owner.Atk - opponent.Res);
                owner.AddFirstAttackDamageAlteration("ExtraDamage", extraDamage);
            }
        }
    }
}