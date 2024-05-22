namespace Fire_Emblem {
    public class GenericSkill : Skill {
        public GenericSkill(string name, string description) : base(name, description) { }

        public override void ApplyEffect(Battle battle, Character owner) {
            Console.WriteLine($"Applying generic effect of {Name} to {owner.Name}.");
        }
    }
}