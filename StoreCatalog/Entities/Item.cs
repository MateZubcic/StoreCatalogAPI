using System;

namespace StoreCatalog.Entities
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}