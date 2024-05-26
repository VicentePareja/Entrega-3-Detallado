namespace Fire_Emblem {
    public class Sandstorm : Skill {
        public Sandstorm(string name, string description) : base(name, description) {
        }

        public override void ApplyEffect(Battle battle, Character owner) {
     
            double adjustedAttackValue = Math.Floor(1.5 * owner.Def);
            int attackAdjustment = Convert.ToInt32(adjustedAttackValue) - owner.Atk;
            
            if (attackAdjustment > 0) {
                owner.AddTemporaryFollowUpBonuses("Atk", attackAdjustment);
            } else if (attackAdjustment < 0) {
                owner.AddTemporaryFollowUpPenalties("Atk", attackAdjustment);
            }
        }
    }
}