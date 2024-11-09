using Alicante.Client.BaseComponents;
using Alicante.Client.Models;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Radzen;
using Radzen.Blazor;
using System.Net.Http.Json;
using System.Reflection;

namespace Alicante.Client.Pages.Tournament;
   
public partial class TournamentList
{
    RadzenDataGrid<TournamentModel> tournamentGrid;
    private DeleteConfirmationDialog deleteDialog;

    [Inject] public HttpClient http { get; set; }
    [Inject] private IToastService ToastService { get; set; }
    [Inject] private DialogService DialogService { get; set; } = default!;

    public List<TournamentModel> tournaments { get; set; }
    public int activeId { get; set; }
    public int deleteId { get; set; }
    public bool Editing { get; set; } = false;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await LoadData();
        activeId = tournaments.Where(x => x.Active == true).Select(x => x.TournamentId).SingleOrDefault();
        StateHasChanged();
    }

    #region Grid events

    async Task OnUpdateRow(TournamentModel tournament)
    {
        await SaveRow(tournament);
    }
    async void OnCreateRow(TournamentModel tournament)
    {
        //await UpdateRow(tournament);
        throw new NotImplementedException("OnCreateRow called");
    }

    async Task EditRow(TournamentModel tournament)
    {
        await tournamentGrid.EditRow(tournament);
        Editing = true;
    }
    async Task SaveRow(TournamentModel model)
    {
        var res = await http.PostAsJsonAsync($"/api/tournament/", model);
        if (res!.IsSuccessStatusCode)
        {
            ToastService.ShowSuccess("Tournamenten er opdateret");
            await LoadData();
        }
        else
        {
            tournamentGrid.CancelEditRow(model);
        }
        await tournamentGrid.Reload();
        Editing = false;
    }
    async Task DeleteRow(TournamentModel model)
    {
        // Trigger the dialog to show
        deleteId = model.TournamentId;        
        deleteDialog.Show(model.TournamentName);
    }

    private async Task HandleDeleteConfirmation()
    {
        var res = await http.DeleteFromJsonAsync<BaseResponseModel>($"/api/tournament/{deleteId}");
        if (res!.Success)
        {
            ToastService.ShowSuccess("Turnering er slettet");
        }
        //else
        //{
        //    tournamentGrid.CancelEditRow(model);
        //}
        await tournamentGrid.Reload();
    }

    async Task InsertRow()
    {
        var tournament = new TournamentModel();
        await tournamentGrid.InsertRow(tournament);
        Editing = true;
    }
    void CancelEdit(TournamentModel tournament)
    {
        tournamentGrid.CancelEditRow(tournament);
        Editing = false;
    }
    #endregion

    async Task ChangeActive() 
    {
        var res = await http.PostAsJsonAsync("/api/Tournament/active", activeId);
        if (res!.IsSuccessStatusCode)
        {
            ToastService.ShowSuccess("Det er nu den aktive turnering");
        }
    }
    void Reset()
    {
        Editing = false;
    }

    protected async Task LoadData()
    {
        var res = await http.GetFromJsonAsync<BaseResponseModel>("/api/Tournament");
        if (res != null && res.Success)
        {
            tournaments = JsonConvert.DeserializeObject<List<TournamentModel>>(res.Data.ToString());
        }
    }
}

