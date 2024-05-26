using Fire_Emblem_View;
namespace Fire_Emblem;

public class BattleController
{
    private readonly View _view;
    
    public BattleController(View view)
    {
        _view = view;
    }
    
    public string GetUnitIndex()
    {
        return _view.ReadLine();
    }
}