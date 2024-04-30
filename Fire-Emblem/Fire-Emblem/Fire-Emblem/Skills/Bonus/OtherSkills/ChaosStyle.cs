namespace Fire_Emblem {
    public class ChaosStyle : Skill {
        public int Bonus { get; private set; }

        public ChaosStyle(string name, string description) : base(name, description) {
            Bonus = 3; 
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;
            if (combat._attacker == owner) {
                bool isOwnerUsingPhysical = IsPhysicalWeapon(owner.Weapon);
                bool isOwnerUsingMagical = IsMagicalWeapon(owner.Weapon);
                bool isOpponentUsingMagical = IsMagicalWeapon(combat._defender.Weapon);
                bool isOpponentUsingPhysical = IsPhysicalWeapon(combat._defender.Weapon);

                if ((isOwnerUsingPhysical && isOpponentUsingMagical) || (isOwnerUsingMagical && isOpponentUsingPhysical)) {
                    owner.AddTemporaryBonus("Spd", Bonus);
                }
            }
        }

        private bool IsPhysicalWeapon(string weapon) {
            return weapon == "Sword" || weapon == "Axe" || weapon == "Lance" || weapon == "Bow"; 
        }
        
        private bool IsMagicalWeapon(string weapon) {
            return weapon == "Magic"; 
        }
    }
}