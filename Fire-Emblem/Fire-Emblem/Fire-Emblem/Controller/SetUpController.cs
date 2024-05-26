using Fire_Emblem_View;
namespace Fire_Emblem;

public class SetUpController
{
    private readonly View _view;
    public SetUpController(View view)
    {
        _view = view;
    }
    
    public string GetTeamsFolder()
    {
        string input =_view.ReadLine();
        return input;
    }
}