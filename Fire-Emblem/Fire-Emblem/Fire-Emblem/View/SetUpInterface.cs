using Fire_Emblem_View;
namespace Fire_Emblem;

public class SetUpInterface
{
    private readonly View _view;
    
    public SetUpInterface(View view)
    {
        _view = view;
    }

    public void PrintTeamsNotValid()
    {
        _view.WriteLine("Archivo de equipos no válido");
    }
    
    public void PrintGetTeamsFolder()
    {
        _view.WriteLine("Elige un archivo para cargar los equipos");
    }
    
    public void PrintNotFilesInFolder()
    {
        _view.WriteLine("No hay archivos disponibles.");
    }
    
    public void PrintFile(int index, string file)
    {
        _view.WriteLine($"{index}: {file}");
    }
    
}