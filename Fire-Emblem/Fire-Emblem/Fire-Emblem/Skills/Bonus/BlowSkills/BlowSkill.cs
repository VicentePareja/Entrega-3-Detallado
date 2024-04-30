namespace Fire_Emblem
{
    public abstract class BlowSkill : Skill
    {
        protected BlowSkill(string name, string description) : base(name, description)
        {
        }
        protected abstract void ApplySpecificEffect(Character owner);

        public override void ApplyEffect(Battle battle, Character owner)
        {
            if (battle.currentCombat._attacker == owner)
            {
                ApplySpecificEffect(owner);
            }
        }
    }
}