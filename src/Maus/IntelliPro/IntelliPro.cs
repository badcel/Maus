using System.Diagnostics;
using System.Drawing;

namespace Maus;

public class IntelliPro(string path) : IDisposable, Mouse
{
    private enum Get : byte
    {
        Dpi = 0x97,
        Polling = 0x84,
        LiftOffDistance = 0xB6,
        Color = 0xB3
    }

    private enum Set : byte
    {
        Dpi = 0x96,
        Polling = 0x83,
        LiftOffDistance = 0xB8,
        Color = 0xB2
    }

    private readonly HidApi.Device device = new(path);

    public string GetName()
    {
        return $"{device.GetManufacturer()} {device.GetProduct()}";
    }

    public int GetDpi()
    {
        var data = SendRequest(Get.Dpi);
        return BitConverter.ToInt16(data);
    }

    public void SetDpi(int dpi)
    {
        if (dpi % 50 != 0)
            throw new ArgumentOutOfRangeException(nameof(dpi), dpi, "Please ensure that the dpi value is multiple of 50");

        if (dpi is < 200 or > 16000)
            throw new ArgumentOutOfRangeException(nameof(dpi), dpi, "Please ensure that the dpi value is greater or equal to 200 and smaller or equal to 16000");

        SendData(Set.Dpi, BitConverter.GetBytes((short)dpi));
    }

    public int GetPollingRate()
    {
        var data = SendRequest(Get.Polling);
        return data[0] switch
        {
            0 => 1000,
            1 => 500,
            2 => 125,
            _ => throw new Exception($"Unknown polling rate identifier: {data[0]}")
        };
    }

    public void SetPollingRate(int rate)
    {
        byte value = rate switch
        {
            1000 => 0,
            500 => 1,
            125 => 2,
            _ => throw new ArgumentOutOfRangeException(nameof(rate), rate, "The rate must be one of 125, 500, 1000")
        };

        SendData(Set.Polling, new[] { value });
    }

    public int GetLiftOffDistance()
    {
        var data = SendRequest(Get.LiftOffDistance);
        return data[0] switch
        {
            0 => 2,
            1 => 3,
            _ => throw new Exception($"Unknown lift off distance identifier: {data[0]}")
        };
    }

    public void SetLiftOffDistance(int distance)
    {
        byte value = distance switch
        {
            2 => 0,
            3 => 1,
            _ => throw new ArgumentOutOfRangeException(nameof(distance), distance, "The lift off distance must be one of 2 or 3")
        };

        SendData(Set.LiftOffDistance, [value]);
    }

    public Color GetColor()
    {
        var data = SendRequest(Get.Color);
        return Color.FromArgb(data[0], data[1], data[2]);
    }

    public void SetColor(Color color)
    {
        SendData(Set.Color, [color.R, color.G, color.B]);
    }

    private ReadOnlySpan<byte> SendRequest(Get get)
    {
        var sendData = new byte[73];
        sendData[0] = 0x24;
        sendData[1] = (byte)get;
        sendData[2] = 0x01;

        device.SendFeatureReport(sendData);
        var receiveData = device.GetInputReport(0x27, 0x29);
        var dataEnd = 4 + receiveData[3];
        return receiveData[4..dataEnd];
    }

    private void SendData(Set set, byte[] data)
    {
        Debug.Assert(data.Length <= 70, "Can't send more than 70 bytes of data");

        var sendData = new byte[73];
        sendData[0] = 0x24;
        sendData[1] = (byte)set;
        sendData[2] = (byte)data.Length;
        data.CopyTo(sendData, 3);

        device.SendFeatureReport(sendData);
    }

    public void Dispose()
    {
        device.Dispose();
    }
}
