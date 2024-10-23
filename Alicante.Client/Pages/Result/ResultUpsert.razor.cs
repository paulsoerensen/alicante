using Alicante.Client.Models;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Alicante.Client.Pages.Result;

public partial class ResultUpsert
{
    public ResultViewModel Model { get; set; } = new();
    /// <summary>
    /// Spillere i matchen, der endnu ikke har registreret result
    /// </summary>
    public List<PlayerViewModel> players { get; set; } = new();

    private bool EditVisible => Model.PlayerId != 0;

    [Parameter]
    public int? Id { get; set; }

    [Parameter]
    public int? matchId { get; set; }

    [Inject]
    public HttpClient http { get; set; }
    [Inject]
    private IToastService ToastService { get; set; }
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {

        if (Id != null)
        {
            var res = await http.GetFromJsonAsync<BaseResponseModel>($"/api/result/{Id}");
            if (res != null && res.Success)
            {
                Model = JsonConvert.DeserializeObject<ResultViewModel>(res.Data.ToString());
                matchId = Model.MatchId;
            }
        }
        if (matchId != null)
        {
            var res = await http.GetFromJsonAsync<BaseResponseModel>($"/api/player/match/{matchId}");
            if (res != null && res.Success)
            {
                players = JsonConvert.DeserializeObject<List<PlayerViewModel>>(res.Data.ToString());
            }
        }    
    }


    protected void PlayerChanged()
    {
        var p = players.SingleOrDefault(p => p.PlayerId == Model.PlayerId);
        if (p != null)
        {
            Model = new ResultViewModel()
            {
                PlayerId = p.PlayerId,
                PlayerName = p.PlayerName,
                Hcp = p.Hcp
            };
        }
    }

    protected void Cancel()
    {
        NavigationManager.NavigateTo($"/result/match/{matchId}");
    }

    public async Task Submit(ResultViewModel arg)
    {
        ResultModel r = new ResultModel()
        {
            MatchId = matchId.Value,
            PlayerId = arg.PlayerId,
            Score = arg.Score,
            Birdies = arg.Birdies,
            Hcp = arg.Hcp,
            Par3 = arg.Par3
        };
        var res = await http.PostAsJsonAsync("/api/result", r);
        if (res != null && res.IsSuccessStatusCode)
        {
            var res2 = JsonConvert.DeserializeObject<BaseResponseModel>(await res.Content.ReadAsStringAsync());
            if (res2 != null && res2.Success)
            {
                ToastService.ShowSuccess("resultat oprettet");
                NavigationManager.NavigateTo($"/result/match/{matchId}");
            }
            else
            {
                ToastService.ShowError(res2?.ErrorMessage);
            }
        }
    }
}

