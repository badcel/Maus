namespace Maus;

public class DemoInfo : MouseInfo
{
    public string GetName()
    {
        return "Demo Mouse";
    }

    public Mouse Connect()
    {
        return new DemoMouse();
    }
}