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
    public List<ResultViewModel>? results { get; set; }
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
        var res = await http.GetFromJsonAsync<BaseResponseModel>("/api/result");
        if (res != null && res.Success)
        {
            results = JsonConvert.DeserializeObject<List<ResultViewModel>>(res.Data.ToString());
        }
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

