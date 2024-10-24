﻿using Alicante.Client.BaseComponents;
using Alicante.Client.Models;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Alicante.Client.Pages.Player;

public partial class PlayerList
{
    [Inject]
    public HttpClient http { get; set; }
    public List<PlayerModel> players { get; set; }
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
        var res = await http.GetFromJsonAsync<BaseResponseModel>("/api/player");
        if (res != null && res.Success)
        {
            players = JsonConvert.DeserializeObject<List<PlayerModel>>(res.Data.ToString());
        }
    }

    protected async Task HandleDelete()
    {
        var res = await http.DeleteAsync($"/api/player/{DeleteID}");
        if (res != null)
        {
            ToastService.ShowSuccess("Spilleren er smidt ud");
            await LoadData();
            Modal.Close();
        }
    }
}

