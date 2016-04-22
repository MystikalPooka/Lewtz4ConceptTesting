using LewtzTesting.Visitors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace LewtzTesting.Data_Structure
{
    public class Item : ItemNode
    {
        public Item()
        {
            Name = "Unnamed Item";
            Probability = 0;
            Book = "";
        }

        public Item(string name, int prob, int cost = 0, string book = "")
        {
            Name = name;
            Probability = prob;
            Cost = cost;
            Book = book;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}