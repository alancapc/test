using System.Threading.Tasks;

namespace RerouteBlobs.Interfaces
{
    public interface IBlobService
    {
        Task Run();

        void MoveBlobInSameStorageAccountAsync();
    }
}