namespace Fire_Emblem {
    public class HPIncreaseSkill : Skill {
        public int HPIncrease { get; private set; }
        public bool alreadyUsed { get; private set; }

        public HPIncreaseSkill(string name, string description) : base(name, description) {
            HPIncrease = 15;
            alreadyUsed = false;

        }

        public override void ApplyEffect(Battle battle, Character owner) {

            if (!alreadyUsed)
            {
                owner.MaxHP += HPIncrease;
                owner.CurrentHP += HPIncrease;
                alreadyUsed = true;
            }
        }
    }
}