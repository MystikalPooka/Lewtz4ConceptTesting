using LewtzTesting.Data_Structure;

namespace LewtzTesting.Visitors
{
    public interface IVisitor
    {
        void Visit(Table table);
        void Visit(Item item);
        void Visit(MagicItem item);
        void Visit(Ability ability);
    }
}