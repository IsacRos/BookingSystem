using MongoDB.Bson;

namespace BookingSystem.Server.Entities
{
    public abstract class EntityBase
    {
        public ObjectId Id { get; set; }
    }
}
