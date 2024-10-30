using Alicante.Client.BaseComponents;
using Alicante.Client.Models;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Json;


namespace Alicante.Client.Pages.Result;
public partial class ResultList
{
    [Inject]
    public HttpClient http { get; set; }
    public List<KeyValuePair<int, string>>? Matches { get; set; }
    public List<ResultViewModel>? results { get; set; }
    public AppModal Modal { get; set; }
    
    [Parameter]
    public int? matchId { get; set; }

    public int DeleteID { get; set; }
    [Inject]
    private IToastService ToastService { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var res = await http.GetFromJsonAsync<BaseResponseModel>("/api/match/items");
        if (res != null && res.Success)
        {
            Matches = JsonConvert.DeserializeObject<List<KeyValuePair<int, string>>>(res.Data.ToString());
        }
        await base.OnInitializedAsync();
        await LoadData();
    }

	
    protected async Task LoadData()
    {
        if (matchId != null)
        {
            var res = await http.GetFromJsonAsync<BaseResponseModel>($"/api/result/match/{matchId}");
            if (res != null && res.Success)
            {
                results = JsonConvert.DeserializeObject<List<ResultViewModel>>(res.Data.ToString());
                StateHasChanged();
            }
        }
    }

    protected async Task MatchChanged()
    {
        await LoadData();
    }
    

    protected async Task HandleDelete()
    {
        var res = await http.DeleteAsync($"/api/result/{DeleteID}");
        if (res != null)
        {
            ToastService.ShowSuccess("Spilleren er smidt ud");
            await LoadData();
            Modal.Close();
        }
    }
}

