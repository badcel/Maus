using System.Diagnostics;

namespace Maus;

[GObject.Subclass<Gtk.Box>]
public partial class SelectMouseView
{
    private readonly IEnumerable<MouseInfo>? mouseInfos;
    private readonly Action<MouseInfo>? showMouse;
    private readonly Gtk.ListBox listBox = Gtk.ListBox.New();

    public SelectMouseView(IEnumerable<MouseInfo> mouseInfos, Action<MouseInfo> showMouse) : this()
    {
        this.mouseInfos = mouseInfos;
        this.showMouse = showMouse;

        CreateUi();
    }

    private void CreateUi()
    {
        Debug.Assert(mouseInfos is not null, $"{nameof(SelectMouseView)} is not initialized.");

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
        Debug.Assert(mouseInfos is not null, $"{nameof(SelectMouseView)} is not initialized.");
        Debug.Assert(showMouse is not null, $"{nameof(SelectMouseView)} is not initialized.");

        var label = (Gtk.Label?)args.Row.Child;

        if (label is null)
            throw new Exception("Selected row does not have a child");

        var intelliProInfo = mouseInfos.First(x => x.GetName() == label.GetText());
        showMouse(intelliProInfo);
    }

    public override void Dispose()
    {
        listBox.OnRowActivated -= ListBoxOnOnRowActivated;

        base.Dispose();
    }
}