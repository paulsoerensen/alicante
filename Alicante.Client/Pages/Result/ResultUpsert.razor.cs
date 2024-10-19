using Alicante.Client.Models;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Alicante.Client.Pages.Result;

public partial class ResultUpsert
{
    public ResultModel Model { get; set; } = new();

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
            var res = await http.GetFromJsonAsync<BaseResponseModel>($"/api/result/{Id}");
            if (res != null && res.Success)
            {
                Model = JsonConvert.DeserializeObject<ResultModel>(res.Data.ToString());
            }
        }
    }

    public async Task Submit()
    {
        var res = await http.PostAsJsonAsync("/api/result", Model);
        if (res != null && res.IsSuccessStatusCode)
        {
            var res2 = JsonConvert.DeserializeObject<BaseResponseModel>(await res.Content.ReadAsStringAsync());
            if (res2 != null && res2.Success)
            {
                ToastService.ShowSuccess("resultat oprettet");
                NavigationManager.NavigateTo("/result");
            }
        }
    }
}

