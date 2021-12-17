namespace Parsec.Core.Version
{
    public interface IVersionService
    {
        /// <summary>
        /// Gets current GUI version.
        /// </summary>
        /// <returns></returns>
        string GetCurrentVersion();

        /// <summary>
        /// Gets Parsec Nuget package version.
        /// </summary>
        string GetParsecVersion();
    }
}
