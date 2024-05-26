﻿namespace Fire_Emblem {
    public class WindBoost : Skill {
        public int Bonus { get; private set; }

        public WindBoost(string name, string description) : base(name, description) {
            Bonus = 6;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.CurrentCombat;
            Character otherCharacter;
            Character thisCharacter;

            if (owner == combat._attacker) {
                thisCharacter = combat._attacker;
                otherCharacter = combat._defender;
            } else {
                thisCharacter = combat._defender;
                otherCharacter = combat._attacker;
            }

            if (thisCharacter.CurrentHP >= otherCharacter.CurrentHP + 3) {
                owner.AddTemporaryBonus("Spd", Bonus);
            }
        }
    }
}