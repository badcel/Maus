namespace Maus;

public class ShellPresenter(SelectionPresenter selectionPresenter, DetailPresenter detailPresenter)
{
    public void Attach(ShellView view)
    {
        selectionPresenter.Attach(view.Selection);
        detailPresenter.Attach(view.Detail);
    }
}