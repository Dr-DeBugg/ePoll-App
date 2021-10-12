using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollBackend.Models
{
    public class Poll
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public List<Option> Options { get; set; }
    }

    public class PollDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
    }
    public class PollCollection
    {
        public PollCollection (List<PollDTO> polls)
        {
            this.Polls = polls;
        }
        public List<PollDTO> Polls { get; set; }
    }
}
