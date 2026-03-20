namespace Maus;

[GObject.Subclass<Gtk.Box>]
public partial class DetailView
{
    private readonly Gtk.Entry dpiEntry = Gtk.Entry.New();
    private readonly Gtk.ColorDialogButton colorDialogButton = Gtk.ColorDialogButton.New(Gtk.ColorDialog.New());
    private readonly Adw.ComboRow pollingComboRow = Adw.ComboRow.New();
    private readonly Adw.ComboRow liftOffComboRow = Adw.ComboRow.New();

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
        SetOrientation(Gtk.Orientation.Vertical);
        SetSpacing(5);

        Gtk.Editable.Text_PropertyDefinition.Notify(
            sender: dpiEntry,
            signalHandler: OnDpiChanged,
            after: false
        );

        var dpiActionRow = Adw.ActionRow.New();
        dpiActionRow.Title = "Tracking speed";
        dpiActionRow.Subtitle = "in dots per inch (DPI)";
        dpiActionRow.AddSuffix(dpiEntry);
        dpiActionRow.SetActivatableWidget(dpiEntry);

        pollingComboRow.Title = "Polling rate";
        pollingComboRow.Subtitle = "in reports / second";
        Adw.ComboRow.SelectedPropertyDefinition.Notify(
            sender: pollingComboRow,
            signalHandler: OnPollingRateChanged,
            after: false
        );

        liftOffComboRow.Title = "Lift off distance";
        liftOffComboRow.Subtitle = "in mm";
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

        var colorActionRow = Adw.ActionRow.New();
        colorActionRow.Title = "Color";
        colorActionRow.AddSuffix(colorDialogButton);
        colorActionRow.SetActivatableWidget(colorDialogButton);

        var listBox = Gtk.ListBox.New();
        listBox.SelectionMode = Gtk.SelectionMode.None;
        listBox.Append(dpiActionRow);
        listBox.Append(pollingComboRow);
        listBox.Append(liftOffComboRow);
        listBox.Append(colorActionRow);

        Append(listBox);
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