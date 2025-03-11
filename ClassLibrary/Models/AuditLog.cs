using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchips.Models.Models
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Action { get; set; } // What action was performed (e.g., "User Created", "Password Changed")
        public DateTime ActionDate { get; set; }
        public string Details { get; set; }
    }
}
