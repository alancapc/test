using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Blob;
using RerouteBlobs.Configurations;
using RerouteBlobs.Interfaces;

namespace RerouteBlobs.Implementations
{
    public class BlobService : IBlobService
    {
        private readonly ILogger<BlobService> _logger;
        private readonly AzureConfig _azureConfig;
        private readonly IAzureStorage _azureStorage;
        public List<string> ApplicantIds = new List<string>();
        public BlobService(ILogger<BlobService> logger, IOptions<AzureConfig> azureConfig, IAzureStorage azureStorage)
        {
            _logger = logger;
            _azureConfig = azureConfig.Value;
            _azureStorage = azureStorage;
        }
        public void Run()
        {
            _logger.LogInformation($"Azure Storage Connection String: {_azureConfig.StorageConnectionString}");
        }
        public async void MoveBlobInSameStorageAccountAsync()
        {
            BlobContinuationToken token = null;
            do
            {
                BlobResultSegment resultSegment = await _azureStorage.Container.ListBlobsSegmentedAsync(token);
                token = resultSegment.ContinuationToken;

                foreach (IListBlobItem item in resultSegment.Results)
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blob = (CloudBlockBlob)item;
                        
                        var applicantId = blob.Name.Substring(0, 7);
                        if (!ApplicantIds.Contains(applicantId))
                        {
                            ApplicantIds.Add(applicantId);
                            Console.WriteLine($"Creating directory: {applicantId}");
                        }
                        var previousDocumentLocation = _azureStorage.Container.GetBlockBlobReference($"{blob.Name}");
                        var newDocumentLocation = _azureStorage.Container.GetBlockBlobReference($"{applicantId}/{blob.Name}");
                        await newDocumentLocation.StartCopyAsync(previousDocumentLocation);
                        Console.WriteLine($"Moved {previousDocumentLocation.Name} to {newDocumentLocation.Name}");
                    }
                    else if (item.GetType() == typeof(CloudBlobDirectory))
                    {
                        CloudBlobDirectory directory = (CloudBlobDirectory)item;

                        Console.WriteLine("Directory: {0}", directory.Uri);
                    }
                    else if (item.GetType() == typeof(CloudPageBlob))
                    {
                        CloudPageBlob pageBlob = (CloudPageBlob)item;

                        Console.WriteLine($"We are not using pageBlobs: {pageBlob}");
                    }
                }
            } while (token != null);
        }
    }
}
