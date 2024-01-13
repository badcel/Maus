namespace Maus;

public class SelectMouseView : Gtk.Box
{
    private readonly IEnumerable<MouseInfo> mouseInfos;
    private readonly Action<MouseInfo> showMouse;
    private Gtk.ListBox? listBox;

    public SelectMouseView(IEnumerable<MouseInfo> mouseInfos, Action<MouseInfo> showMouse)
    {
        this.mouseInfos = mouseInfos;
        this.showMouse = showMouse;

        CreateUi();
    }

    private void CreateUi()
    {
        SetOrientation(Gtk.Orientation.Vertical);
        SetSpacing(5);

        listBox = Gtk.ListBox.New();
        listBox.SelectionMode = Gtk.SelectionMode.Single;

        foreach (var intelliProInfo in mouseInfos)
            listBox.Append(Gtk.Label.New(intelliProInfo.GetName()));

        listBox.OnRowActivated += ListBoxOnOnRowActivated;

        Append(Gtk.Label.New("Select device"));
        Append(listBox);
    }

    private void ListBoxOnOnRowActivated(Gtk.ListBox sender, Gtk.ListBox.RowActivatedSignalArgs args)
    {
        var label = (Gtk.Label?) args.Row.Child;

        if (label is null)
            throw new Exception("Selected row does not have a child");
        
        var intelliProInfo = mouseInfos.First(x => x.GetName() == label.GetText());
        showMouse(intelliProInfo);
    }

    public override void Dispose()
    {
        if(listBox is not null)
            listBox.OnRowActivated -= ListBoxOnOnRowActivated;
        
        base.Dispose();
    }
}