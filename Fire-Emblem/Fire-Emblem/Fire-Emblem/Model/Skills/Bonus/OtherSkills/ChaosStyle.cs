namespace Fire_Emblem {
    public class ChaosStyle : Skill {
        public int Bonus { get; private set; }

        public ChaosStyle(string name, string description) : base(name, description) {
            Bonus = 3;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.CurrentCombat;
            if (combat._attacker == owner && ShouldApplyEffect(owner, combat._defender)) {
                owner.AddTemporaryBonus("Spd", Bonus);
            }
        }

        private bool ShouldApplyEffect(Character owner, Character opponent) {
            bool isOwnerUsingPhysical = IsPhysicalWeapon(owner.Weapon);
            bool isOwnerUsingMagical = IsMagicalWeapon(owner.Weapon);
            bool isOpponentUsingMagical = IsMagicalWeapon(opponent.Weapon);
            bool isOpponentUsingPhysical = IsPhysicalWeapon(opponent.Weapon);

            return (isOwnerUsingPhysical && isOpponentUsingMagical) || (isOwnerUsingMagical && isOpponentUsingPhysical);
        }

        private bool IsPhysicalWeapon(string weapon) {
            return weapon == "Sword" || weapon == "Axe" || weapon == "Lance" || weapon == "Bow";
        }

        private bool IsMagicalWeapon(string weapon) {
            return weapon == "Magic";
        }
    }
}