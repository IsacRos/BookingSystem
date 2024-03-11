using MongoDB.Bson;

namespace BookingSystem.Core.Entities
{
    public abstract class EntityBase
    {
        public ObjectId Id { get; set; }
    }
}
