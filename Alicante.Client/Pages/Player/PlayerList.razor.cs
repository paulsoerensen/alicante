using Alicante.Client.BaseComponents;
using Alicante.Client.Models;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Radzen.Blazor;
using System.Net.Http.Json;
using System.Reflection;

namespace Alicante.Client.Pages.Player;

public partial class PlayerList
{
    RadzenDataGrid<PlayerModel> playerGrid;

    [Inject]
    public HttpClient http { get; set; }
    public IEnumerable<PlayerModel> players { get; set; }

    public bool Editing { get; set; } = false;

    [Inject]
    private IToastService ToastService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var res = await http.GetFromJsonAsync<BaseResponseModel>("/api/player");
        if (res != null && res.Success)
        {
            players = JsonConvert.DeserializeObject<IEnumerable<PlayerModel>>(res.Data.ToString());
        }
    }

    #region Grid events

    async Task OnUpdateRow(PlayerModel player)
    {
        await SaveRow(player);
    }
    async void OnCreateRow(PlayerModel player)
    {
        //await UpdateRow(player);
        throw new NotImplementedException("OnCreateRow called");
    }

    async Task EditRow(PlayerModel player)
    {
        await playerGrid.EditRow(player);
        Editing = true;
    }
    async Task SaveRow(PlayerModel model)
    {
        var res = await http.PostAsJsonAsync<PlayerModel>($"/api/player/", model);
        if (res!.IsSuccessStatusCode)
        {
            ToastService.ShowSuccess("Matchen er opdateret");
            await LoadData();
        }
        else
        {
            playerGrid.CancelEditRow(model);
        }
        await playerGrid.Reload();
        Editing = false;
    }
    async Task DeleteRow(PlayerModel model)
    {
        var res = await http.DeleteFromJsonAsync<BaseResponseModel>($"/api/player/{model.PlayerId}");
        if (res!.Success)
        {
            ToastService.ShowSuccess("Matchen er slettet");
        }
        else
        {
            playerGrid.CancelEditRow(model);
        }
        await playerGrid.Reload();
    }
    async Task InsertRow()
    {
        var model = new PlayerModel();
        await playerGrid.InsertRow(model);
        Editing = true;
    }
    void CancelEdit(PlayerModel model)
    {
        playerGrid.CancelEditRow(model);
        Editing = false;
    }
    #endregion

    void Reset()
    {
        Editing = false;
    }

    protected async Task LoadData()
    {
        var res = await http.GetFromJsonAsync<BaseResponseModel>("/api/player");
        if (res != null && res.Success)
        {
            players = JsonConvert.DeserializeObject<List<PlayerModel>>(res.Data.ToString());
        }
    }

}

