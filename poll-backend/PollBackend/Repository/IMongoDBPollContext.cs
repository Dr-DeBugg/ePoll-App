using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollBackend.Repository
{
    public interface IMongoDBPollContext
    {
        IMongoCollection<Poll> GetCollection<Poll>(string name);
    }
}
