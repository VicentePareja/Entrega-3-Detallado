using Fire_Emblem_View;

namespace Fire_Emblem
{
    public class Combat
    {
        public readonly Character _attacker;
        public readonly Character _defender;
        public readonly string _advantage;
        private readonly CombatInterface _combatInterface;
        private readonly Battle _battle;

        public Combat(Character attacker, Character defender, string advantage, CombatInterface combatInterface, Battle battle)
        {
            _attacker = attacker;
            _defender = defender;
            _advantage = advantage;
            _combatInterface = combatInterface;
            _battle = battle;
        }

        public void Start()
        {
            PrepareCombat();
            ExecuteCombat();
            FinalizeCombat();
        }

        private void PrepareCombat()
        {
            ApplySkills();
            _combatInterface.PrintSkills(_attacker);
            _combatInterface.PrintSkills(_defender);
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
        
        private void FinalizeCombat()
        {
            ClearTemporaryBonuses();
            ClearTemporaryPenalties();
            CleanTemporaryDamageAlterations();
            _combatInterface.PrintFinalState(_attacker, _defender);
        }

        private void ApplySkills() {
            ApplyAttackerSkills();
            ApplyDefenderSkills();
            ApplyAttackerDamageAlterationSkills();
            ApplyDefenderDamageAlterationSkills();
        }

        private void ApplyAttackerSkills()
        {
            foreach (var skill in _attacker.Skills) {
                if (!skill.IsDamageAlteration)
                {
                    skill.ApplyEffect(_battle, _attacker);
                }
            }
        }
        
        private void ApplyDefenderSkills()
        {
            foreach (var skill in _defender.Skills) {
                if (!skill.IsDamageAlteration)
                {
                    skill.ApplyEffect(_battle, _defender);
                }
            }
        }
        
        private void ApplyAttackerDamageAlterationSkills()
        {
            foreach (var skill in _attacker.Skills) {
                if (skill.IsDamageAlteration)
                {
                    skill.ApplyEffect(_battle, _attacker);
                }
            }
        }
        
        private void ApplyDefenderDamageAlterationSkills()
        {
            foreach (var skill in _defender.Skills) {
                if (skill.IsDamageAlteration)
                {
                    skill.ApplyEffect(_battle, _defender);
                }
            }
        }

        private void PerformInitialAttack()
        {
            Attack attack = new Attack(_attacker, _defender, _combatInterface);
            attack.PerformAttack(_advantage);
        }

        private void PerformCounterAttack()
        {
            if (_defender.CurrentHP > 0)
            {
                Attack counterAttack = new Attack(_attacker, _defender, _combatInterface);
                counterAttack.PerformCounterAttack(_advantage);
            }
        }

        private void PerformFollowUp()
        {
            if (_attacker.CurrentHP > 0 && _defender.CurrentHP > 0)
            {
                Attack followUpAttack = new Attack(_attacker, _defender, _combatInterface);
                if (_attacker.GetEffectiveAttribute("Spd") >= _defender.GetEffectiveAttribute("Spd") + 5)
                {
                    followUpAttack.PerformFollowUpAttacker(_advantage);
                }
                else if (_defender.GetEffectiveAttribute("Spd") >= _attacker.GetEffectiveAttribute("Spd") + 5)
                {
                    followUpAttack.PerformFollowUpDefender(_advantage);
                }
                else
                {
                    _combatInterface.PrintNoFollowUp();
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
    }
}
