using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Kaizen.UI;
using Kaizen.Sample.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddSingleton<ToastService>();
builder.Services.AddSingleton<SampleCodeService>();

await builder.Build().RunAsync();
