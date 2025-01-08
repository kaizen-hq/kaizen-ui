using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Mizu.UI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddSingleton<ToastService>();

await builder.Build().RunAsync();
