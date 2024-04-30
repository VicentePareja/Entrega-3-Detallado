namespace Fire_Emblem.NegateBonus{
    public class BeorcsBlessing : Skill {

        public BeorcsBlessing(string name, string description) : base(name, description) {
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;
            Character opponent;
            if (owner == combat._attacker) {
                opponent = combat._defender;
            }else {
                opponent = combat._attacker;
            }

            opponent.DisableAllBonuses();

        }
    }
}