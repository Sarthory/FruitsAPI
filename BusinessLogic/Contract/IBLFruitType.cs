using Entities.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Contract
{
    public interface IBLFruitType
    {
        Task<FruitTypeDTO> Save(FruitTypeDTO entity);

        Task<FruitTypeDTO> Update(long id, FruitTypeDTO entity);

        Task Delete(long id);

        Task<FruitTypeDTO> FindById(long id);

        Task<IEnumerable<FruitTypeDTO>> GetAll();
    }
}
