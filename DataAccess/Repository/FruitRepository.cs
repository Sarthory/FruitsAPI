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
    public class FruitRepository : IFruitRepository, IDisposable
    {
        private readonly DbContextOptions<FruitContext> _dbContext;

        public FruitRepository()
        {
            _dbContext = new DbContextOptions<FruitContext>();
        }

        public async Task Delete(long id)
        {
            using var db = new FruitContext(_dbContext);

            var existingFruit = await db.Fruits.FindAsync(id);

            if (existingFruit == null) throw new Exception("Fruit not found.");

            db.Fruits.Remove(existingFruit);

            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<FruitDTO>> FindAll()
        {
            using var db = new FruitContext(_dbContext);

            var fruitsQuery = await db.Fruits.ToListAsync();
            var fruitsList = new List<FruitDTO>();

            if (fruitsQuery.Count > 0)
            {
                foreach (var fruit in fruitsQuery)
                {
                    var fruitDTO = new FruitDTO
                    {
                        Id = fruit.Id,
                        Name = fruit.Name,
                        Description = fruit.Description,
                        Type = fruit.Type
                    };

                    fruitsList.Add(fruitDTO);

                }
            }

            return fruitsList;
        }

        public async Task<FruitDTO> FindById(long id)
        {
            using var db = new FruitContext(_dbContext);

            var existingFruit = await db.Fruits.FindAsync(id);

            if (existingFruit == null) throw new Exception("Fruit not found.");

            var resFruit = new FruitDTO
            {
                Id = existingFruit.Id,
                Name = existingFruit.Name,
                Description = existingFruit.Description,
                Type = existingFruit.Type
            };

            return resFruit;
        }

        public async Task<FruitDTO> Save(FruitDTO fruit)
        {
            using var db = new FruitContext(_dbContext);

            if (fruit != null)
            {
                var fruitObj = DTOtoModel.Convert<Fruit, FruitDTO>(fruit);

                var addFruit = await db.Fruits.AddAsync(fruitObj);

                await db.SaveChangesAsync();

                var resFruit = new FruitDTO
                {
                    Id = addFruit.Entity.Id,
                    Name = addFruit.Entity.Name,
                    Description = addFruit.Entity.Description,
                    Type = addFruit.Entity.Type
                };

                return resFruit;
            }

            throw new Exception("Fruit can't be null.");
        }

        public async Task<FruitDTO> Update(long id, FruitDTO fruit)
        {
            using var db = new FruitContext(_dbContext);

            var existingFruit = await db.Fruits.FindAsync(id);

            if (existingFruit == null) throw new Exception("Fruit not found.");

            var fruitModel = DTOtoModel.Convert<Fruit, FruitDTO>(fruit);

            existingFruit.Name = fruitModel.Name;
            existingFruit.Description = fruitModel.Description;
            existingFruit.Type = fruitModel.Type;

            db.Fruits.Update(existingFruit);

            await db.SaveChangesAsync();

            var resFruit = new FruitDTO
            {
                Id = existingFruit.Id,
                Name = existingFruit.Name,
                Description = existingFruit.Description,
                Type = existingFruit.Type
            };

            return resFruit;
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
