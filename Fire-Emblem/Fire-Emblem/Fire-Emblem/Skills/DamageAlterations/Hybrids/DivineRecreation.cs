namespace Fire_Emblem {
    public class DivineRecreation : DamageAlterationSkill
    {
        Combat combat;
        Character attacker;
        Character defender;
        Character owner;
        Character opponent;

        public DivineRecreation(string name, string description) : base(name, description)
        {
            
        }

        public override void ApplyEffect(Battle battle, Character owner)
        {
            combat = battle.currentCombat;
            opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;
            attacker = combat._attacker;
            defender = combat._defender;
            this.owner = owner;
            
            int damageReductionPercentage = 30;
            

            if ((double)opponent.CurrentHP / (double)opponent.MaxHP >= 0.5)
            {
                ApplyPenalties(opponent);
                opponent.MultiplyFirstAttackDamageAlterations("PercentageReduction", damageReductionPercentage);
                TransferDamage(battle);
            }
        }

        private void ApplyPenalties(Character opponent)
        {
            opponent.AddTemporaryPenalty("Atk", -4);
            opponent.AddTemporaryPenalty("Spd", -4);
            opponent.AddTemporaryPenalty("Def", -4);
            opponent.AddTemporaryPenalty("Res", -4);
        }
        
        private void TransferDamage(Battle battle)
        {
            
            int damageReduced = SimulateCombat(attacker, defender);
            ApplyTransferDamage(owner, damageReduced);

        }
        
        private void ApplyTransferDamage(Character owner, int damageReduced)
        {
            if (owner == combat._attacker)
            {
                owner.AddFollowUpDamageAlteration("extraDamage", damageReduced);
            }
            if (owner == combat._defender)
            {
                owner.AddFirstAttackDamageAlteration("extraDamage", damageReduced);
            }
        }
        
        public int CalculateDamageReduced(Character opponent)
        {
            double baseDamage = opponent.DamageStatsFirstAttack["damage"];
            double reduction = owner.DamageStatsFirstAttack["reduction"];
            double extraDamage = opponent.DamageStatsFirstAttack["extraDamage"];
            double initialDamage = baseDamage;
            double newDamage = initialDamage + Math.Floor(extraDamage);
            double damageReduced = newDamage * (100.0 - reduction) / 100.0;
            damageReduced = Math.Round(damageReduced, 9);
            
            return (int) newDamage - (int) damageReduced;
        }
        
        private int SimulateCombat(Character attacker, Character defender)
        {
            SimulatedCombat combat = new SimulatedCombat(attacker, defender);
            combat.Start();
            int damageReduced = CalculateDamageReduced(opponent);
            PrintEveryDamage();
            combat.FinalizeCombat();
            return damageReduced;
        }
        
        private void PrintEveryDamage()
        {
            Console.WriteLine($" {attacker.DamageStatsFirstAttack["damage"]} de daño");
            Console.WriteLine($" {attacker.DamageStatsFirstAttack["reduction"]} de reducción");
            Console.WriteLine($" {attacker.DamageStatsFirstAttack["extraDamage"]} de daño extra");
            
            Console.WriteLine($" {defender.DamageStatsFirstAttack["damage"]} de daño");
            Console.WriteLine($" {defender.DamageStatsFirstAttack["reduction"]} de reducción");
            Console.WriteLine($" {defender.DamageStatsFirstAttack["extraDamage"]} de daño extra");
   
            
            Console.WriteLine($" {attacker.DamageStatsFollowUp["damage"]} de daño en Follow Up");
            Console.WriteLine($" {attacker.DamageStatsFollowUp["reduction"]} de reducción en Follow Up");
            Console.WriteLine($" {attacker.DamageStatsFollowUp["extraDamage"]} de daño extra en Follow Up");
            
        }
        
        private Character GetAttacker(Character owner, Combat combat)
        {
            return (combat._attacker == owner) ? combat._defender : combat._attacker;
        }
    }
}
