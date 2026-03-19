namespace Maus.Core;

public class Connector
{
    public IEnumerable<MouseInfo> Enumerate()
    {
        foreach(var demo in Connectors.Demo.Enumerator.Enumerate())
            yield return demo;

        foreach(var intelliPro in Connectors.IntelliPro.Enumerator.Enumerate())
            yield return intelliPro;
    }
}