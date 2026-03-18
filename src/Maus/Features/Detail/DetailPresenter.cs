namespace Maus;

public class DetailPresenter
{
    private Detail? _view;
    private Core.Mouse? _mouse;

    public void Attach(Detail view)
    {
        _view = view;
        _view.Presenter = this;
    }

    public void Show(Core.Mouse mouse)
    {
        _mouse = mouse;

        _view?.ShowDpi(mouse.GetDpi());
        _view?.ShowColor(mouse.GetColor());
        _view?.ShowPollingRates(
            pollingRates: mouse.GetPollingRates(), 
            index: (uint) mouse.GetPollingRateIndex()
        );
        
        _view?.ShowLiftOffDistances(
            liftOffDistances: mouse.GetLiftOffDistances(),
            index: (uint)mouse.GetLiftOffDistanceIndex()
        );
    }

    public void SetDpi(int dpi)
    { 
        _mouse?.SetDpi(dpi);
    }

    public void SetPollingRateIndex(int pollingRateIndex)
    {
        _mouse?.SetPollingRateIndex(pollingRateIndex);
    }

    public void SetLiftOffDistanceIndex(int distanceIndex)
    {
        _mouse?.SetLiftOffDistanceIndex(distanceIndex);
    }

    public void SetColor(System.Drawing.Color color)
    {
        _mouse?.SetColor(color);
    }
}