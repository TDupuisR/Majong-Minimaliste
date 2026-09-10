using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TileData", menuName = "Scriptable Objects/TileData")]
[Serializable]
public class TileData : ScriptableObject
{
    [SerializeField] private families family;
    public families Family { get => family;}
    [SerializeField] private int value;
    public int Value { get => value;}
}
