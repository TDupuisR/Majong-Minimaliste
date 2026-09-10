using NaughtyAttributes;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    //[SerializeField] private SpriteRenderer _tileSprite;
    [SerializeField] private SpriteRenderer _symbolSpriteRndr;
    [SerializeField] private TileData _data;
    [Space(7)]
    [SerializeField, ReadOnly] private TileGrid _parentGrid;
    [SerializeField, Tooltip("-1 is player hand")] private int _gridPos; // -1 is player hand

    public TileBehaviour(TileData a_tileData) {
        _data = a_tileData;
    }

    [Button]
    public void ApplyData()
    {
        if (!_data) {
            Debug.LogError($"Missing TileData to apply", gameObject);
            return;
        }
        if (!_symbolSpriteRndr) {
            Debug.LogError($"Reference for SpriteRenderer Symbol missing", gameObject);
            return;
        }
        
        _symbolSpriteRndr.sprite = _data.TileSprite;
        if (_data.TileSprite == null) 
            Debug.LogWarning($"Symbol Sprite is null", gameObject);
    }
    public void ApplyData(TileData a_tileData)
    {
        _data = a_tileData;
        ApplyData();
    }

    public void Start() {
        if (!_data) {
            Debug.LogWarning($"Missing TileData at Start", gameObject);
            return;
        }
        
        ApplyData();
    }
}
