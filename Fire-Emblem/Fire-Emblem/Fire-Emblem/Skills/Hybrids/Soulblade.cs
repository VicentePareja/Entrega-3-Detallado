namespace Fire_Emblem {
    public class Soulblade : Skill {
        public Soulblade(string name, string description) : base(name, description) {
            // No se necesita establecer un bono específico ya que el cálculo depende del rival.
        }

        public override void ApplyEffect(Battle battle, Character owner)
        {
            Combat combat = battle.currentCombat;

            if (owner.Weapon == "Sword") {
                Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;
                double averageDefRes = (opponent.Def + opponent.Res) / 2.0;
                int changeDef = Convert.ToInt32(Math.Floor(averageDefRes - opponent.Def));
                int changeRes = Convert.ToInt32(Math.Floor(averageDefRes - opponent.Res));

                if (changeDef < 0 || changeRes < 0) {
                    Console.WriteLine("Applying penalties.");
                }

                ApplyAttributeChange(opponent, "Def", changeDef);
                ApplyAttributeChange(opponent, "Res", changeRes);
            }
        }

        private void ApplyAttributeChange(Character character, string attribute, int change) {
            if (change > 0) {
                character.AddTemporaryBonus(attribute, change);
            } else if (change < 0) {
                character.AddTemporaryPenalty(attribute, change);
            }
        }
    }
}