using GObject;

namespace Maus;

[Subclass<Gtk.ApplicationWindow>]
public partial class Shell
{
    public Selection Selection { get; } = Selection.New();
    public Detail Detail { get; } = Detail.New();

    public static Shell New(ShellPresenter presenter)
    {
        var obj = NewWithProperties([]);
        presenter.Attach(obj);
    
        return obj;
    }
    
    partial void Initialize()
    {
        SetTitle("Maus");
        SetDefaultSize(300, 300);
        SetChild();
    }

    private void SetChild()
    {
        var content = Adw.NavigationPage.New(Detail, "Maus");
        var sidebar = Adw.NavigationPage.New(Selection, "Select");
        
        var splitView = Adw.NavigationSplitView.New();
        splitView.SetSidebar(sidebar);
        splitView.SetContent(content);
        
        SetChild(splitView);
    }
}