namespace Maus;

[GObject.Subclass<Gtk.Label>]
public partial class SelectionLabel
{
    private Core.MouseInfo? _mouseInfo;
    
    public static SelectionLabel New(Core.MouseInfo mouseInfo)
    {
        var obj = NewWithProperties([]);
        obj._mouseInfo = mouseInfo;
        obj.SetText(mouseInfo.GetName());
        
        return obj;
    }
    
    public Core.MouseInfo GetMouseInfo() => _mouseInfo ?? throw new Exception("MouseInfo is not set");
}