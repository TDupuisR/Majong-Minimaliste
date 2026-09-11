using UnityEngine;

public abstract class ComboData : ScriptableObject
{
    [SerializeField] protected int n;
    public int GetN { get => n; }
    public abstract bool CheckCombo (TileData[] tiles);
}
