namespace Fire_Emblem {
    public class AtkResPlusFive : Skill {
        public int Bonus { get; private set; }

        public AtkResPlusFive(string name, string description) : base(name, description) {
            Bonus = 5;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            owner.AddTemporaryBonus("Atk", Bonus);
            owner.AddTemporaryBonus("Res", Bonus);
   
        }
    }
}