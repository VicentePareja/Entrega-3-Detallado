using Fire_Emblem_View;
namespace Fire_Emblem;

public class BattleInterface
{
    private readonly View _view;
    public CombatInterface CombatInterface { get; private set; }
    
    public BattleInterface(View view)
    {
        _view = view;
        CombatInterface = new CombatInterface(view);
    }
    
    public void PrintRoundStart(int turn, Character attackerUnit, Player attackerPlayer)
    {
        _view.WriteLine($"Round {turn}: {attackerUnit.Name} ({attackerPlayer.Name}) comienza");
    }
    public void PrintAdvantage(Character attackerUnit, Character defenderUnit)
    {
        _view.WriteLine($"{attackerUnit.Name} ({attackerUnit.Weapon}) tiene ventaja con respecto a {defenderUnit.Name} ({defenderUnit.Weapon})");
    }
    public void PrintNotAdvantage()
    {
        _view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
    }
    public void PrintCharacterOptions(Player player)
    {
        _view.WriteLine($"{player.Name} selecciona una opción");
        for (int i = 0; i < player.Team.Characters.Count; i++)
        {
            _view.WriteLine($"{i}: {player.Team.Characters[i].Name}");
        }
    }
    public void PrintNotValidOption()
    {
        _view.WriteLine("Elección inválida. Por favor, elige de nuevo.");
    }
    public void PrintWinner(Player player)
    {
        _view.WriteLine($"{player.Name} ganó");
    }

    public void PrintTie()
    {
        _view.WriteLine("Empate!");
    }

    public void PrintAdvantages(string advantage, Character attackerUnit, Character defenderUnit)
    {
        switch (advantage)
        {
            case "atacante":
                PrintAdvantage(attackerUnit, defenderUnit);
                break;
            case "defensor":
                PrintAdvantage(defenderUnit, attackerUnit);
                break;
            default:
                PrintNotAdvantage();
                break;
        }
    }
}