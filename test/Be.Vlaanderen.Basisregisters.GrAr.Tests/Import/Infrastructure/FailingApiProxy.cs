namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Import.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GrAr.Import.Processing;
    using GrAr.Import.Processing.Api;
    using Serilog;

    /// <summary>
    ///     Allows for testing what happens when the api throws an error
    /// </summary>
    public class FailingApiProxy : IApiProxy
    {
        private readonly int _failAfter;
        private readonly ILogger _logger;
        private int _counter;

        public FailingApiProxy(ILogger logger,
            int failAfter)
        {
            _counter = 0;
            _failAfter = failAfter;
            _logger = logger;
        }

        public void ImportBatch<TKey>(IEnumerable<KeyImport<TKey>> imports)
        {
            if (++_counter == _failAfter)
                throw new ApplicationException($"I was supposed to fail after {_failAfter} posts.");

            _logger.Information($"Fake sending {imports.Count()} imports ({_counter}/{_failAfter})");
        }
    }
}
