using Blazored.Toast.Services;
using Alicante.Client.BaseComponents;
using Alicante.Client.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Radzen.Blazor;
using System.Text.RegularExpressions;

namespace Alicante.Client.Pages.Match;
public partial class MatchList
{

    RadzenDataGrid<MatchViewModel> matchGrid;

    [Inject]
    public HttpClient http { get; set; }
    public List<MatchViewModel> matches { get; set; }
    public List<MatchViewModel> persistedMatches { get; set; }
    public List<CourseModel> courses { get; set; }

    public bool Editing { get; set; } = false;

    [Inject]
    private IToastService ToastService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        var res = await http.GetFromJsonAsync<BaseResponseModel>("/api/Course");
        if (res != null && res.Success)
        {
            courses = JsonConvert.DeserializeObject<List<CourseModel>>(res.Data.ToString());
        }
        await LoadData();
    }

    #region Grid events

    async Task OnUpdateRow(MatchViewModel match)
    {
        await SaveRow(match);
    }
    async void OnCreateRow(MatchViewModel match)
    {
        //await UpdateRow(match);
        throw new NotImplementedException("OnCreateRow called");
    }

    async Task EditRow(MatchViewModel match)
    {
        matches = persistedMatches;
        await matchGrid.EditRow(match);
        Editing = true;
    }
    async Task SaveRow(MatchViewModel match)
    {
        MatchModel model = new()
        {
            MatchId = match.MatchId,
            CourseId = match.CourseId,
            MatchDate = match.MatchDate,
            TournamentId = match.TournamentId
        };
        var res = await http.PostAsJsonAsync<MatchModel>($"/api/match/", model);
        if (res!.IsSuccessStatusCode)
        {
            ToastService.ShowSuccess("Matchen er opdateret");
            await LoadData();
        }
        else
        {
            matchGrid.CancelEditRow(match);
        }
        await matchGrid.Reload();
        Editing = false;
    }
    async Task DeleteRow(MatchViewModel match)
    {
        var res = await http.DeleteFromJsonAsync<BaseResponseModel>($"/api/match/{match.MatchId}");
        if (res!.Success)
        {
            ToastService.ShowSuccess("Matchen er slettet");
            var matchToRemove = persistedMatches.FirstOrDefault(m => m.MatchId == match.MatchId);
            if (matchToRemove != null)
            {
                persistedMatches.Remove(matchToRemove);
                matches = persistedMatches;
            }
        }
        else
        {
            matchGrid.CancelEditRow(match);
        }
        await matchGrid.Reload();
    }
    async Task InsertRow()
    {
        var match = new MatchViewModel();
        await matchGrid.InsertRow(match);
        Editing = true;
    }
    void CancelEdit(MatchViewModel match)
    {
        matchGrid.CancelEditRow(match);
        Editing = false;
    }
    #endregion

    void Reset()
    {
        matches = persistedMatches;
        Editing = false;
    }

    protected async Task LoadData()
    {
        var res = await http.GetFromJsonAsync<BaseResponseModel>("/api/match");
        if (res != null && res.Success)
        {
            persistedMatches = JsonConvert.DeserializeObject<List<MatchViewModel>>(res.Data.ToString());
            matches = persistedMatches;
        }
    }
}

