//using Parsec.Shaiya.Svmap;
using System.Diagnostics;
using System.Reflection;

namespace Parsec.Core.Version
{
    public class VersionService : IVersionService
    {
        public string GetCurrentVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public string GetParsecVersion()
        {
            return "Parsec .net standard 2.0";//FileVersionInfo.GetVersionInfo(Assembly.GetAssembly(typeof(Svmap)).Location).ProductVersion;
        }
    }
}
