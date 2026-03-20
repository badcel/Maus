namespace Maus;

public static class RgbaConverter
{
    public static Gdk.RGBA FromColor(System.Drawing.Color color)
    {
        return new Gdk.RGBA
        {
            Red = color.R / 256.0f,
            Green = color.G / 256.0f,
            Blue = color.B / 256.0f,
            Alpha = color.A / 256.0f
        };
    }

    public static System.Drawing.Color ToColor(Gdk.RGBA rgba)
    {
        return System.Drawing.Color.FromArgb(
            alpha: Math.Clamp(Convert.ToInt32(rgba.Alpha * 256), 0, 255),
            red: Math.Clamp(Convert.ToInt32(rgba.Red * 256), 0, 255),
            green: Math.Clamp(Convert.ToInt32(rgba.Green * 256), 0, 255),
            blue: Math.Clamp(Convert.ToInt32(rgba.Blue * 256), 0, 255)
        );
    }
}