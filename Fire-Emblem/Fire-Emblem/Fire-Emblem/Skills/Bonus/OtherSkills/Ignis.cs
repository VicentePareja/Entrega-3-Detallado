namespace Fire_Emblem {
    public class Ignis : Skill {
        public double Bonus { get; private set; }

        public Ignis(string name, string description) : base(name, description) {
            Bonus = 0.5;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;
            int bonusAtk = Convert.ToInt32(Math.Floor(owner.Atk * Bonus));
            owner.AddTemporaryFirstAttackBonuses("Atk", bonusAtk);
        }
    }
}