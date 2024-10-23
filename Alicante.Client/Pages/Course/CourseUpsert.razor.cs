using Alicante.Client.Models;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Alicante.Client.Pages.Course;

public partial class CourseUpsert
{
    public CourseModel Model { get; set; } = new();

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
            var res = await http.GetFromJsonAsync<BaseResponseModel>($"/api/Course/{Id}");
            if (res != null && res.Success)
            {
                Model = JsonConvert.DeserializeObject<CourseModel>(res.Data.ToString());
            }
        }
    }

    protected void Cancel()
    {
        NavigationManager.NavigateTo("/Course");
    }

    public async Task Submit(CourseModel args)
    {
        var res = await http.PostAsJsonAsync("/api/Course", Model);
        if (res != null && res.IsSuccessStatusCode)
        {
            var res2 = JsonConvert.DeserializeObject<BaseResponseModel>(await res.Content.ReadAsStringAsync());
            if (res2 != null && res2.Success)
            {
                ToastService.ShowSuccess("Bane oprettet");
                NavigationManager.NavigateTo("/Course");
            }
            else
            {
                ToastService.ShowError(res2?.ErrorMessage);
            }
        }
    }
}

