using System.Drawing;

namespace Maus.Connectors.Demo;

public class Mouse : Core.Mouse
{
    private int dpi = 200;
    private int pollingRateIndex;
    private int liftOffDistanceIndex;
    private Color color = Color.Blue;

    public int GetDpi()
    {
        return dpi;
    }

    public void SetDpi(int dpi)
    {
        this.dpi = dpi;
    }

    public int[] GetPollingRates()
    {
        return [1000, 500, 125];
    }

    public int GetPollingRateIndex()
    {
        return pollingRateIndex;
    }

    public void SetPollingRateIndex(int index)
    {
        pollingRateIndex = index;
    }
    
    public int[] GetLiftOffDistances()
    {
        return [2, 3];
    }

    public int GetLiftOffDistanceIndex()
    {
        return liftOffDistanceIndex;
    }

    public void SetLiftOffDistanceIndex(int index)
    {
        liftOffDistanceIndex = index;
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