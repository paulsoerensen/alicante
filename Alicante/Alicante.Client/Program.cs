using Alicante.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.Toast;
using Radzen;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

//builder.Services.AddHttpClient(HttpClientNames.PublicClient, client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

//builder.Services.AddHttpClient<ApiClient>(client =>
//{
//    // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
//    // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
//    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
//});
builder.Services.AddScoped(sp => new HttpClient
{ 
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddScoped<ThemeService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<TooltipService>();

builder.Services.AddBlazoredToast();


await builder.Build().RunAsync();
