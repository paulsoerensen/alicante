using Alicante.Client.Models;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Radzen.Blazor;
using System.Net.Http.Json;

namespace Alicante.Client.Pages.Result;
public partial class ResultList
{
    //[Parameter]
    public int matchId { get; set; } = 0;

    RadzenDataGrid<ResultViewModel> resultGrid;

    [Inject]
    public HttpClient http { get; set; }
    public IEnumerable<ResultViewModel> results { get; set; }
    public IEnumerable<ResultViewModel> persistedResults { get; set; }
    public IEnumerable<PlayerViewModel> players { get; set; }
    public IEnumerable<PlayerViewModel> filteredPlayers { get; set; }
    public IEnumerable<MatchViewModel> matches { get; set; }

    protected ResultViewModel insertedRow;

    public bool Editing { get; set; } = false;

    [Inject]
    private IToastService ToastService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        var res = await http.GetFromJsonAsync<BaseResponseModel>("/api/match");
        if (res != null && res.Success)
        {
            matches = JsonConvert.DeserializeObject<List<MatchViewModel>>(res.Data.ToString());
        }

        await LoadData();
    }

    #region Grid events

    async Task OnUpdateRow(ResultViewModel result)
    {
        await SaveRow(result);
    }
    async void OnCreateRow(ResultViewModel result)
    {
        //await UpdateRow(result);
        throw new NotImplementedException("OnCreateRow called");
    }

    async Task EditRow(ResultViewModel result)
    {
        results = persistedResults;
        await resultGrid.EditRow(result);
        Editing = true;
    }
    async Task SaveRow(ResultViewModel result)
    {
        ResultModel model = new()
        {
            MatchId = matchId,
            ResultId = result.ResultId,
            PlayerId = result.PlayerId,
            Birdies = result.Birdies,
            Hcp = result.Hcp,
            Par3 = result.Par3,
            Score = result.Score
        };
        var res = await http.PostAsJsonAsync<ResultModel>($"/api/result", model);
        if (res!.IsSuccessStatusCode)
        {
            ToastService.ShowSuccess("Resultat er opdateret");
            await LoadData();
        }
        else
        {
            resultGrid.CancelEditRow(result);
        }
        await resultGrid.Reload();
        Editing = false;
        filteredPlayers = players;
    }
    async Task DeleteRow(ResultViewModel result)
    {
        var res = await http.DeleteFromJsonAsync<BaseResponseModel>($"/api/result/{result.ResultId}");
        if (res!.Success)
        {
            ToastService.ShowSuccess("Resulten er slettet");
        }
        else
        {
            resultGrid.CancelEditRow(result);
        }
        await resultGrid.Reload();
    }
    async Task InsertRow()
    {
        insertedRow = new ResultViewModel();
        await resultGrid.InsertRow(insertedRow);
        Editing = true;
        filteredPlayers = players.Where(o => o.ResultId == null);
    }
    void CancelEdit(ResultViewModel result)
    {
        resultGrid.CancelEditRow(result);
        Editing = false;
        filteredPlayers = players;
    }
    #endregion

    void Reset()
    {
        results = persistedResults;
        Editing = false;
        filteredPlayers = players;
    }
    private void PlayerChanged(int playerId)
    {
        // Cast the value to the appropriate type (if needed)

        PlayerViewModel? model = players.SingleOrDefault(p => p.PlayerId == playerId);
        insertedRow.Hcp = model.Hcp;
    }

    protected async Task LoadData()
    {
        var res = await http.GetFromJsonAsync<BaseResponseModel>($"/api/result/match/{matchId}");
        if (res != null && res.Success)
        {
            persistedResults = JsonConvert.DeserializeObject<List<ResultViewModel>>(res.Data.ToString());
            results = persistedResults;
        }
        res = await http.GetFromJsonAsync<BaseResponseModel>($"/api/player/match/{matchId}");
        if (res != null && res.Success)
        {
            players = JsonConvert.DeserializeObject<List<PlayerViewModel>>(res.Data.ToString());
            filteredPlayers = players;
        }
    }

}

