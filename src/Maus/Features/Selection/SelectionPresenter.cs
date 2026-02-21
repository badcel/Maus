namespace Maus;

public class SelectionPresenter(Core.Connector connector, DetailPresenter detailPresenter)
{
    private Selection? _view;

    public void Attach(Selection view)
    {
        _view = view;
        _view.Presenter = this;
        _view.SetMouseInfos(connector.Enumerate());
    }

    public void SelectMouse(Core.MouseInfo mouseInfo)
    {
        detailPresenter.Show(mouseInfo.Connect());
    }
}