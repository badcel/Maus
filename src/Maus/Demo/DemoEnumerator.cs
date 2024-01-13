namespace Maus;

public static class DemoEnumerator
{
    public static IEnumerable<MouseInfo> Enumerate()
    {
        yield return new DemoInfo();
    }
}