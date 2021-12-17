using Parsec.Core.Logging;
using Parsec.Shaiya.Data;
using System;
using System.Threading.Tasks;

namespace Parsec.Core.Parsec
{
    public class ParsecService : IParsecService
    {
        private readonly ILoggingService _loggingService;

        public ParsecService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public async Task<(bool ok, Data data)> TryOpenSah(string path)
        {
            Data data = null;
            bool ok = false;

            await Task.Run(() =>
            {
                try
                {
                    data = new Data(path);
                    ok = true;

                    _loggingService.Info(this, $"Successfully parsed {path}. Found {data.FileCount} files.");
                }
                catch (Exception ex)
                {
                    _loggingService.Error(this, $"Failed to parse .sah", ex);
                    ok = false;
                }
            });

            return (ok, data);
        }
    }
}
