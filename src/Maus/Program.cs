using HidApi;
using Maus;

var application = Adw.Application.New("org.Maus", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    var window = Gtk.ApplicationWindow.New((Adw.Application)sender);

    void Show(MouseInfo info)
    {
        if (window.Child is IDisposable d)
            d.Dispose();

        window.SetChild(new MouseView(info.Connect()));
    }

#if DEMO
    var mouseInfos = DemoEnumerator.Enumerate();
#else
    var mouseInfos = IntelliProEnumerator.Enumerate();
#endif
    window.Title = "Window";
    window.SetDefaultSize(300, 300);
    window.SetChild(new SelectMouseView(mouseInfos, Show));
    window.Show();
};

var exitCode = application.RunWithSynchronizationContext(null);

Hid.Exit();

return exitCode;
