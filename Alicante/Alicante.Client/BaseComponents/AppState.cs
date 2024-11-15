namespace Alicante.Client.BaseComponents;

public class AppState
{
    private int _tournamentId;

    public int TournamentId
    {
        get => _tournamentId;
        set
        {
            if (_tournamentId != value)
            {
                _tournamentId = value;
                StateHasChanged();
            }
        }
    }

    public event EventHandler StateChangedHandler;
    
    private void StateHasChanged()
    {
        StateChangedHandler?.Invoke(this, EventArgs.Empty);
    }
}
