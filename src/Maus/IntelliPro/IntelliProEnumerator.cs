using HidApi;

namespace Maus;

public static class IntelliProEnumerator
{
    private const ushort VenderId = 0x45E;
    private const ushort ProductId = 0x82A;
    private const int UsagePage = 0xFF07;
    private const int Usage = 0x212;
    private const int Interface = 0x01;

    public static IEnumerable<IntelliProInfo> Enumerate()
    {
        return Hid
            .Enumerate(VenderId, ProductId)
            .Where(x => x is { InterfaceNumber: Interface, Usage: Usage, UsagePage: UsagePage })
            .Select(x => new IntelliProInfo(x));
    }
}
