namespace Fire_Emblem {
    public class SwordFocus : Skill {
        public int AttackBonus { get; private set; }
        public int ResistancePenalty { get; private set; }

        public SwordFocus(string name, string description) : base(name, description) {
            AttackBonus = 10;
            ResistancePenalty = -10;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            if (owner.Weapon == "Sword") {
                owner.AddTemporaryBonus("Atk", AttackBonus);
                owner.AddTemporaryPenalty("Res", ResistancePenalty);
            }
        }
    }
}