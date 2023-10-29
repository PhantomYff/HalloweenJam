public interface IItem
{
    void Accept(IItemVisitor visitor, PlayerInventory inventory);
}
