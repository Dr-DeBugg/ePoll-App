using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json;
using PollBackend.Models;
using PollBackend.Repository;

namespace PollBackend.Controllers
{
    [Route("polls")]
    [ApiController]
    public class PollsController : ControllerBase
    {
        private readonly IMongoDBPollContext _context;
        public IMongoCollection<Poll> _dbCollection;

        public PollsController(IMongoDBPollContext context)
        {
            _context = context;
            _dbCollection = _context.GetCollection<Poll>(typeof(Poll).Name);
        }

        // GET: all polls
        [EnableCors]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Poll>>> GetPolls()
        {
            var allPolls = await _dbCollection.FindAsync(Builders<Poll>.Filter.Empty);
            string allPolls_str = System.Text.Json.JsonSerializer.Serialize(allPolls.ToList());
            PollCollection pollCollection = new PollCollection(new List<PollDTO>(
                JsonConvert.DeserializeObject<List<PollDTO>>(allPolls_str)));
            return Ok(pollCollection);
        }


        // GET: polls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Poll>> GetPoll(long id)
        {
            var poll = await _dbCollection.FindAsync(Builders<Poll>.Filter.Where(p => p.Id == id));
            if (poll == null)
            {
                return NotFound();
            }
            return Ok(poll.FirstOrDefault());
        }

        // POST: polls
        [HttpPost]
        public async Task<ActionResult<Poll>> PostPoll(JsonElement body)
        {
            //format JSON input
            List<Option> options = new List<Option>();
            string[] optionsArr = JsonConvert.DeserializeObject<string[]>(body.GetProperty("options").ToString());
            for (int i = 0; i < optionsArr.Length; i++)
            {
                options.Add(new Option(optionsArr[i], i+1));
            }

            var allPolls = await _dbCollection.FindAsync(Builders<Poll>.Filter.Empty);
            //create new poll 
            Poll poll = new Poll();
            poll.Id = allPolls.ToList().Count() + 1;
            poll.Options = options;
            poll.Title = body.GetProperty("title").ToString();

            await _dbCollection.InsertOneAsync(poll);
            return poll;
        }

        // POST: polls/{id}/vote/{option}
        [HttpPost("{pollID}/vote/{optionId}")]
        public async Task<ActionResult<Poll>> PostVote(long pollId, long optionId)
        {
            //var poll = _dbCollection.Find(x => x.Id == id && x.Options.Id == option).ToList();
            var found_polls = _dbCollection.Find(p => p.Id == pollId).ToList();
            var poll = found_polls.FirstOrDefault();
            if (poll == null)
            {
                return NotFound();
            }

            //find option to vote for
            var optionToVote = poll.Options.Find(o => o.Id == optionId);
            if (optionToVote == null)
            {
                return NotFound();
            }
            optionToVote.Votes++;            

            await _dbCollection.ReplaceOneAsync(Builders<Poll>.Filter.Where(p => p.Id == pollId), poll);
            return poll;
        }
        
        // DELETE: polls/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Poll>> Delete(long id)
        {
            var poll = await _dbCollection.FindOneAndDeleteAsync(Builders<Poll>.Filter.Where(s => s.Id == id));
            return Ok("deleted");
        }
    }
}
