using UnityEngine;
using Managers;
using System;
using System.Collections;
using NaughtyAttributes;

[Serializable]
public enum PlayerId { P1, P2 }

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private PlayerId playerId;
    public PlayerId GetPlayerId { get => playerId; }
    [Header("Grid")]
    [SerializeField] private TileGrid grid;
    [SerializeField, ReadOnly] private int playerPos;
    [Header("Tiles")]
    [SerializeField] private PlayerTileStacking _tileStack;
    [Header("Moving")]
    [SerializeField] private Vector2 _timeToMove;
    [SerializeField] private int _maxSpeedDebuf;
    [SerializeField] private float _squishFactor;
    private Vector3 _startScale;
    
    private void OnDisable() {
        switch (playerId) {

            case PlayerId.P2:
                PlayerInputCapture.Instance.OnP2MoveStart -= PlayerMoveStart;
                break;

            case PlayerId.P1:
            default:
                PlayerInputCapture.Instance.OnP1MoveStart -= PlayerMoveStart;
                break;
        }

    }

    private void Start() {
        playerPos = (int)(TilesManager.Instance.NbCol / 2);
        Reposition(playerPos);
        
        _startScale = transform.localScale;
        
        switch (playerId) {

            case PlayerId.P2:
                PlayerInputCapture.Instance.OnP2MoveStart += PlayerMoveStart;
                break;

            case PlayerId.P1:
            default:
                PlayerInputCapture.Instance.OnP1MoveStart += PlayerMoveStart;
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (grid.CheckCollision(playerPos, _tileStack.CurrentHeight, out TileBehaviour tile, out float hit))
        {
            case 1:
                _tileStack.Stack(tile);
                break;
            case >= 2:
                _tileStack.Collid(hit);
                break;
            case <= 0:
            default:
                break;
        }
    }

    private void PlayerMoveStart(Vector2 movementValue) {
        
        if (movementValue.x < 0) {
            playerPos = playerPos <= 0 ? playerPos : playerPos - 1;
        }
        else if (movementValue.x > 0) {
            playerPos = playerPos >= TilesManager.Instance.NbCol - 1 ? playerPos : playerPos + 1;
        }
        else return;

        StartCoroutine(Reposition(playerPos));
    }

    private IEnumerator Reposition(int index) {
        Vector2 start = transform.position;
        Vector2 target = new Vector2(grid.GridPos(index), transform.position.y);
        
        float startTime = Mathf.Lerp(_timeToMove.x, _timeToMove.y, (float)_tileStack.CurrentNbTiles / (float)_maxSpeedDebuf);
        float time = startTime;
        while (time > 0) {
            time -= Time.deltaTime;
            transform.position = Vector2.Lerp(target, start, time / startTime);
            transform.localScale = new Vector3(Mathf.Lerp(_startScale.x, _startScale.x * _squishFactor, (time / startTime) >= 0.5f ? (time / startTime) - 0.5f : (1 - (time / startTime)) - 0.5f), _startScale.y, _startScale.z);
            yield return new WaitForFixedUpdate();
        }

        transform.position = target;
        transform.localScale = _startScale;
        
        yield break;
    }
    
    
}
