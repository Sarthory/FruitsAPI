using Entities.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository.Contract
{
    public interface IFruitTypeRepository
    {
        Task<IEnumerable<FruitTypeDTO>> FindAll();
        Task<FruitTypeDTO> FindById(long id);
        Task<FruitTypeDTO> Save(FruitTypeDTO fruitType);
        Task<FruitTypeDTO> Update(long id, FruitTypeDTO fruitType);
        Task Delete(long id);
    }
}
