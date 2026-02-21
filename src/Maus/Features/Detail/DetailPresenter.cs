namespace Maus;

public class DetailPresenter
{
    private Detail? _view;

    private Core.Mouse? mouse;
    
    public void Attach(Detail view)
    {
        _view = view;
        _view.Presenter = this;
    }

    public void Show(Core.Mouse mouse)
    {
        this.mouse = mouse;

        _view?.ShowDpi(mouse.GetDpi().ToString());
    }

    public void SetDpi(string dpi)
    {
        mouse?.SetDpi(int.Parse(dpi));
    }
}