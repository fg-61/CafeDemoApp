using System;

namespace CafeDemoLib.Model
{
    public abstract class CafeBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime EklenmeZamani { get; set; } = DateTime.Now; 
    }
}