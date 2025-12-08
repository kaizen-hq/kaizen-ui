using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Kaizen.UI;
using Kaizen.Sample.Web.Client;
using Kaizen.Sample.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<Routes>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<ToastService>();
builder.Services.AddSingleton<SampleCodeService>();

await builder.Build().RunAsync();
