using BusinessLogic.Contract;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FruitAPI.Controllers
{
    [ApiController]
    [Route("/fruits")]
    public class FruitsController : ControllerBase
    {
        private readonly IBLFruit _bLFruit;

        public FruitsController(IBLFruit bLFruit)
        {
            _bLFruit = bLFruit;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FruitDTO>>> FindAllFruits()
        {
            return Ok(await _bLFruit.FindAll());
        }

        [HttpGet("/fruits/{id}")]
        public async Task<ActionResult<object>> FindFruitById(long id)
        {
            var fruit = await _bLFruit.FindById(id);

            if (fruit == null)
            {
                return NotFound(new
                {
                    status = 404,
                    msg = "Fruit not found",
                    date = DateTime.Now,
                });
            }

            return Ok(fruit);
        }

        [HttpPost]
        public async Task<ActionResult<object>> SaveFruit(FruitDTO fruit)
        {
            if (string.IsNullOrWhiteSpace($"{fruit.Type}") || fruit.Type < 1)
            {
                return BadRequest(new
                {
                    status = 400,
                    msg = "Please informa a valid fruit type.",
                    date = DateTime.Now
                });
            }

            if (string.IsNullOrWhiteSpace(fruit.Name))
            {
                return BadRequest(new
                {
                    status = 400,
                    msg = "Please informa a valid fruit name.",
                    date = DateTime.Now
                }); ;
            }

            if (string.IsNullOrWhiteSpace(fruit.Description))
            {
                return BadRequest(new
                {
                    status = 400,
                    msg = "Please informa a valid fruit description.",
                    date = DateTime.Now
                }); ;
            }

            if (fruit.Description.Length < 25)
            {
                return BadRequest(new
                {
                    status = 400,
                    msg = "Description must have at least 25 characters length.",
                    date = DateTime.Now
                }); ;
            }

            var newFruit = await _bLFruit.Save(fruit);
            return Created($"fruits/{newFruit.Id}", newFruit);
        }

        [HttpDelete("/fruits/{id}")]
        public async Task<ActionResult> DeleteFruit(long id)
        {
            var existingFruit = await _bLFruit.FindById(id);

            if (existingFruit == null)
            {
                return NotFound(new
                {
                    status = 404,
                    msg = "Fruit not found",
                    date = DateTime.Now,
                });
            }

            await _bLFruit.Delete(id);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateFruit(long id, FruitDTO fruit)
        {
            var existingFruit = await _bLFruit.FindById(id);

            if (existingFruit == null)
            {
                return NotFound(new
                {
                    status = 404,
                    msg = "Fruit not found",
                    date = DateTime.Now,
                });
            }

            if (string.IsNullOrWhiteSpace($"{fruit.Type}") || fruit.Type < 1)
            {
                return BadRequest(new
                {
                    status = 400,
                    msg = "Please informa a valid fruit type.",
                    date = DateTime.Now
                });
            }

            if (string.IsNullOrWhiteSpace(fruit.Name))
            {
                return BadRequest(new
                {
                    status = 400,
                    msg = "Please informa a valid fruit name.",
                    date = DateTime.Now
                }); ;
            }

            if (string.IsNullOrWhiteSpace(fruit.Description))
            {
                return BadRequest(new
                {
                    status = 400,
                    msg = "Please informa a valid fruit description.",
                    date = DateTime.Now
                }); ;
            }

            if (fruit.Description.Length < 25)
            {
                return BadRequest(new
                {
                    status = 400,
                    msg = "Description must have at least 25 characters length.",
                    date = DateTime.Now
                }); ;
            }

            return Ok(await _bLFruit.Update(id, fruit));
        }
    }
}
