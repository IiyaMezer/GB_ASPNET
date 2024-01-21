using SEM3.Models.DTO;

namespace SEM3.Abstractions
{
    public interface IStorageServices
    {
         IEnumerable<StoreDTO> GetStores();
         int AddStores(StoreDTO store);
    }
}