using System.IO;
using System.Reflection;

namespace Core
{
    public class ResourcesHandler
    {
        public static Stream GetStream(string fileRelativePath)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(fileRelativePath);
        }
    }
}