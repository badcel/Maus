namespace Maus.Connectors.Demo;

public class Info : Core.MouseInfo
{
    public string GetName()
    {
        return "Demo Mouse";
    }

    public Core.Mouse Connect()
    {
        return new Mouse();
    }
}