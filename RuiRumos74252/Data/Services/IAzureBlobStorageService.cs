namespace RuiRumos74252.Data.Services
{
    public interface IAzureBlobStorageService
    {
        void UploadBlob(Stream stream, string blobName);
    }
}
