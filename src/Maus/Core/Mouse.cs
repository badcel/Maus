using System.Drawing;

namespace Maus.Core;

public interface Mouse
{
    int GetDpi();
    void SetDpi(int dpi);
    int GetPollingRate();
    void SetPollingRate(int rate);
    int GetLiftOffDistance();
    void SetLiftOffDistance(int distance);
    Color GetColor();
    void SetColor(Color color);
}