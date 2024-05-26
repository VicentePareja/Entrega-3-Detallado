namespace Fire_Emblem {
    public class AttackPlusSix : Skill {
        public int Bonus { get; private set; }

        public AttackPlusSix(string name, string description) : base(name, description) {
            Bonus = 6;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            owner.AddTemporaryBonus("Atk", Bonus);
   
        }
    }
}