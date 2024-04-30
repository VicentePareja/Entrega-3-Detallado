namespace Fire_Emblem.NegateBonus{
    public class AgneasArrow : Skill {

        public AgneasArrow(string name, string description) : base(name, description) {
        }

        public override void ApplyEffect(Battle battle, Character owner)
        {
            owner.DisableAllPenalties();
        }
    }
}