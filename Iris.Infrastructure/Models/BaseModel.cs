using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Infrastructure.Models
{
    public class BaseModel
    {
        public string SenderId { get;  set; }

        public string RecipientId { get; set; }
    }
}
