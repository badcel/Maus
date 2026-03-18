namespace Maus.Core;

public class Connector
{
    public IEnumerable<MouseInfo> Enumerate()
    {
#if DEMO
        return Connectors.Demo.Enumerator.Enumerate();
#else
        return Connectors.IntelliPro.IntelliProEnumerator.Enumerate();
#endif
    }
}