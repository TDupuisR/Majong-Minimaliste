using UnityEngine;
using System.Collections.Generic;
using Managers;
using NaughtyAttributes;

public class PlayerTileStacking : MonoBehaviour
{
    [SerializeField] private PlayerBehavior _playerBehavior;
    [SerializeField] private Transform _playerCollider;
    [Space(7)]
    [SerializeField, ReadOnly] private List<TileBehaviour> _stackedTiles;
    public float CurrentHeight { get => _stackedTiles.Count > 0 ? _stackedTiles[^1].transform.position.y : _playerCollider.position.y; }
    public int CurrentNbTiles { get => _stackedTiles.Count; }

    public void Stack(TileBehaviour tile) {
        tile.transform.SetParent(_playerCollider.transform);
        tile.transform.position = new Vector2(_playerCollider.position.x, CurrentHeight + (TilesManager.Instance.TileCollider.y * 2));
        _stackedTiles.Add(tile);

        Unstack(ComboManager.Instance.CheckCombos(_stackedTiles.ToArray()));
    }

    public void Collid(float hit) {
        for (int i = _stackedTiles.Count - 1; i >= 0; i--) {
            if (hit - TilesManager.Instance.TileCollider.y < _stackedTiles[i].transform.position.y + TilesManager.Instance.TileColliderWTolerance.y) {
                TilesManager.Instance.GiveBackTile(_stackedTiles[i]);
                _stackedTiles.RemoveAt(i);
            }
            else break;
        }
    }

    public void Unstack(int nb) {
        if (nb <= 0) return;

        for (int i = _stackedTiles.Count - 1; i >= 0 && nb > 0; i--) {
            TilesManager.Instance.GiveBackTile(_stackedTiles[i]);
            _stackedTiles.RemoveAt(i);
            nb--;
        }
        
        RoundManager.Instance.SendDamage(_playerBehavior.GetPlayerId);
    }

    [Button]
    private void ShowStackCollider() {
        SeeCollider(_playerCollider.position);

        foreach (TileBehaviour tile in _stackedTiles) {
            SeeCollider(tile.transform.position);
        }
    }
    private void SeeCollider(Vector2 position) {
        Vector2 size = TilesManager.Instance.TileCollider;
        Debug.DrawLine(new Vector2(position.x + size.x, position.y + size.y), new Vector2(position.x - size.x, position.y + size.y), Color.darkCyan, 2f, false);
        Debug.DrawLine(new Vector2(position.x + size.x, position.y + size.y), new Vector2(position.x + size.x, position.y - size.y), Color.darkCyan, 2f, false);
        Debug.DrawLine(new Vector2(position.x - size.x, position.y - size.y), new Vector2(position.x + size.x, position.y - size.y), Color.darkCyan, 2f, false);
        Debug.DrawLine(new Vector2(position.x - size.x, position.y - size.y), new Vector2(position.x - size.x, position.y + size.y), Color.darkCyan, 2f, false);
    }
}
