namespace Maus;

public class SelectionPresenter(Core.Connector connector, DetailPresenter detailPresenter)
{
    public void Attach(SelectionView view)
    {
        view.Presenter = this;
        view.SetMouseInfos(connector.Enumerate());
    }

    public void SelectMouse(Core.MouseInfo mouseInfo)
    {
        detailPresenter.Show(mouseInfo.Connect());
    }
}