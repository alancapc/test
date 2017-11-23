namespace RerouteBlobs.Interfaces
{
    public interface IBlobService
    {
        void Run();

        void MoveBlobInSameStorageAccountAsync();
    }
}