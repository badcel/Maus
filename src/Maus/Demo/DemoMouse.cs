using System.Drawing;

namespace Maus;

public class DemoMouse : Mouse
{
    private int dpi = 200;
    private int pollingRate = 1000;
    private int liftOffDistance = 2;
    private Color color = Color.Blue;
    
    public int GetDpi()
    {
        return dpi;
    }

    public void SetDpi(int dpi)
    {
        this.dpi = dpi;
    }

    public int GetPollingRate()
    {
        return pollingRate;
    }

    public void SetPollingRate(int rate)
    {
        pollingRate = rate;
    }

    public int GetLiftOffDistance()
    {
        return liftOffDistance;
    }

    public void SetLiftOffDistance(int distance)
    {
        liftOffDistance = distance;
    }

    public Color GetColor()
    {
        return color;
    }

    public void SetColor(Color color)
    {
        this.color = color;
    }
}