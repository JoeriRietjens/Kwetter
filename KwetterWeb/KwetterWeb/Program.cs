using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using KwetterWeb;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(impl => new HttpClient { BaseAddress = new Uri("http://localhost:5296") });
//Backend runs on port https://localhost:7184;http://localhost:5296 
await builder.Build().RunAsync();