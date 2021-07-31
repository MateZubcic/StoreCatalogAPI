using System;
using System.Collections.Generic;
using StoreCatalog.Dtos;
using StoreCatalog.Entities;

namespace StoreCatalog
{
    public interface IItemsRepository
    {
        IEnumerable<Item> GetItemsAsync();

        Item GetItemAsync(Guid id);

        Item CreateItemAsync(Item itemToCreate);

        void UpdateItemAsync(Item itemToUpdate);

        void DeleteItemAsync(Guid id);

    }
}