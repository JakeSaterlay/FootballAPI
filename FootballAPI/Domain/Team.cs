using MongoDB.Bson;

namespace FootballAPI.Domain
{
    public class Team
    {
        public ObjectId _id { get; set; }
        public int TeamId { get; set; }
        public string Name { get; set; }
        public Stadium Stadium { get; set; }
    }

    public class Stadium
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
    }
}
