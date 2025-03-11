using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Langchips.Models.Models
{
    public class Term
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int SetId { get; set; }
        public Set Set { get; set; }
       // public ICollection<Translation> Translations { get; set; } = new List<Translation>();
    }
}
