namespace Maus;

[GObject.Subclass<Gtk.Box>]
[Gtk.Template<Gtk.AssemblyResource>("selection.ui")]
public partial class Selection
{
    [Gtk.Connect]
    private Gtk.ListBox listBox;

    internal SelectionPresenter? Presenter { get; set; }
    
    public static Selection New()
    {
        return NewWithProperties([]);
    }

    partial void Initialize()
    {
        listBox.OnRowActivated += ListBoxOnOnRowActivated;
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