namespace Maus;

[GObject.Subclass<Gtk.Box>]
public partial class Selection
{
    private readonly Gtk.ListBox listBox = Gtk.ListBox.New();

    internal SelectionPresenter? Presenter { get; set; }
    
    public static Selection New()
    {
        return NewWithProperties([]);
    }

    partial void Initialize()
    {
        SetOrientation(Gtk.Orientation.Vertical);
        SetSpacing(5);

        listBox.SelectionMode = Gtk.SelectionMode.Single;

        listBox.OnRowActivated += ListBoxOnOnRowActivated;

        Append(Gtk.Label.New("Select device"));
        Append(listBox);
    }

    public void SetMouseInfos(IEnumerable<Core.MouseInfo> mouseInfos)
    {
        listBox.RemoveAll();

        foreach (var info in mouseInfos)
            listBox.Append(SelectionLabel.New(info));
    }

    private void ListBoxOnOnRowActivated(Gtk.ListBox sender, Gtk.ListBox.RowActivatedSignalArgs args)
    {
        var label = (SelectionLabel?)args.Row.Child;

        if (label is null)
            throw new Exception("Selected row does not have a child");

        Presenter?.SelectMouse(label.GetMouseInfo());
    }

    public override void Dispose()
    {
        listBox.OnRowActivated -= ListBoxOnOnRowActivated;

        base.Dispose();
    }
}