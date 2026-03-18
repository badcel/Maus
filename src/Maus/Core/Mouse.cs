using System.Drawing;

namespace Maus.Core;

public interface Mouse
{
    int GetDpi();
    void SetDpi(int dpi);
    
    int[] GetPollingRates();
    int GetPollingRateIndex();
    void SetPollingRateIndex(int index);
    
    int[] GetLiftOffDistances();
    int GetLiftOffDistanceIndex();
    void SetLiftOffDistanceIndex(int distance);
    
    Color GetColor();
    void SetColor(Color color);
}