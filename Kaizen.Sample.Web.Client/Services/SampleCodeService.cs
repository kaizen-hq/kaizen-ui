using System.Reflection;

namespace Kaizen.Sample.Web.Client.Services;

public class SampleCodeService
{
    public string GetSampleCode(string sampleName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = $"Kaizen.Sample.Web.Client.Samples.{sampleName}.razor";

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            return $"// Could not find sample: {resourceName}";
        }

        using var reader = new StreamReader(stream);
        var content = reader.ReadToEnd();

        // Remove @using statements
        var lines = content.Split('\n')
            .Where(line => !line.TrimStart().StartsWith("@using"))
            .ToArray();

        return string.Join('\n', lines).Trim();
    }
}
