using Alicante.Client.BaseComponents;
using Alicante.Client.Models;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Alicante.Client.Pages.Tournament;
   
public partial class TournamentList
{
    [Inject]
    public HttpClient http { get; set; }
    public List<TournamentModel> tournaments { get; set; }
    public AppModal Modal { get; set; }
    public int DeleteID { get; set; }
    [Inject]
    private IToastService ToastService { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await LoadData();
    }

    protected async Task LoadData()
    {
        var res = await http.GetFromJsonAsync<BaseResponseModel>("/api/Tournament");
        if (res != null && res.Success)
        {
            tournaments = JsonConvert.DeserializeObject<List<TournamentModel>>(res.Data.ToString());
        }
    }
    protected async Task HandleDelete()
    {
        var res = await http.DeleteAsync($"/api/tournament/{DeleteID}");
        if (res != null)
        {
            ToastService.ShowSuccess("Turneringen er smidt ud");
            await LoadData();
            Modal.Close();
        }
    }
}
