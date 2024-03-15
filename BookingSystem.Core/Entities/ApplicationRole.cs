using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace BookingSystem.Core.Entities;

[CollectionName("Roles")]
public class ApplicationRole : MongoIdentityRole<Guid>
{
}
