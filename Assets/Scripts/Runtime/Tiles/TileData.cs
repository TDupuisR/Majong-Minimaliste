using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TileData", menuName = "Scriptable Objects/TileData")]
[Serializable]
public class TileData : ScriptableObject
{
    [SerializeField] private TileFamilies tileFamily;
    public TileFamilies TileFamily { get => tileFamily;}
    [SerializeField] private int value;
    public int Value { get => value;}

    [SerializeField] private Sprite _tileSprite;
    public Sprite TileSprite { get => _tileSprite; }
}
