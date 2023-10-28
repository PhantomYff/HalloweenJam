public class Key : IItem
{
    public void Accept(IItemVisitor visitor, PlayerInventory inventory) => visitor.VisitKey(this, inventory);
}
