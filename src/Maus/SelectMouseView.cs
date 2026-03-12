namespace Maus;

[GObject.Subclass<Gtk.Box>]
public partial class SelectMouseView
{
    private readonly Gtk.ListBox listBox = Gtk.ListBox.New();
    
    private IEnumerable<MouseInfo>? mouseInfos;
    private Action<MouseInfo>? showMouse;
    
    private IEnumerable<MouseInfo> MouseInfos => mouseInfos ?? throw new Exception($"{nameof(mouseInfos)} is null");
    private Action<MouseInfo> ShowMouse => showMouse ?? throw new Exception($"{nameof(showMouse)} is null");
    
    public static SelectMouseView New(IEnumerable<MouseInfo> mouseInfos, Action<MouseInfo> showMouse)
    {
        var selectMouseView = NewWithProperties([]);
        selectMouseView.mouseInfos = mouseInfos;
        selectMouseView.showMouse = showMouse;

        selectMouseView.CreateUi();

        return selectMouseView;
    }

    private void CreateUi()
    {
        SetOrientation(Gtk.Orientation.Vertical);
        SetSpacing(5);

        listBox.SelectionMode = Gtk.SelectionMode.Single;

        foreach (var intelliProInfo in mouseInfos)
            listBox.Append(Gtk.Label.New(intelliProInfo.GetName()));

        listBox.OnRowActivated += ListBoxOnOnRowActivated;

        Append(Gtk.Label.New("Select device"));
        Append(listBox);
    }

    private void ListBoxOnOnRowActivated(Gtk.ListBox sender, Gtk.ListBox.RowActivatedSignalArgs args)
    {
        var label = (Gtk.Label?)args.Row.Child;

        if (label is null)
            throw new Exception("Selected row does not have a child");

        var intelliProInfo = MouseInfos.First(x => x.GetName() == label.GetText());
        ShowMouse(intelliProInfo);
    }

    public override void Dispose()
    {
        listBox.OnRowActivated -= ListBoxOnOnRowActivated;

        base.Dispose();
    }
}