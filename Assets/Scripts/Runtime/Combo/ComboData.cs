using UnityEngine;

public abstract class ComboData : ScriptableObject
{
    [SerializeField] protected int n;
    public abstract bool CheckCombo (TileData[] tiles);
}
