﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Blob;
using RerouteBlobs.Configurations;
using RerouteBlobs.Interfaces;
using Serilog;

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

        public async Task Run()
        {
            _logger.LogInformation($"Azure Storage Connection String: {_azureConfig.StorageConnectionString}");

            await MoveBlobInSameStorageAccountAsync();
            //await SaveAllBlobNamesToFileAsync();
        }

        public async Task MoveBlobInSameStorageAccountAsync()
        {
            BlobContinuationToken token = null;
            do
            {
                var watchAllBlobsSelection = Stopwatch.StartNew();
                BlobResultSegment resultSegment = await _azureStorage.Container.ListBlobsSegmentedAsync(token);
                watchAllBlobsSelection.Stop();
                var elapsedMs = watchAllBlobsSelection.ElapsedMilliseconds;
                _logger.LogInformation($"Gettting all blobs elapsed time (ms): {elapsedMs}");
                token = resultSegment.ContinuationToken;

                var watchCopyingBlobs = Stopwatch.StartNew();
                foreach (IListBlobItem item in resultSegment.Results)
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blob = (CloudBlockBlob)item;

                        NewFileName FileName = GetBlobDetails(blob);

                        if (FileName.ApplicantId.Contains("NotePage"))
                        {
                            continue;
                        }

                        if (!ApplicantIds.Contains(FileName.ApplicantId))
                        {
                            ApplicantIds.Add(FileName.ApplicantId);
                            Console.WriteLine($"Creating directory: {FileName.ApplicantId}");
                            _logger.LogInformation($"Directory created for applicant: {FileName.ApplicantId}");
                        }
                        var previousDocumentLocation = _azureStorage.Container.GetBlockBlobReference($"{blob.Name}");
                        var newDocumentLocation = _azureStorage.Container.GetBlockBlobReference($"{FileName.ApplicantId}/{FileName.FileName}");
                        try
                        {
                            await newDocumentLocation.StartCopyAsync(previousDocumentLocation);
                        }
                        catch (Exception e)
                        {
                            _logger.LogError(e.Message);
                            throw;
                        }
                        Log.Logger.Information($"Moved {previousDocumentLocation.Name} to {newDocumentLocation.Name}");
                    }
                    else if (item.GetType() == typeof(CloudBlobDirectory))
                    {
                        CloudBlobDirectory directory = (CloudBlobDirectory) item;
                        Console.WriteLine($"Skipping existing directory: {directory.Uri}");

                        /* Delete blobs in directory
                         * await DeleteBlobsInDirectory(directory);
                         * continue;
                         */
                    }
                    else if (item.GetType() == typeof(CloudPageBlob))
                    {
                        CloudPageBlob pageBlob = (CloudPageBlob)item;
                        Console.WriteLine($"We are not using pageBlobs: {pageBlob}");
                    }
                }
                watchCopyingBlobs.Stop();
                /* Only run this if you want to create records to be processed.
                 * await CreateNBlobsOf500KbAsync(1200);
                 */
                _logger.LogInformation($"Copying blobs elapsed time(ms): {watchCopyingBlobs.ElapsedMilliseconds}");
            } while (token != null);
            _logger.LogInformation("Finished with the Script.");
        }

        private async Task DeleteBlobsInDirectory(CloudBlobDirectory directory)
        {
            var continuationToken = new BlobContinuationToken();

            var result = await directory.ListBlobsSegmentedAsync(continuationToken);

            foreach (IListBlobItem deleteItem in result.Results)
            {
                if (deleteItem.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob deleteBlob = (CloudBlockBlob)deleteItem;

                    await deleteBlob.DeleteIfExistsAsync();
                }
            }
        }

        public async Task SaveAllBlobNamesToFileAsync()
        {
            BlobContinuationToken token = null;
            do
            {
                BlobResultSegment resultSegment = await _azureStorage.Container.ListBlobsSegmentedAsync(token);
                token = resultSegment.ContinuationToken;
                
                foreach (IListBlobItem item in resultSegment.Results)
                {
                    try
                    {
                        if (item.GetType() == typeof(CloudBlockBlob))
                        {
                            CloudBlockBlob blob = (CloudBlockBlob)item;

                            string[] parts = blob.Name.Split('-');
                            
                            if (parts.Length != 2)
                            {
                                _logger.LogInformation($"{blob.Name}");
                            }
                            //NewFileName blobName = GetBlobDetails(blob);

                            if (parts[0].Contains("NotePage"))
                            {
                                continue;
                            }
                            if (!Regex.IsMatch(parts[0], "^[0-9]+$"))
                            {
                                _logger.LogInformation($"{blob.Name}");
                            }
                            else if (!Regex.IsMatch(parts[1], "^(?=(?:.{11}|.{12}|.{13}|.{14}|.{15}|)$)[a-z]+\\.[a-z]+$"))
                            {
                                _logger.LogInformation($"{blob.Name}");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message);
                        throw;
                    }
                }
                _logger.LogInformation("Finished with the script.");
            } while (token != null);
        }
        private NewFileName GetBlobDetails(CloudBlockBlob blob)
        {
            string[] parts = blob.Name.Split('-');
            string applicantId = parts[0];
            string[] afterHyphen = parts[1].Split('.');
            string proofType = afterHyphen[0];
            string extension = afterHyphen[1];
            string date = blob.Properties.LastModified.Value.LocalDateTime.ToString("yyyyMMdd");

            return new NewFileName(applicantId, proofType, date, extension);
        }

        public async Task CreateNBlobsOf500KbAsync(int numberOfBlobsToCreate)
        {
            for (var i = 0; i < numberOfBlobsToCreate; i++)
            {
                CloudBlockBlob blockBlob = _azureStorage.Container.GetBlockBlobReference($"{i}.jpg");
                await blockBlob.UploadFromFileAsync($"C:/Users/alan.costa/Desktop/sample.jpg");
                Log.Logger.Information(blockBlob.Name);}
        }
    }

}
