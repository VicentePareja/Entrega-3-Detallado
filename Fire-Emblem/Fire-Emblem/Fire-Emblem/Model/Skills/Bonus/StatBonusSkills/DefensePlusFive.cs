namespace Fire_Emblem {
    public class DefensePlusFive : Skill {
        public int Bonus { get; private set; }

        public DefensePlusFive(string name, string description) : base(name, description) {
            Bonus = 5;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            owner.AddTemporaryBonus("Def", Bonus);
   
        }
    }
}