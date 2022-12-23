using BusinessLogic.Contract;
using DataAccess.Repository.Contract;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BLFruit : IBLFruit
    {
        private readonly IFruitRepository _fruitRepository;

        public BLFruit(IFruitRepository fruitRepository)
        {
            _fruitRepository = fruitRepository;
        }

        public async Task Delete(long id)
        {
            try
            {
                await _fruitRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FruitDTO> FindById(long id)
        {
            try
            {
                var fruit = await _fruitRepository.FindById(id);
                return fruit;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<FruitDTO>> FindAll()
        {
            try
            {
                return await _fruitRepository.FindAll(); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FruitDTO> Save(FruitDTO entity)
        {
            try
            {
                return await _fruitRepository.Save(entity); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FruitDTO> Update(long id, FruitDTO entity)
        {
            try
            {
                return await _fruitRepository.Update(id, entity); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
