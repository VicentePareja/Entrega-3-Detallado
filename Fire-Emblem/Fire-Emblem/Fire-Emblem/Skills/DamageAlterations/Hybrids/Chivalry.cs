namespace Fire_Emblem {
    public class Chivalry : DamageAlterationSkill {
        public Chivalry(string name, string description) : base(name, description) {
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;
            Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;
            bool isOppnentFullHP = opponent.CurrentHP == opponent.MaxHP;
            bool isOwnerAtacking = combat._attacker == owner;

            if (isOppnentFullHP && isOwnerAtacking) {
                owner.AddTemporaryDamageAlteration("ExtraDamage", 2);
                owner.AddTemporaryDamageAlteration("AbsoluteReduction", -2);
            }
        }
    }
}