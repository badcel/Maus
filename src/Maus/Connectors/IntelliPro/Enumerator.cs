using HidApi;

namespace Maus.Connectors.IntelliPro;

public static class Enumerator
{
    private const ushort VendorId = 0x45E;
    private const ushort ProductId = 0x82A;
    private const int UsagePage = 0xFF07;
    private const int Usage = 0x212;
    private const int Interface = 0x01;

    public static IEnumerable<Info> Enumerate()
    {
        return Hid
            .Enumerate(VendorId, ProductId)
            .Where(x => x is { InterfaceNumber: Interface, Usage: Usage, UsagePage: UsagePage })
            .Select(x => new Info(x));
    }
}
