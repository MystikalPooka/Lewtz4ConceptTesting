namespace LewtzTesting.Visitors
{
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}
