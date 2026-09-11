using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;
using Managers;

public class TileGrid : MonoBehaviour
{
    [SerializeField, ReadOnly] private int n;
    public int SetNbCol
    {
        set {
            n = Mathf.Abs(value);
            UpdateDebugDisplay();
        }
    }
    [SerializeField] private float e;
    [Space(7)]
    [SerializeField, ReadOnly] private List<TileBehaviour> _childTiles;

    private void FixedUpdate() {
        if (!TilesManager.Instance) return;
        
        float displacement = TilesManager.Instance.TileSpeed * Time.fixedDeltaTime;

        List<int> childToRemove = new List<int>();
        foreach (var tile in _childTiles) {
            tile.transform.position += Vector3.down * displacement;
            if (tile.transform.position.y <= TilesManager.Instance.TileBottomHeight) {
                TilesManager.Instance.GiveBackTile(tile);
                childToRemove.Add(_childTiles.IndexOf(tile));
            }
        }
        foreach (var index in childToRemove) {
            _childTiles.RemoveAt(index);
        }
        
        UpdateDebugDisplay(0.2f);
    }

    public float GridPos (int index) {
        if (e == 0 || n == 0) return transform.position.x;
        float Ec = e / n;
        // Calcule - moitié espacement + diff col + centre Col(player)
        return transform.position.x - (e / 2) + index * Ec + (Ec / 2);
    }

    public bool CheckCollision(int index, float height, out float hit)
    {
        bool result = false;
        hit = TilesManager.Instance.TileTopHeight;
        
        foreach (var tile in _childTiles) {
            if (tile.GridPos == index && hit > tile.transform.position.y) {
                hit = tile.transform.position.y;

                if (hit - TilesManager.Instance.TileCollider.y < height + TilesManager.Instance.TileCollider.y) {
                    result = true;
                }
            }
        }

        return result;
    }

    public void CallCollision(int index, Vector2 dir) {
        
    }
    

    [Button]
    private void UpdateDebugDisplay(float time = 2f) {
        for (int i = 0; i < n; i++) {
            float x = GridPos(i);
            Debug.DrawLine(new Vector2(x, -100), new Vector2(x, 100), Color.red, time, false);
        }
        Debug.DrawLine(new Vector2(transform.position.x - (e / 2), -100), new Vector2(transform.position.x - (e / 2), 100), Color.green, time, false);
        Debug.DrawLine(new Vector2(transform.position.x + (e / 2), -100), new Vector2(transform.position.x + (e / 2), 100), Color.green, time, false);

        if (!TilesManager.Instance) return;
        float Ec = (e / 2) + 5;
        Debug.DrawLine(new Vector2(-Ec + transform.position.x, transform.position.y + TilesManager.Instance.TileTopHeight - TilesManager.Instance.TileCollider.y), new Vector2(Ec + transform.position.x, transform.position.y + TilesManager.Instance.TileTopHeight - TilesManager.Instance.TileCollider.y), Color.blue, time, false);
        Debug.DrawLine(new Vector2(-Ec + transform.position.x, transform.position.y + TilesManager.Instance.TileBottomHeight - TilesManager.Instance.TileCollider.y), new Vector2(Ec + transform.position.x, transform.position.y + TilesManager.Instance.TileBottomHeight - TilesManager.Instance.TileCollider.y), Color.blue, time, false);
    }

    public void AddChildTile(TileBehaviour a_tile, int index) {
        //Debug.Log($"Grid : SetTile {a_tile.name} | {index}");
        a_tile.transform.position = new Vector2(GridPos(index), a_tile.transform.position.y);
        _childTiles.Add(a_tile);
    }
}
