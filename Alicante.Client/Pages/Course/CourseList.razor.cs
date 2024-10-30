using Alicante.Client.BaseComponents;
using Alicante.Client.Models;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Radzen.Blazor;
using System.Net.Http.Json;

namespace Alicante.Client.Pages.Course;
public partial class CourseList
{
    RadzenDataGrid<CourseModel> coursesGrid;

    [Inject]
    public HttpClient http { get; set; }
    public List<CourseModel> courses { get; set; }
    public AppModal Modal { get; set; }
    public int DeleteID { get; set; }

    List<CourseModel> coursesToInsert = new List<CourseModel>();
    List<CourseModel> coursesToUpdate = new List<CourseModel>();


    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private IToastService ToastService { get; set; }

    void Reset()
    {
        coursesToInsert.Clear();
        coursesToUpdate.Clear();
    }

    void Reset(CourseModel course)
    {
        coursesToInsert.Remove(course);
        coursesToUpdate.Remove(course);
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await LoadData();
    }

    async Task EditRow(CourseModel course)
    {
        if (coursesToInsert.Count() > 0)
        {
            Reset();
        }

        coursesToUpdate.Add(course);
        await coursesGrid.EditRow(course);
    }

    void OnUpdateRow(CourseModel course)
    {
        Reset(course);

        //dbContext.Update(course);

        //dbContext.SaveChanges();
    }

    async Task SaveRow(CourseModel course)
    {
        await coursesGrid.UpdateRow(course);
    }

    void CancelEdit(CourseModel course)
    {
        Reset(course);

        coursesGrid.CancelEditRow(course);

        //var courseEntry = dbContext.Entry(course);
        //if (courseEntry.State == EntityState.Modified)
        //{
        //    courseEntry.CurrentValues.SetValues(courseEntry.OriginalValues);
        //    courseEntry.State = EntityState.Unchanged;
        //}
    }

    async Task DeleteRow(CourseModel course)
    {
        Reset(course);

        if (courses.Contains(course))
        {
            //dbContext.Remove<CourseModel>(course);

            //dbContext.SaveChanges();

            await coursesGrid.Reload();
        }
        else
        {
            coursesGrid.CancelEditRow(course);
            await coursesGrid.Reload();
        }
    }

    async Task InsertRow()
    {
        Reset();

        var course = new CourseModel();
        coursesToInsert.Add(course);
        await coursesGrid.InsertRow(course);
    }

    void OnCreateRow(CourseModel course)
    {
        //dbContext.Add(course);

        //dbContext.SaveChanges();

        coursesToInsert.Remove(course);
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

