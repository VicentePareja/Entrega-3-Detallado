namespace Fire_Emblem;

public abstract class Skill
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    
    public bool IsDamageAlteration { get; protected set; }

    protected Skill(string name, string description)
    {
        Name = name;
        Description = description;
        IsDamageAlteration = false;
    }
    
    public abstract void ApplyEffect(Battle battle, Character owner);

    public void PrintDetails()
    {
        Console.WriteLine($"Habilidad: {Name}\nDescripción: {Description}\n");
    }
}