using Blazored.Toast.Services;
using Alicante.Client.BaseComponents;
using Alicante.Client.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Alicante.Client.Pages.Match;
public partial class MatchList
{
    [Inject]
    public HttpClient http { get; set; }
    public List<MatchViewModel> matches { get; set; }
    public AppModal Modal { get; set; }
    public int DeleteID { get; set; }
    [Inject]
    private IToastService ToastService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var res = await http.GetFromJsonAsync<BaseResponseModel>("/api/match");
        if (res != null && res.Success)
        {
            matches = JsonConvert.DeserializeObject<List<MatchViewModel>>(res.Data.ToString());
        }
        await base.OnInitializedAsync();
    }
    protected async Task LoadProduct()
    {
        var res = await http.GetFromJsonAsync<BaseResponseModel>("/api/match");
        if (res != null && res.Success)
        {
            matches = JsonConvert.DeserializeObject<List<MatchViewModel>>(res.Data.ToString());
        }
    }
    protected async Task HandleDelete()
    {
        var res = await http.DeleteAsync($"/api/match/{DeleteID}");
        if (res != null)
        {
            ToastService.ShowSuccess("Matchen er droppet");
            await LoadProduct();
            Modal.Close();
        }
    }
}

