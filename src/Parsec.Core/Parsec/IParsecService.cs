using Parsec.Shaiya.Data;
using System.Threading.Tasks;

namespace Parsec.Core.Parsec
{
    /// <summary>
    /// Wrapper around Parsec library. As library is single-thread we need to work with it in worker thread.
    /// </summary>
    public interface IParsecService
    {
        Task<(bool ok, Data data)> TryOpenSah(string path);
    }
}
