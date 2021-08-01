using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreCatalog.Dtos;
using StoreCatalog.Entities;

namespace StoreCatalog
{
    public interface IItemsRepository
    {
        Task<IEnumerable<Item>> GetItemsAsync();

        Task<Item> GetItemAsync(Guid id);

        Task<Item> CreateItemAsync(Item itemToCreate);

        Task UpdateItemAsync(Item itemToUpdate);

        Task DeleteItemAsync(Guid id);
    }
}