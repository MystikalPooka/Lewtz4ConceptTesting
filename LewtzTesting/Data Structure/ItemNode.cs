using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LewtzTesting.Data_Structure
{
    public abstract class ItemNode : Component
    {
        public int Cost { get; set; }

        public override string ToString()
        {
            return base.ToString() + " | Cost: " + Cost;
        }
    }
}
