using Parsec.Shaiya.Data;
using System.Threading.Tasks;

namespace Parsec.Core.Parsec
{
    /// <summary>
    /// Wrapper around Parsec library. As library is single-thread we need to work with it in worker thread.
    /// </summary>
    public interface IParsecService
    {
        /// <summary>
        /// Tries to parse sah file.
        /// </summary>
        /// <param name="path">path to sah file</param>
        /// <returns>true or false and data if parsed without errors</returns>
        Task<(bool ok, Data data)> TryOpenSah(string path);

        /// <summary>
        /// Tries to extract saf to path on operation system.
        /// </summary>
        /// <param name="data">sah & saf files</param>
        /// <returns>true if it could extract</returns>
        Task<bool> TryExtractSaf(string path, Data data);
    }
}
