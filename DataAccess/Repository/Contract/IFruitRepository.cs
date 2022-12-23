using Entities.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository.Contract
{
    public interface IFruitRepository
    {
        Task<IEnumerable<FruitDTO>> FindAll();
        Task<FruitDTO> FindById(long id);
        Task<FruitDTO> Save(FruitDTO fruit);
        Task<FruitDTO> Update(long id, FruitDTO fruit);
        Task Delete(long id);
    }
}
