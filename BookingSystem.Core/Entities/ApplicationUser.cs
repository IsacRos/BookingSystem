﻿using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace BookingSystem.Core.Entities;

[CollectionName("Users")]
public class ApplicationUser : MongoIdentityUser<Guid>
{
}
