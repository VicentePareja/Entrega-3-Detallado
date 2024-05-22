namespace Fire_Emblem {
    public class DivineRecreation : DamageAlterationSkill
    {
        public DivineRecreation(string name, string description) : base(name, description)
        {
        }

        private double _nextAttackExtraDamage = 0;
        private double _baseDamage;
        private double _reduction;
        private double _extraDamage;
        private double _absoluteReduction;
        private int _firstAttackDamageReduction = 30;
        private string _advantage = "";
        private string _opponentFirstAtack = "";
        private string _ownerNextAtack = "";
        private bool _isOwnerAttacker;
        private Character _attacker;
        private Character _defender;
        private Character _opponent;

        public override void ApplyEffect(Battle battle, Character owner)
        {
            SetVariables(battle, owner);

            if (_opponent.CurrentHP >= _opponent.MaxHP * 0.5)
            {
                ApplyStatPenalties(_opponent);
                SetAttackOrder();
                double withOutReductionDamage = CalculateDamageOpponentWithOutReduction();
                owner.MultiplyFirstAttackDamageAlterations("PercentageReduction", _firstAttackDamageReduction);
                double reducedDamage = CalculateDamageOpponentWithReduction();
                double damageDifference = withOutReductionDamage - reducedDamage;
                SetExtraDamage(owner, damageDifference);
            }
        }

        private void SetExtraDamage(Character owner, double damageDifference)
        {
            _nextAttackExtraDamage = damageDifference;

            if (_ownerNextAtack == "FollowUpAttacker")
            {
                owner.AddFollowUpDamageAlteration("ExtraDamage", _nextAttackExtraDamage);
            }
            else if (_opponentFirstAtack == "CounterAttack")
            {
                owner.AddFirstAttackDamageAlteration("ExtraDamage", _nextAttackExtraDamage);
            }
        }

        private double CalculateDamageOpponentWithOutReduction()
        {
            if (_opponentFirstAtack == "CounterAttack")
            {
                return (double)PerformCounterAttack(_advantage);
            }
            else
            {
                return (double)PerformAttack(_advantage);
            }
        }

        private double CalculateDamageOpponentWithReduction()
        {
            if (_opponentFirstAtack == "CounterAttack")
            {
                return (double)PerformCounterAttack(_advantage);
            }
            else
            {
                return (double)PerformAttack(_advantage);
            }
        }

        private void SetAttackOrder()
        {
            if (_isOwnerAttacker)
            {
                _opponentFirstAtack = "CounterAttack";
                _ownerNextAtack = "FollowUpAttacker";
            }
            else
            {
                _opponentFirstAtack = "Attack";
                _ownerNextAtack = "CounterAttack";

            }
        }

        private void SetVariables(Battle battle, Character owner)
        {
            Combat combat = battle.CurrentCombat;
            _advantage = combat._advantage;
            _attacker = combat._attacker;
            _defender = combat._defender;
            _opponent = DetermineOpponent(combat, owner);
            _isOwnerAttacker = _attacker == owner;
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

        private int CalculateDamage()
        {
            double initialDamage = (double)_baseDamage;
            double newDamage = initialDamage + _extraDamage;
            double damageReduced = newDamage * (100.0 - _reduction) / 100.0;
            damageReduced = Math.Round(damageReduced, 9);
            return Math.Max(Convert.ToInt32(Math.Floor(damageReduced)) + Convert.ToInt32(_absoluteReduction), 0);
        }

        public int PerformAttack(string advantage)
        {
            double weaponTriangleBonus = advantage == "atacante" ? 1.2 : advantage == "defensor" ? 0.8 : 1.0;
            int attackerAtk = _attacker.GetFirstAttackAttribute("Atk");
            int defenderDef = _defender.GetFirstAttackAttribute(_attacker.Weapon == "Magic" ? "Res" : "Def");
            _baseDamage = Math.Max((int)((attackerAtk * weaponTriangleBonus) - defenderDef), 0);
            _reduction = _defender.GetFirstAttackDamageAlteration("PercentageReduction");
            _extraDamage = _attacker.GetFirstAttackDamageAlteration("ExtraDamage");
            _absoluteReduction = _defender.GetFirstAttackDamageAlteration("AbsoluteReduction");
            return CalculateDamage();
        }

        public int PerformCounterAttack(string advantage)
        {
            double weaponTriangleBonus = advantage == "defensor" ? 1.2 : advantage == "atacante" ? 0.8 : 1.0;
            int defenderAtk = _defender.GetFirstAttackAttribute("Atk");
            int attackerDef = _attacker.GetFirstAttackAttribute(_defender.Weapon == "Magic" ? "Res" : "Def");
            _baseDamage = Math.Max((int)((defenderAtk * weaponTriangleBonus) - attackerDef), 0);
            _reduction = _attacker.GetFirstAttackDamageAlteration("PercentageReduction");
            _extraDamage = _defender.GetFirstAttackDamageAlteration("ExtraDamage");
            _absoluteReduction = _attacker.GetFirstAttackDamageAlteration("AbsoluteReduction");
            return CalculateDamage();
        }

        public int PerformFollowUpAtacker(string advantage)
        {
            double weaponTriangleBonus = advantage == "atacante" ? 1.2 : advantage == "defensor" ? 0.8 : 1.0;
            int attackerAtk = _attacker.GetFollowUpAttribute("Atk");
            int defenderDef = _defender.GetFollowUpAttribute(_attacker.Weapon == "Magic" ? "Res" : "Def");
            _baseDamage = Math.Max((int)((attackerAtk * weaponTriangleBonus) - defenderDef), 0);
            _reduction = _defender.GetFollowUpDamageAlteration("PercentageReduction");
            _extraDamage = _attacker.GetFollowUpDamageAlteration("ExtraDamage");
            _absoluteReduction = _defender.GetFollowUpDamageAlteration("AbsoluteReduction");
            return CalculateDamage();
        }

        public int PerformFollowUpDefender(string advantage)
        {
            double weaponTriangleBonus = advantage == "defensor" ? 1.2 : advantage == "atacante" ? 0.8 : 1.0;
            int defenderAtk = _defender.GetFollowUpAttribute("Atk");
            int attackerDef = _attacker.GetFollowUpAttribute(_defender.Weapon == "Magic" ? "Res" : "Def");
            _baseDamage = Math.Max((int)((defenderAtk * weaponTriangleBonus) - attackerDef), 0);
            _reduction = _attacker.GetFollowUpDamageAlteration("PercentageReduction");
            _extraDamage = _defender.GetFollowUpDamageAlteration("ExtraDamage");
            _absoluteReduction = _attacker.GetFollowUpDamageAlteration("AbsoluteReduction");
            return CalculateDamage();
        }
    }
}