using HidApi;

namespace Maus;

public class IntelliProInfo : MouseInfo
{
    private readonly DeviceInfo deviceInfo;

    public IntelliProInfo(DeviceInfo deviceInfo)
    {
        this.deviceInfo = deviceInfo;
    }

    public string GetName()
    {
        return $"{deviceInfo.ManufacturerString} {deviceInfo.ProductString} ({deviceInfo.Path})";
    }

    public Mouse Connect()
    {
        return new IntelliPro(deviceInfo.Path);
    }
}