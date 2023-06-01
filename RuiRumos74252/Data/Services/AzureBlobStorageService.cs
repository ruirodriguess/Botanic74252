using Microsoft.WindowsAzure.Storage;

namespace RuiRumos74252.Data.Services
{
    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        private const string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=saruirumos74252;AccountKey=OKDSeyyWJYacYUDlrDq7jR79KKxukdY5HqpaxWPPT9hxRSMltzyT2/Mz+UYCAcmxGnXR2D4UpTEU+AStWbfQNQ==;EndpointSuffix=core.windows.net";
        private const string ContainerName = "blog74252";

        public void UploadBlob(Stream stream, string blobName)
        {
            var storageAccount = CloudStorageAccount.Parse(StorageConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(ContainerName);
            container.CreateIfNotExistsAsync();

            var blockBlob = container.GetBlockBlobReference(blobName);
            blockBlob.UploadFromStreamAsync(stream);
        }
    }
}
