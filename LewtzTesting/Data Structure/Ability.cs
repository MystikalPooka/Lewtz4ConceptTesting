using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LewtzTesting.Visitors;

namespace LewtzTesting.Data_Structure
{
    public class Ability : Component
    {
        public int Cost { get; set; }
        public int Bonus { get; set; }

        public override void Accept(IVisitor visitor)
        {
        }
    }
}
