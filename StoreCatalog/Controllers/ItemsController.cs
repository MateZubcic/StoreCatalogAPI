using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoreCatalog.Dtos;
using StoreCatalog.Entities;
using StoreCatalog.Repositories;

namespace StoreCatalog.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository _repository;
        private readonly IMapper _mapper;

        public ItemsController(IItemsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var repoItems = await _repository.GetItemsAsync();
            
            return _mapper.Map<IEnumerable<ItemDto>>(repoItems);
        }

        [HttpGet("id")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var repoItem = await _repository.GetItemAsync(id);

            if(repoItem == null)
                return NotFound();

            var item = _mapper.Map<ItemDto>(repoItem);

            return item;
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto item)
        {
            var itemToCreate = _mapper.Map<Item>(item);
            
            var createdItem = await _repository.CreateItemAsync(itemToCreate);
            
            return CreatedAtAction(nameof(GetItemAsync), new {id = createdItem.Id}, _mapper.Map<ItemDto>(createdItem)); 
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdateItemAsync(UpdateItemDto updateItem, Guid id)
        {
            var itemToUpdate = _mapper.Map<Item>(updateItem);
            itemToUpdate.Id = id;
            await _repository.UpdateItemAsync(itemToUpdate);
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var existingItem = await _repository.GetItemAsync(id);

            if(existingItem  is null)
                return NotFound();

            await _repository.DeleteItemAsync(id);

            return Ok();
        }
    }
}