// Models/SwapSpace.cs
using System.Collections.Generic;

namespace MMU.Simulator.Api.Models
{
    public class SwapSpace
    {
        public List<Page> Pages { get; set; }

        public SwapSpace()
        {
            Pages = new List<Page>();
        }
    }
}
