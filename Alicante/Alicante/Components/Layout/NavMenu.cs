using Alicante.Client.Models;
using Alicante.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Newtonsoft.Json;


namespace Alicante.Components.Layout;
public partial class NavMenu
{
    private string? currentUrl;

    [Inject] public IRepository _repo { get; set; }
    public TournamentModel? activeTournament { get; set; }

    protected override void OnInitialized()
    {
        currentUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
        NavigationManager.LocationChanged += OnLocationChanged;
    }
    protected override async Task OnInitializedAsync()
    {
        activeTournament = await _repo.GetAciveTournament();
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        currentUrl = NavigationManager.ToBaseRelativePath(e.Location);
        StateHasChanged();
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;
    }
}