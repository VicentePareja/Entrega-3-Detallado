using Fire_Emblem_View;
namespace Fire_Emblem;

public class BattleInterface
{
    private readonly View _view;
    
    public BattleInterface(View view)
    {
        _view = view;
    }
    
    public void PrintRoundStart(int turn, Character attackerUnit, Player attackerPlayer)
    {
        _view.WriteLine($"Round {turn}: {attackerUnit.Name} ({attackerPlayer.Name}) comienza");
    }
}