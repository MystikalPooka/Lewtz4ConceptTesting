using LewtzTesting.Visitors;
using Newtonsoft.Json;

namespace LewtzTesting.Data_Structure
{
    public abstract class Component : IVisitable
    {
        [JsonProperty("name")]
        public string Name { get; protected set; }

        [JsonProperty("probability")]
        public int Probability { get; protected set; }

        [JsonProperty("book")]
        public string Book { get; protected set; }

        public abstract void Accept(IVisitor visitor);

        public override string ToString()
        {
            return this.GetType().Name +": " + Name + " | Probability: " + Probability + " | Book: " + Book;
        }
    }
}
