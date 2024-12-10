using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CustomerRecords.Application.Models
{
    public class Customer
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public string FirstName { get; set; }

        public string LastName{ get; set; }

        [Phone]
        public string PhoneNumber{ get; set; }

        public string Address{ get; set; }
    }
}
