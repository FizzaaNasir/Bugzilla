using Blazored.LocalStorage;
using Bugzilla.Client;
using Bugzilla.Client.Service;
using Bugzilla.Client.Service.IService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
string baseAddress = "https://localhost:7279/";
if (!builder.HostEnvironment.IsDevelopment())
{
   
    baseAddress = "https://bugzilla3-webapp.azurewebsites.net/";
}
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress)});


builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectUserService, ProjectUserService>();
builder.Services.AddScoped<IBugService, BugService>();

await builder.Build().RunAsync();
