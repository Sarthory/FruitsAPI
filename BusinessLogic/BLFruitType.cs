using BusinessLogic.Contract;
using DataAccess.Repository.Contract;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BLFruitType : IBLFruitType
    {
        private readonly IFruitTypeRepository _fruitTypeRepository;

        public BLFruitType(IFruitTypeRepository fruitTypeRepository)
        {
            _fruitTypeRepository = fruitTypeRepository;
        }

        public async Task Delete(long id)
        {
            try
            {
                await _fruitTypeRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FruitTypeDTO> FindById(long id)
        {
            try
            {
                var fruitType = await _fruitTypeRepository.FindById(id);
                return fruitType;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<FruitTypeDTO>> FindAll()
        {
            try
            {
                return await _fruitTypeRepository.FindAll(); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FruitTypeDTO> Save(FruitTypeDTO entity)
        {
            try
            {
                return await _fruitTypeRepository.Save(entity); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FruitTypeDTO> Update(long id, FruitTypeDTO entity)
        {
            try
            {
                return await _fruitTypeRepository.Update(id, entity); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
