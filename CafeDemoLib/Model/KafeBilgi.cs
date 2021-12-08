using System.Collections.Generic;

namespace CafeDemoLib.Model
{
    public class KafeBilgi : CafeBase
    {
        public byte[] Logo { get; set; }
        public string Ad { get; set; }
        public List<Kat> Katlar { get; set; } = new List<Kat>();
    }
}
