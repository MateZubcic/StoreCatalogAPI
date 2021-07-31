using System;
using System.Collections.Generic;
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
        public ActionResult <IEnumerable<ItemDto>> GetItems()
        {
            var repoItems = _repository.GetItemsAsync();
            
            return Ok(_mapper.Map<IEnumerable<ItemDto>>(repoItems));
        }

        [HttpGet("id")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var repoItem = _repository.GetItemAsync(id);

            if(repoItem == null)
                return NotFound();

            var item = _mapper.Map<ItemDto>(repoItem);

            return item;
        }

        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto item)
        {
            var itemToCreate = _mapper.Map<Item>(item);
            
            var createdItem = _repository.CreateItemAsync(itemToCreate);
            
            return CreatedAtAction(nameof(GetItem), new {id = createdItem.Id}, _mapper.Map<ItemDto>(createdItem)); 
        }

        [HttpPut("id")]
        public ActionResult UpdateItem(UpdateItemDto updateItem, Guid id)
        {
            var itemToUpdate = _mapper.Map<Item>(updateItem);
            itemToUpdate.Id = id;
            _repository.UpdateItemAsync(itemToUpdate);
            return Ok();
        }

        [HttpDelete("id")]
        public ActionResult DeleteItem(Guid id)
        {
            if(_repository.GetItemAsync(id) == null)
                return NotFound();

            _repository.DeleteItemAsync(id);

            return Ok();
        }
    }
}