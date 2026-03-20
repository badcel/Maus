using GObject;

namespace Maus;

[Subclass<Gtk.ApplicationWindow>]
[Gtk.Template<Gtk.AssemblyResource>("shell.ui")]
public partial class ShellView
{
    [Gtk.Connect] private SelectionView selection;
    [Gtk.Connect] private DetailView detail;

    public SelectionView Selection => selection;
    public DetailView Detail => detail;

    public static ShellView New(ShellPresenter presenter)
    {
        var obj = NewWithProperties([]);
        presenter.Attach(obj);

        return obj;
    }
}