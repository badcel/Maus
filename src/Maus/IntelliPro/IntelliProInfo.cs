using HidApi;

namespace Maus;

public class IntelliProInfo(DeviceInfo deviceInfo) : MouseInfo
{
    public string GetName()
    {
        return $"{deviceInfo.ManufacturerString} {deviceInfo.ProductString} ({deviceInfo.Path})";
    }

    public Mouse Connect()
    {
        return new IntelliPro(deviceInfo.Path);
    }
}