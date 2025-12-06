using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Kaizen.Sample.Web.Services;

public class ComponentSourceService
{
    private readonly IWebHostEnvironment _env;

    public ComponentSourceService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> GetComponentSourceAsync(string componentPath)
    {
        // componentPath should be like "Samples/FlatGridSample.razor"
        var fullPath = Path.Combine(_env.ContentRootPath, "..", "Kaizen.Sample.Web.Client", componentPath);

        if (File.Exists(fullPath))
        {
            return await File.ReadAllTextAsync(fullPath);
        }

        return "// Source not found";
    }
}
