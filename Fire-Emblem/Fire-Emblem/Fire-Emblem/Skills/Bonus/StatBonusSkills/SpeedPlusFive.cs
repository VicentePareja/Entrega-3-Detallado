namespace Fire_Emblem {
    public class SpeedPlusFive : Skill {
        public int Bonus { get; private set; }

        public SpeedPlusFive(string name, string description) : base(name, description) {
            Bonus = 5;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            owner.AddTemporaryBonus("Spd", Bonus);
   
        }
    }
}