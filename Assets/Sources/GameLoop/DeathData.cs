using UnityEngine;

[CreateAssetMenu(menuName = "Data/Death")]
public class DeathData : ScriptableObject
{
    [field: SerializeField] public float SecondsUntilRestart { get; private set; }
}
