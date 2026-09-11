using NaughtyAttributes;
using UnityEngine;
using Managers;

public class TileBehaviour : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _symbolSpriteRndr;
    [SerializeField] private TileData getData;
    public TileData GetData { get => getData; }
    [Header("Position")]
    [SerializeField, ReadOnly] private TileGrid _parentGrid;
    [SerializeField, Tooltip("-1 is player | -2 is Null")] private int _gridPos; // -1 is player hand | -2 is Null
    public int GridPos { get => _gridPos; }
    public void SetInPlayersHand() { _gridPos = -1; }
    [SerializeField] private Vector3 _startScale = Vector3.one * 0.5f;

    [SerializeField] private bool debug_showCollider = true;

    [Button]
    private void SeeCollider() {
        Vector2 size = TilesManager.Instance.TileColliderWTolerance;
        Debug.DrawLine(new Vector2(transform.position.x + size.x, transform.position.y + size.y), new Vector2(transform.position.x - size.x, transform.position.y + size.y), Color.green, 0.1f, false);
        Debug.DrawLine(new Vector2(transform.position.x + size.x, transform.position.y + size.y), new Vector2(transform.position.x + size.x, transform.position.y - size.y), Color.green, 0.1f, false);
        Debug.DrawLine(new Vector2(transform.position.x - size.x, transform.position.y - size.y), new Vector2(transform.position.x + size.x, transform.position.y - size.y), Color.green, 0.1f, false);
        Debug.DrawLine(new Vector2(transform.position.x - size.x, transform.position.y - size.y), new Vector2(transform.position.x - size.x, transform.position.y + size.y), Color.green, 0.1f, false);
    }

    public TileBehaviour(TileData aTileGetData) {
        getData = aTileGetData;
    }

    [Button]
    public void ApplyData()
    {
        if (!getData) {
            Debug.LogError($"Missing TileData to apply", gameObject);
            return;
        }
        if (!_symbolSpriteRndr) {
            Debug.LogError($"Reference for SpriteRenderer Symbol missing", gameObject);
            return;
        }
        
        _symbolSpriteRndr.sprite = getData.TileSprite;
        if (getData.TileSprite == null) 
            Debug.LogWarning($"Symbol Sprite is null", gameObject);
    }
    public void ApplyData(TileData a_tileData, int index) {
        getData = a_tileData;
        _gridPos = index;
        
        ApplyData();
    }

    public void ResetData() {
        getData = null;
        _symbolSpriteRndr.sprite = null;
        _gridPos = -2;
        transform.localScale = _startScale;
    }

    private void Start() {
        ResetData();
    }

    private void Update() {
        if (debug_showCollider) SeeCollider();
    }
}
