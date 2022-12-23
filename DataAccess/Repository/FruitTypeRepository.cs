using DataAccess.Repository.Contract;
using DataAccess.Utils;
using DataAcess.Context;
using Entities.DTO;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class FruitTypeRepository : IFruitTypeRepository, IDisposable
    {
        private readonly DbContextOptions<FruitContext> _dbContext;

        public FruitTypeRepository()
        {
            _dbContext = new DbContextOptions<FruitContext>();
        }

        public async Task Delete(long id)
        {
            using var db = new FruitContext(_dbContext);

            var existingType = await db.FruitTypes.FindAsync(id);

            if (existingType == null) throw new Exception("Fruit type not found.");

            db.FruitTypes.Remove(existingType);

            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<FruitTypeDTO>> FindAll()
        {
            using var db = new FruitContext(_dbContext);

            var fruitTypesQuery = await db.FruitTypes.ToListAsync();
            var fruitTypesList = new List<FruitTypeDTO>();

            if (fruitTypesQuery.Count > 0)
            {
                foreach (var fruitType in fruitTypesQuery)
                {
                    var fruitTypeDTO = new FruitTypeDTO
                    {
                        Id = fruitType.Id,
                        Name = fruitType.Name,
                        Description = fruitType.Description
                    };

                    fruitTypesList.Add(fruitTypeDTO);

                }
            }

            return fruitTypesList;
        }

        public async Task<FruitTypeDTO> FindById(long id)
        {
            using var db = new FruitContext(_dbContext);

            var existingFruitType = await db.FruitTypes.FindAsync(id);

            if (existingFruitType == null) throw new Exception("Fruit type not found.");

            var resFruitType = new FruitTypeDTO
            {
                Id = existingFruitType.Id,
                Name = existingFruitType.Name,
                Description = existingFruitType.Description
            };

            return resFruitType;
        }

        public async Task<FruitTypeDTO> Save(FruitTypeDTO fruitType)
        {
            using var db = new FruitContext(_dbContext);

            if (fruitType != null)
            {
                var fruitTypeObj = DTOtoModel.Convert<FruitType, FruitTypeDTO>(fruitType);

                var addFruitType = await db.FruitTypes.AddAsync(fruitTypeObj);

                await db.SaveChangesAsync();

                var resFruitType = new FruitTypeDTO
                {
                    Id = addFruitType.Entity.Id,
                    Name = addFruitType.Entity.Name,
                    Description = addFruitType.Entity.Description
                };

                return resFruitType;
            }

            throw new Exception("Fruit type can't be null.");
        }

        public async Task<FruitTypeDTO> Update(long id, FruitTypeDTO fruitType)
        {
            using var db = new FruitContext(_dbContext);

            var existingFruitType = await db.FruitTypes.FindAsync(id);

            if (existingFruitType == null) throw new Exception("Fruit type not found.");

            var fruitTypeModel = DTOtoModel.Convert<FruitType, FruitTypeDTO>(fruitType);

            existingFruitType.Name = fruitTypeModel.Name;
            existingFruitType.Description = fruitTypeModel.Description;

            db.FruitTypes.Update(existingFruitType);

            await db.SaveChangesAsync();

            var resFruitType = new FruitTypeDTO
            {
                Id = existingFruitType.Id,
                Name = existingFruitType.Name,
                Description = existingFruitType.Description
            };

            return resFruitType;
        }

        #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose
        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Instantiate a SafeHandle instance.
        readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
        #endregion
    }
}
