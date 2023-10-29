using UnityEngine;
using Zenject;

public abstract class ItemInteractable : MonoBehaviour, IInterractable, IItemVisitor
{
    protected ITextDisplay textDisplay;

    [Inject]
    private void Construct(ITextDisplay textDisplay)
    {
        this.textDisplay = textDisplay;
    }

    public void Interract(PlayerInventory inventory)
    {
        if (inventory.Item == null)
            InteractNoItem();
        else
            inventory.Item.Accept(this, inventory);
    }

    public virtual void VisitKey(Key key, PlayerInventory inventory)
    {
        textDisplay.Display("Ключ не подходит сюда.");
    }

    public virtual void InteractNoItem() { }
}
