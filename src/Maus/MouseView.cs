using System.Diagnostics.CodeAnalysis;

namespace Maus;

public class MouseView : Gtk.Box
{
    private static readonly string[] pollingRates = { "1000", "500", "125" };
    private static readonly string[] liftOffDistances = { "2", "3" };
 
    private readonly Mouse mouse;
    private Gtk.Entry dpiEntry;
    private Gtk.ColorDialogButton colorDialogButton;
    private Adw.ComboRow pollingComboRow;
    private Adw.ComboRow liftOffComboRow;

    public MouseView(Mouse mouse)
    {
        this.mouse = mouse;
        
        CreateUi();
    }

    private void SetDpi(GObject.Object obj, NotifySignalArgs args)
    {
        mouse.SetDpi(int.Parse(dpiEntry.GetText()));
    }

    private void SetPollingRate(GObject.Object obj, NotifySignalArgs args)
    {
        mouse.SetPollingRate(int.Parse(pollingRates[pollingComboRow.Selected]));
    }

    private void SetLiftOffDistance(GObject.Object obj, NotifySignalArgs args)
    {
        mouse.SetLiftOffDistance(int.Parse(liftOffDistances[liftOffComboRow.Selected]));
    }
    
    private void SetColor(GObject.Object obj, NotifySignalArgs args)
    {
        var rgba = colorDialogButton.GetRgba();
        mouse.SetColor(RgbaConverter.ToColor(rgba));
    }

    [MemberNotNull(nameof(dpiEntry))]
    [MemberNotNull(nameof(colorDialogButton))]
    [MemberNotNull(nameof(pollingComboRow))]
    [MemberNotNull(nameof(liftOffComboRow))]
    private void CreateUi()
    {
        SetOrientation(Gtk.Orientation.Vertical);
        SetSpacing(5);

        dpiEntry = Gtk.Entry.New();
        dpiEntry.SetText(mouse.GetDpi().ToString());
        NotifySignal.Connect(
            sender: dpiEntry, 
            signalHandler: SetDpi,
            after: false,
            detail: Gtk.Editable.Text_PropertyDefinition.UnmanagedName
        );

        var dpiActionRow = Adw.ActionRow.New();
        dpiActionRow.Title = "Tracking speed";
        dpiActionRow.Subtitle = "in dots per inch (DPI)";
        dpiActionRow.AddSuffix(dpiEntry);
        dpiActionRow.SetActivatableWidget(dpiEntry);

        pollingComboRow = Adw.ComboRow.New();
        pollingComboRow.Title = "Polling rate";
        pollingComboRow.Subtitle = "in reports / second";
        pollingComboRow.SetModel(Gtk.StringList.New(pollingRates));
        pollingComboRow.SetSelected((uint) Array.IndexOf(pollingRates, mouse.GetPollingRate().ToString()));
        NotifySignal.Connect(
            sender: pollingComboRow,
            signalHandler: SetPollingRate,
            after: false,
            detail: Adw.ComboRow.SelectedPropertyDefinition.UnmanagedName
        );
        
        liftOffComboRow = Adw.ComboRow.New();
        liftOffComboRow.Title = "Lift off distance";
        liftOffComboRow.Subtitle = "in mm";
        liftOffComboRow.SetModel(Gtk.StringList.New(liftOffDistances));
        liftOffComboRow.SetSelected((uint) Array.IndexOf(liftOffDistances, mouse.GetLiftOffDistance().ToString()));
        NotifySignal.Connect(
            sender: liftOffComboRow,
            signalHandler: SetLiftOffDistance,
            after: false,
            detail: Adw.ComboRow.SelectedPropertyDefinition.UnmanagedName
        );

        colorDialogButton = Gtk.ColorDialogButton.New(Gtk.ColorDialog.New());
        colorDialogButton.SetRgba(RgbaConverter.FromColor(mouse.GetColor()));
        NotifySignal.Connect(
            sender: colorDialogButton,
            signalHandler: SetColor,
            after: false,
            detail: Gtk.ColorDialogButton.RgbaPropertyDefinition.UnmanagedName
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
        NotifySignal.Disconnect(dpiEntry, SetDpi);
        NotifySignal.Disconnect(pollingComboRow, SetPollingRate);
        NotifySignal.Disconnect(liftOffComboRow, SetLiftOffDistance);
        NotifySignal.Disconnect(colorDialogButton, SetColor);

        base.Dispose();
    }
}