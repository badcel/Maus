using GObject;

namespace Maus;

[Subclass<Gtk.Box>]
public partial class Detail
{
    private readonly Gtk.Entry dpiEntry = Gtk.Entry.New();
    private readonly Gtk.ColorDialogButton colorDialogButton = Gtk.ColorDialogButton.New(Gtk.ColorDialog.New());
    private readonly Adw.ComboRow pollingComboRow = Adw.ComboRow.New();
    private readonly Adw.ComboRow liftOffComboRow = Adw.ComboRow.New();
    
    internal DetailPresenter? Presenter { get; set; }

    public static Detail New()
    {
        return NewWithProperties([]);
    }
    
    public void ShowDpi(string dpi)
    {
        dpiEntry.SetText(dpi);
    }
    
    private void OnDpiChanged(GObject.Object obj, NotifySignalArgs args)
    {
        Presenter?.SetDpi(dpiEntry.GetText());
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
        /*pollingComboRow.SetModel(Gtk.StringList.New(pollingRates));
        pollingComboRow.SetSelected((uint)Array.IndexOf(pollingRates, newMouse.GetPollingRate().ToString()));
        Adw.ComboRow.SelectedPropertyDefinition.Notify(
            sender: pollingComboRow,
            signalHandler: SetPollingRate,
            after: false
        );*/

        liftOffComboRow.Title = "Lift off distance";
        liftOffComboRow.Subtitle = "in mm";
        /*liftOffComboRow.SetModel(Gtk.StringList.New(liftOffDistances));
        liftOffComboRow.SetSelected((uint)Array.IndexOf(liftOffDistances, newMouse.GetLiftOffDistance().ToString()));
        Adw.ComboRow.SelectedPropertyDefinition.Notify(
            sender: liftOffComboRow,
            signalHandler: SetLiftOffDistance,
            after: false
        );

        colorDialogButton.SetRgba(RgbaConverter.FromColor(newMouse.GetColor()));
        Gtk.ColorDialogButton.RgbaPropertyDefinition.Notify(
            sender: colorDialogButton,
            signalHandler: SetColor,
            after: false
        );*/
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
        /*
        Gtk.Editable.Text_PropertyDefinition.Unnotify(dpiEntry, SetDpi);
        Adw.ComboRow.SelectedPropertyDefinition.Unnotify(pollingComboRow, SetPollingRate);
        Adw.ComboRow.SelectedPropertyDefinition.Unnotify(liftOffComboRow, SetLiftOffDistance);
        Gtk.ColorDialogButton.RgbaPropertyDefinition.Unnotify(colorDialogButton, SetColor);
*/
        base.Dispose();
    }
}