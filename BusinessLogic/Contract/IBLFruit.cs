using Entities.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Contract
{
    public interface IBLFruit
    {
        Task<FruitDTO> Save(FruitDTO entity);

        Task<FruitDTO> Update(long id, FruitDTO entity);

        Task Delete(long id);

        Task<FruitDTO> FindById(long id);

        Task<IEnumerable<FruitDTO>> FindAll();
    }
}
