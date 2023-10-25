using UnityEngine;

public class RenameAttribute : PropertyAttribute
{
    public RenameAttribute(string name)
	{
        newName = name;
    }

    public readonly string newName;
}
