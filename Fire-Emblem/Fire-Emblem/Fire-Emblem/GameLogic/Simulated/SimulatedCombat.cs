using Fire_Emblem_View;
namespace Fire_Emblem
{
    public class SimulatedCombat
    {
        public readonly Character _attacker;
        public readonly Character _defender;
        public readonly string _advantage;

        public SimulatedCombat(Character attacker, Character defender)
        {
            _attacker = attacker;
            _defender = defender;
            _advantage = CalculateAdvantage(attacker, defender);
        }

        public void Start()
        {
            ExecuteCombat();
        }
        
        
        private void ExecuteCombat()
        {
            PerformInitialAttack();
            if (_defender.CurrentHP > 0)
            {
                PerformCounterAttack();
            }
            if (_attacker.CurrentHP > 0 && _defender.CurrentHP > 0)
            {
                PerformFollowUp();
            }
        }
        
        public void FinalizeCombat()
        {
            ClearTemporaryBonuses();
            ClearTemporaryPenalties();
            CleanTemporaryDamageAlterations();
        }
 
        private void PerformInitialAttack()
        {
            SimulatedAttack attack = new SimulatedAttack(_attacker, _defender);
            attack.PerformAttack(_advantage);
        }

        private void PerformCounterAttack()
        {
            if (_defender.CurrentHP > 0)
            {
                SimulatedAttack counterAttack = new SimulatedAttack(_attacker, _defender);
                counterAttack.PerformCounterAttack(_advantage);
            }
        }

        private void PerformFollowUp()
        {
            if (_attacker.CurrentHP > 0 && _defender.CurrentHP > 0)
            {
                SimulatedAttack followUpAttack = new SimulatedAttack(_attacker, _defender);
                if (_attacker.GetEffectiveAttribute("Spd") >= _defender.GetEffectiveAttribute("Spd") + 5)
                {
                    followUpAttack.PerformFollowUpAttacker(_advantage);
                }
                else if (_defender.GetEffectiveAttribute("Spd") >= _attacker.GetEffectiveAttribute("Spd") + 5)
                {
                    followUpAttack.PerformFollowUpDefender(_advantage);
                }
            }
        }
        
        private void ClearTemporaryBonuses()
        {
            _attacker.CleanBonuses();
            _defender.CleanBonuses();
            _attacker.CleanFirstAttackBonuses();
            _defender.CleanFirstAttackBonuses();
            _attacker.CleanFollowUpBonuses();
            _defender.CleanFollowUpBonuses();
            _attacker.ReEnableBonuses();
            _defender.ReEnableBonuses();
        }

        private void ClearTemporaryPenalties()
        {
            _attacker.CleanPenalties();
            _defender.CleanPenalties();
            _attacker.CleanFirstAttackPenalties();
            _defender.CleanFirstAttackPenalties();
            _attacker.CleanFollowUpPenalties();
            _defender.CleanFollowUpPenalties();
            _attacker.ReEnablePenalties();
            _defender.ReEnablePenalties();
        }

        private void CleanTemporaryDamageAlterations()
        {
            _attacker.CleanFirstAttackDamageAlterations();
            _defender.CleanFirstAttackDamageAlterations();
            _attacker.CleanTemporaryDamageAlterations();
            _defender.CleanTemporaryDamageAlterations();
            _attacker.CleanFollowUpDamageAlterations();
            _defender.CleanFollowUpDamageAlterations();
        }
        
        public string CalculateAdvantage(Character attacker, Character defender)
        {
            var advantages = new Dictionary<string, string>
            {
                {"Sword", "Axe"},
                {"Axe", "Lance"},
                {"Lance", "Sword"}
            };

            if (advantages.ContainsKey(attacker.Weapon) && advantages[attacker.Weapon] == defender.Weapon)
            {
                return "atacante";
            }
            else if (advantages.ContainsKey(defender.Weapon) && advantages[defender.Weapon] == attacker.Weapon)
            {
                return "defensor";
            }
            return "ninguno";
        }
        
    }
}
