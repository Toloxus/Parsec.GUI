using System.Diagnostics;
using System.Reflection;
using Parsec.Shaiya.Data;

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
            return FileVersionInfo.GetVersionInfo(Assembly.GetAssembly(typeof(Data)).Location).ProductVersion;
        }
    }
}
