namespace Maus;

[GObject.Subclass<Gtk.Box>]
[Gtk.Template<Gtk.AssemblyResource>("detail.ui")]
public partial class DetailView
{
    [Gtk.Connect] private Gtk.Entry dpiEntry;
    [Gtk.Connect] private Adw.ComboRow pollingComboRow;
    [Gtk.Connect] private Adw.ComboRow liftOffComboRow;
    [Gtk.Connect] private Gtk.ColorDialogButton colorDialogButton;

    internal DetailPresenter? Presenter { get; set; }

    public void ShowDpi(int dpi)
    {
        dpiEntry.SetText(dpi.ToString());
    }

    public void ShowColor(System.Drawing.Color color)
    {
        colorDialogButton.SetRgba(RgbaConverter.FromColor(color));
    }

    public void ShowPollingRates(int[] pollingRates, uint index)
    {
        var model = Gtk.StringList.New(pollingRates.Select(x => x.ToString()).ToArray());
        pollingComboRow.Model = model;
        pollingComboRow.SetSelected(index);
    }

    public void ShowLiftOffDistances(int[] liftOffDistances, uint index)
    {
        var model = Gtk.StringList.New(liftOffDistances.Select(x => x.ToString()).ToArray());
        liftOffComboRow.Model = model;
        liftOffComboRow.SetSelected(index);
    }

    private void OnDpiChanged(GObject.Object obj, NotifySignalArgs args)
    {
        if (int.TryParse(dpiEntry.GetText(), out var i))
            Presenter?.SetDpi(i);
    }

    private void OnPollingRateChanged(Object sender, NotifySignalArgs args)
    {
        if (sender is Adw.ComboRow comboRow)
            Presenter?.SetPollingRateIndex((int)comboRow.GetSelected());
    }

    private void OnLiftOffDistanceChanged(GObject.Object sender, NotifySignalArgs args)
    {
        if (sender is Adw.ComboRow comboRow)
            Presenter?.SetLiftOffDistanceIndex((int)comboRow.GetSelected());
    }

    private void OnColorChanged(GObject.Object sender, NotifySignalArgs args)
    {
        var rgba = colorDialogButton.GetRgba();
        Presenter?.SetColor(RgbaConverter.ToColor(rgba));
    }

    partial void Initialize()
    {
        Gtk.Editable.Text_PropertyDefinition.Notify(
            sender: dpiEntry,
            signalHandler: OnDpiChanged,
            after: false
        );

        Adw.ComboRow.SelectedPropertyDefinition.Notify(
            sender: pollingComboRow,
            signalHandler: OnPollingRateChanged,
            after: false
        );

        Adw.ComboRow.SelectedPropertyDefinition.Notify(
            sender: liftOffComboRow,
            signalHandler: OnLiftOffDistanceChanged,
            after: false
        );

        Gtk.ColorDialogButton.RgbaPropertyDefinition.Notify(
            sender: colorDialogButton,
            signalHandler: OnColorChanged,
            after: false
        );
    }

    public override void Dispose()
    {
        Gtk.Editable.Text_PropertyDefinition.Unnotify(dpiEntry, OnDpiChanged);
        Adw.ComboRow.SelectedPropertyDefinition.Unnotify(pollingComboRow, OnPollingRateChanged);
        Adw.ComboRow.SelectedPropertyDefinition.Unnotify(liftOffComboRow, OnLiftOffDistanceChanged);
        Gtk.ColorDialogButton.RgbaPropertyDefinition.Unnotify(colorDialogButton, OnColorChanged);

        base.Dispose();
    }
}