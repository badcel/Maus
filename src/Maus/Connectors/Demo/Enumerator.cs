namespace Maus.Connectors.Demo;

public static class Enumerator
{
    public static IEnumerable<Core.MouseInfo> Enumerate()
    {
        yield return new Info();
    }
}