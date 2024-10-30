using Alicante.Client.Models;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Alicante.Client.Pages.Tournament;

public partial class TournamentUpsert
{
    public TournamentModel Model { get; set; } = new();

    [Parameter]
    public int? Id { get; set; }

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
            var res = await http.GetFromJsonAsync<BaseResponseModel>($"/api/Tournament/{Id}");
            if (res != null && res.Success)
            {
                Model = JsonConvert.DeserializeObject<TournamentModel>(res.Data.ToString());
            }
        }
    }

    protected void Cancel()
    {
        NavigationManager.NavigateTo($"/Tournament/{Id}");
    }

    public async Task Submit(TournamentModel arg)
    {
        var res = await http.PostAsJsonAsync("/api/Tournament", Model);
        if (res != null && res.IsSuccessStatusCode)
        {
            var res2 = JsonConvert.DeserializeObject<BaseResponseModel>(await res.Content.ReadAsStringAsync());
            if (res2 != null && res2.Success)
            {
                ToastService.ShowSuccess("Turnering oprettet");
                NavigationManager.NavigateTo("/Tournament");
            }
        }
    }
}

