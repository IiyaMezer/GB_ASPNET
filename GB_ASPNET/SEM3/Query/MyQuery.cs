using SEM3.Abstractions;
using SEM3.Models.DTO;

namespace SEM3.Query
{
    public class MyQuery
    {
        public IEnumerable<ProductDTO> GetProducts([Service] IProductServices services) => services.GetProducts();

        public IEnumerable<StoreDTO> GetStores([Service] IStorageServices services) => services.GetStores();
        public IEnumerable<GroupDTO> GetGroups([Service] IGroupServices services) => services.GetGroups();
    }
}
