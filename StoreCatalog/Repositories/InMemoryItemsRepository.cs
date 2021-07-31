using System;
using System.Collections.Generic;
using System.Linq;
using StoreCatalog.Entities;

namespace StoreCatalog.Repositories
{
    public class InMemoryItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Health potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Mana potion", Price = 15, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 25, CreatedDate = DateTimeOffset.UtcNow }
        };

        public IEnumerable<Item> GetItemsAsync()
        {
            return items;
        }

        public Item GetItemAsync(Guid id)
        {
            return items.FirstOrDefault(x => x.Id == id);
        }

        public Item CreateItemAsync(Item itemToCreate)
        {
            itemToCreate.CreatedDate = DateTimeOffset.UtcNow;
            itemToCreate.Id = Guid.NewGuid();
           
            items.Add(itemToCreate);

            return itemToCreate;
        }

        public void UpdateItemAsync(Item itemToUpdate)
        {
            var index = items.FindIndex( x => x.Id == itemToUpdate.Id);
            items[index] = itemToUpdate;
        }

        public void DeleteItemAsync(Guid id)
        {
            var itemToDelete = items.FirstOrDefault(x => x.Id == id);
        
            items.Remove(itemToDelete);
        }
    }
}