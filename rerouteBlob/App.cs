﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RerouteBlobs.Configurations;
using RerouteBlobs.Interfaces;

namespace RerouteBlobs
{
    public class App
    {
        private readonly IBlobService _blobService;
        private readonly ILogger<App> _logger;
        private readonly AppSettings _config;
        private readonly AzureConfig _azureConfig;

        public App(
            IBlobService blobService,
            ILogger<App> logger,
            IOptions<AppSettings> config,
            IOptions<AzureConfig> azureConfig)
        {
            _blobService = blobService;
            _logger = logger;
            _config = config.Value;
            _azureConfig = azureConfig.Value;
        }

        public void Run()
        {
            // add provisional services here if you wish
            _logger.LogInformation( "Starting Blob Service..." );

            _blobService.Run();

            System.Console.ReadKey();
        }
    }
}
