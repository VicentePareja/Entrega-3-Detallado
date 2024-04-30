namespace Fire_Emblem {
    public class ResistancePlusFive : Skill {
        public int Bonus { get; private set; }

        public ResistancePlusFive(string name, string description) : base(name, description) {
            Bonus = 5;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            owner.AddTemporaryBonus("Res", Bonus);
   
        }
    }
}