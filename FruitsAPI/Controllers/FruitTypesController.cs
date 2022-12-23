using BusinessLogic.Contract;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FruitAPI.Controllers
{
    [ApiController]
    [Route("/fruitTypes")]
    public class FruitTypesController : ControllerBase
    {
        private readonly IBLFruitType _bLFruitType;

        public FruitTypesController(IBLFruitType bLFruitType)
        {
            _bLFruitType = bLFruitType;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FruitTypeDTO>>> FindAllFruitTypes()
        {
            return Ok(await _bLFruitType.FindAll());
        }

        [HttpGet("/fruitTypes/{id}")]
        public async Task<ActionResult<object>> FindFruitTypeById(long id)
        {
            var fruitType = await _bLFruitType.FindById(id);

            if (fruitType == null)
            {
                return NotFound(new
                {
                    status = 404,
                    msg = "Fruit type not found",
                    date = DateTime.Now,
                });
            }

            return Ok(fruitType);
        }

        [HttpPost]
        public async Task<ActionResult<object>> SaveFruitType(FruitTypeDTO fruitType)
        {
            if (string.IsNullOrWhiteSpace(fruitType.Name))
            {
                return BadRequest(new
                {
                    status = 400,
                    msg = "Please informa a valid fruit type name.",
                    date = DateTime.Now
                }); ;
            }

            if (string.IsNullOrWhiteSpace(fruitType.Description))
            {
                return BadRequest(new
                {
                    status = 400,
                    msg = "Please informa a valid fruit type description.",
                    date = DateTime.Now
                }); ;
            }

            var newFruitType = await _bLFruitType.Save(fruitType);
            return Created($"fruitTypes/{newFruitType.Id}", newFruitType);
        }

        [HttpDelete("/fruitTypes/{id}")]
        public async Task<ActionResult> DeleteFruitType(long id)
        {
            var existingFruitType = await _bLFruitType.FindById(id);

            if (existingFruitType == null)
            {
                return NotFound(new
                {
                    status = 404,
                    msg = "Fruit type not found",
                    date = DateTime.Now,
                });
            }

            await _bLFruitType.Delete(id);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateFruitType(long id, FruitTypeDTO fruitType)
        {
            var existingFruitType = await _bLFruitType.FindById(id);

            if (existingFruitType == null)
            {
                return NotFound(new
                {
                    status = 404,
                    msg = "Fruit type not found",
                    date = DateTime.Now,
                });
            }

            if (string.IsNullOrWhiteSpace(fruitType.Name))
            {
                return BadRequest(new
                {
                    status = 400,
                    msg = "Please informa a valid fruit name.",
                    date = DateTime.Now
                }); ;
            }

            if (string.IsNullOrWhiteSpace(fruitType.Description))
            {
                return BadRequest(new
                {
                    status = 400,
                    msg = "Please informa a valid fruit description.",
                    date = DateTime.Now
                }); ;
            }

            return Ok(await _bLFruitType.Update(id, fruitType));
        }
    }
}
