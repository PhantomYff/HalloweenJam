using UnityEngine;

[RequireComponent(typeof(Animator))]
public class KeyOpenableGate : ItemInteractable
{
    [SerializeField] private Animator _animator;

    private void Reset()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    public override void VisitKey(Key key, PlayerInventory inventory)
    {
        _animator.enabled = true;
        inventory.Item = null;
    }

    public override void InteractNoItem()
    {
        textDisplay.Display("Мне нужен ключ");
    }
}
