namespace Fire_Emblem {
    public class Resolve : Skill {
        public int Bonus { get; private set; }

        public Resolve(string name, string description) : base(name, description) {
            Bonus = 7;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
        
            if (owner.CurrentHP <= owner.MaxHP * 0.75) {
                owner.AddTemporaryBonus("Def", Bonus);
                owner.AddTemporaryBonus("Res", Bonus);
            }
        }
    }
}