using UnityEngine;
using InputCapture;
using System;

public class PlayerMovement : MonoBehaviour
{
    [Serializable]
    private enum PlayerId { P1, P2 }

    [SerializeField] private PlayerId playerId;

    [SerializeField] private TileGrid grid;
    private int playerPos;

    private void OnEnable() {
        switch (playerId) {

            case PlayerId.P2:
                InputCapture.PlayerInputCapture.Instance.OnP2MoveStart += PlayerMoveStart;
                break;

            case PlayerId.P1:
            default:
                InputCapture.PlayerInputCapture.Instance.OnP1MoveStart += PlayerMoveStart;
                break;
        }

    }
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
        playerPos = (int)(grid.N / 2);
        Reposition(playerPos);
    }

    private void PlayerMoveStart(Vector2 movementValue) {
        if (movementValue.x < 0) {
            playerPos = playerPos <= 0 ? playerPos : playerPos--;
        }
        else if (movementValue.x > 0) {
            playerPos = playerPos > grid.N ? playerPos : playerPos++;
        }
        else return;

        Reposition(playerPos);
    }

    private void Reposition(int index) {
        transform.position = new Vector2(grid.GridPos(index), transform.position.y);
    }
}
