public class LyingKey : LyingItem
{
    protected override IItem GetItem() => new Key();
}
