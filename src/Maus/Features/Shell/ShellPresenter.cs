namespace Maus;

public class ShellPresenter(SelectionPresenter selectionPresenter, DetailPresenter detailPresenter)
{
    private Shell? _view;
    
    public void Attach(Shell view)
    {
        selectionPresenter.Attach(view.Selection);
        detailPresenter.Attach(view.Detail);
        
        _view = view;
    }
}