using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdisyonApp.Model
{
    public abstract class KafeBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime EklenmeZamani { get; set; } = DateTime.Now;
    }

}
