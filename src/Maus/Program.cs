using HidApi;
using Maus;

Adw.Module.Initialize();
GirCore.Integration.Initialize();

var application = Adw.Application.New("org.Maus", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    var detailViewPresenter = new DetailPresenter();
    var selectionViewPresenter = new SelectionPresenter(new Maus.Core.Connector(), detailViewPresenter);
    var shellPresenter = new ShellPresenter(selectionViewPresenter, detailViewPresenter);
 
    var shell = ShellView.New(shellPresenter);
    shell.SetApplication(application);
    shell.Show();
};

var exitCode = application.RunWithSynchronizationContext(null);

Hid.Exit();

return exitCode;
