using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollBackend.Models
{
    public class Option
    {
        public Option(string title)
        {
            Title = title;
        }
        public Option(string title, long id)
        {
            Title = title;
            Id = id;
        }
        public long Id { get; set; }
        public string Title { get; set; }
        public int Votes { get; set; }
    }
}
