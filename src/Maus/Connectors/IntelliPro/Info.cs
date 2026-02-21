using HidApi;

namespace Maus.Connectors.IntelliPro;

public class Info(DeviceInfo deviceInfo) : Core.MouseInfo
{
    public string GetName()
    {
        return $"{deviceInfo.ManufacturerString} {deviceInfo.ProductString} ({deviceInfo.Path})";
    }

    public Core.Mouse Connect()
    {
        return new Mouse(deviceInfo.Path);
    }
}