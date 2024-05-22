namespace Fire_Emblem {
    public class DivineRecreation : DamageAlterationSkill
    {
        public DivineRecreation(string name, string description) : base(name, description)
        {
        }

        private double nextAttackExtraDamage = 0;
        private int firstAttackDamageReduction = 30;
        private string advantage = "";
        private string opponentFirstAtack = "";
        private string ownerNextAtack = "";
        private bool isOwnerAttacker;
        private Character Attacker;
        private Character Defender;
        private Character opponent;

        public override void ApplyEffect(Battle battle, Character owner)
        {
            SetVariables(battle, owner);

            if (opponent.CurrentHP >= opponent.MaxHP * 0.5)
            {
                ApplyStatPenalties(opponent);
                SetAttackOrder();
                double withOutReductionDamage = CalculateDamageOpponentWithOutReduction();
                owner.MultiplyFirstAttackDamageAlterations("PercentageReduction", firstAttackDamageReduction);
                double reducedDamage = CalculateDamageOpponentWithReduction();
                double damageDifference = withOutReductionDamage - reducedDamage;
                SetExtraDamage(owner, damageDifference);
            }
        }

        private void SetExtraDamage(Character owner, double damageDifference)
        {
            nextAttackExtraDamage = damageDifference;

            if (ownerNextAtack == "FollowUpAttacker")
            {
                owner.AddFollowUpDamageAlteration("ExtraDamage", nextAttackExtraDamage);
            }
            else if (opponentFirstAtack == "CounterAttack")
            {
                owner.AddFirstAttackDamageAlteration("ExtraDamage", nextAttackExtraDamage);
            }
        }

        private double CalculateDamageOpponentWithOutReduction()
        {
            if (opponentFirstAtack == "CounterAttack")
            {
                return (double)PerformCounterAttack(advantage);
            }
            else
            {
                return (double)PerformAttack(advantage);
            }
        }

        private double CalculateDamageOpponentWithReduction()
        {
            if (opponentFirstAtack == "CounterAttack")
            {
                return (double)PerformCounterAttack(advantage);
            }
            else
            {
                return (double)PerformAttack(advantage);
            }
        }

        private void SetAttackOrder()
        {
            if (isOwnerAttacker)
            {
                opponentFirstAtack = "CounterAttack";
                ownerNextAtack = "FollowUpAttacker";
            }
            else
            {
                opponentFirstAtack = "Attack";
                ownerNextAtack = "CounterAttack";

            }
        }

        private void SetVariables(Battle battle, Character owner)
        {
            Combat combat = battle.CurrentCombat;
            advantage = combat._advantage;
            Attacker = combat._attacker;
            Defender = combat._defender;
            opponent = DetermineOpponent(combat, owner);
            isOwnerAttacker = Attacker == owner;
        }

        private Character DetermineOpponent(Combat combat, Character owner)
        {
            return (combat._attacker == owner) ? combat._defender : combat._attacker;
        }

        private void ApplyStatPenalties(Character opponent)
        {
            opponent.AddTemporaryPenalty("Atk", -4);
            opponent.AddTemporaryPenalty("Spd", -4);
            opponent.AddTemporaryPenalty("Def", -4);
            opponent.AddTemporaryPenalty("Res", -4);
        }

        private int CalculateDamage(int baseDamage, double reduction, double extraDamage, double absoluteReduction)
        {
            double initialDamage = (double)baseDamage;
            double newDamage = initialDamage + extraDamage;
            double damageReduced = newDamage * (100.0 - reduction) / 100.0;
            damageReduced = Math.Round(damageReduced, 9);
            return Math.Max(Convert.ToInt32(Math.Floor(damageReduced)) + Convert.ToInt32(absoluteReduction), 0);
        }

        public int PerformAttack(string advantage)
        {
            double weaponTriangleBonus = advantage == "atacante" ? 1.2 : advantage == "defensor" ? 0.8 : 1.0;
            int attackerAtk = Attacker.GetFirstAttackAttribute("Atk");
            int defenderDef = Defender.GetFirstAttackAttribute(Attacker.Weapon == "Magic" ? "Res" : "Def");
            int damage = Math.Max((int)((attackerAtk * weaponTriangleBonus) - defenderDef), 0);
            double reduction = Defender.GetFirstAttackDamageAlteration("PercentageReduction");
            double extraDamage = Attacker.GetFirstAttackDamageAlteration("ExtraDamage");
            double absoluteReduction = Defender.GetFirstAttackDamageAlteration("AbsoluteReduction");
            return CalculateDamage(damage, reduction, extraDamage, absoluteReduction);
        }

        public int PerformCounterAttack(string advantage)
        {
            double weaponTriangleBonus = advantage == "defensor" ? 1.2 : advantage == "atacante" ? 0.8 : 1.0;
            int defenderAtk = Defender.GetFirstAttackAttribute("Atk");
            int attackerDef = Attacker.GetFirstAttackAttribute(Defender.Weapon == "Magic" ? "Res" : "Def");
            int damage = Math.Max((int)((defenderAtk * weaponTriangleBonus) - attackerDef), 0);
            double reduction = Attacker.GetFirstAttackDamageAlteration("PercentageReduction");
            double extraDamage = Defender.GetFirstAttackDamageAlteration("ExtraDamage");
            double absoluteReduction = Attacker.GetFirstAttackDamageAlteration("AbsoluteReduction");
            return CalculateDamage(damage, reduction, extraDamage, absoluteReduction);
        }

        public int PerformFollowUpAtacker(string advantage)
        {
            double weaponTriangleBonus = advantage == "atacante" ? 1.2 : advantage == "defensor" ? 0.8 : 1.0;
            int attackerAtk = Attacker.GetFollowUpAttribute("Atk");
            int defenderDef = Defender.GetFollowUpAttribute(Attacker.Weapon == "Magic" ? "Res" : "Def");
            int damage = Math.Max((int)((attackerAtk * weaponTriangleBonus) - defenderDef), 0);
            double reduction = Defender.GetFollowUpDamageAlteration("PercentageReduction");
            double extraDamage = Attacker.GetFollowUpDamageAlteration("ExtraDamage");
            double absoluteReduction = Defender.GetFollowUpDamageAlteration("AbsoluteReduction");
            return CalculateDamage(damage, reduction, extraDamage, absoluteReduction);
        }

        public int PerformFollowUpDefender(string advantage)
        {
            double weaponTriangleBonus = advantage == "defensor" ? 1.2 : advantage == "atacante" ? 0.8 : 1.0;
            int defenderAtk = Defender.GetFollowUpAttribute("Atk");
            int attackerDef = Attacker.GetFollowUpAttribute(Defender.Weapon == "Magic" ? "Res" : "Def");
            int damage = Math.Max((int)((defenderAtk * weaponTriangleBonus) - attackerDef), 0);
            double reduction = Attacker.GetFollowUpDamageAlteration("PercentageReduction");
            double extraDamage = Defender.GetFollowUpDamageAlteration("ExtraDamage");
            double absoluteReduction = Attacker.GetFollowUpDamageAlteration("AbsoluteReduction");
            return CalculateDamage(damage, reduction, extraDamage, absoluteReduction);
        }
    }
}