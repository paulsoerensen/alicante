using Alicante.Client.BaseComponents;
using Alicante.Client.Models;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Alicante.Client.Pages.Course;
public partial class CourseList
{
    [Inject]
    public HttpClient http { get; set; }
    public List<CourseModel> courses { get; set; }
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
        var res = await http.GetFromJsonAsync<BaseResponseModel>("/api/Course");
        if (res != null && res.Success)
        {
            courses = JsonConvert.DeserializeObject<List<CourseModel>>(res.Data.ToString());
        }
    }
    protected async Task HandleDelete()
    {
        var res = await http.DeleteAsync($"/api/course/{DeleteID}");
        if (res != null)
        {
            ToastService.ShowSuccess("Banen er droppet");
            await LoadData();
            Modal.Close();
        }
    }
}

