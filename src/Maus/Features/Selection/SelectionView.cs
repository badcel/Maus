namespace Maus;

[GObject.Subclass<Gtk.Box>]
[Gtk.Template<Gtk.AssemblyResource>("selection.ui")]
public partial class SelectionView
{
    [Gtk.Connect] private Gtk.ListBox listBox;

    internal SelectionPresenter? Presenter { get; set; }

    partial void Initialize()
    {
        listBox.OnRowActivated += OnOnRowActivated;
    }

    public void SetMouseInfos(IEnumerable<Core.MouseInfo> mouseInfos)
    {
        listBox.RemoveAll();

        foreach (var info in mouseInfos)
            listBox.Append(SelectionLabel.New(info));
    }

    private void OnOnRowActivated(Gtk.ListBox sender, Gtk.ListBox.RowActivatedSignalArgs args)
    {
        var label = (SelectionLabel?)args.Row.Child;

        if (label is null)
            throw new Exception("Selected row does not have a child");

        Presenter?.SelectMouse(label.GetMouseInfo());
    }

    public override void Dispose()
    {
        listBox.OnRowActivated -= OnOnRowActivated;

        base.Dispose();
    }
}