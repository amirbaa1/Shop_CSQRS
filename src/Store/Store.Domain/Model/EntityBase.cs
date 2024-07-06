using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Model
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTimeStatus { get; set; }
        public DateTime UpdateTimeProduct { get; set; }
    }
}