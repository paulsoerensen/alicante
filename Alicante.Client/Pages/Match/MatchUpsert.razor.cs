using Alicante.Client.Models;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Alicante.Client.Pages.Match;

public partial class MatchUpsert
{
    public MatchModel Model { get; set; } = new();
    public List<CourseModel> courses { get; set; }

    public int tournamentId { get; set; } = 1;

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
        var res = await http.GetFromJsonAsync<BaseResponseModel>("/api/Course");
        if (res != null && res.Success)
        {
            courses = JsonConvert.DeserializeObject<List<CourseModel>>(res.Data.ToString());
        }

        if (Id == null)
        {
            Model.TournamentId = tournamentId;
            Model.MatchDate = DateTime.Now.AddDays(1);
        }
        else { 
            res = await http.GetFromJsonAsync<BaseResponseModel>($"/api/Match/{Id}");
            if (res != null && res.Success)
            {
                Model = JsonConvert.DeserializeObject<MatchModel>(res.Data.ToString());
            }
        }
    }

    public async Task Submit(MatchModel arg)
    {
        arg.TournamentId = tournamentId;
        var res = await http.PostAsJsonAsync("/api/Match", arg);
        if (res != null && res.IsSuccessStatusCode)
        {
            var res2 = JsonConvert.DeserializeObject<BaseResponseModel>(await res.Content.ReadAsStringAsync());
            if (res2?.Success == true)
            {
                ToastService.ShowSuccess("Matchen oprettet");
            }
            else
            {
                ToastService.ShowError($"Opdatering fejlede: {res2.ErrorMessage}");
            }
            NavigationManager.NavigateTo("/Match");
        }
    }

    void Cancel()
    {
        NavigationManager.NavigateTo("/Match");
    }

    //public async Task Submit()
    //{
    //    Model.TournamentId = tournamentId;
    //    var res = await http.PostAsJsonAsync("/api/Match", Model);
    //    if (res != null && res.IsSuccessStatusCode)
    //    {
    //        var res2 = JsonConvert.DeserializeObject<BaseResponseModel>(await res.Content.ReadAsStringAsync());
    //        if (res2?.Success == true)
    //        {
    //            ToastService.ShowSuccess("Matchen oprettet");
    //        }
    //        else
    //        {
    //            ToastService.ShowError($"Opdatering fejlede: {res2.ErrorMessage}");
    //        }
    //        NavigationManager.NavigateTo("/Match");
    //    }
    //}
}

