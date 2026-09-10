using UnityEngine;
using NaughtyAttributes;

public class TileGrid : MonoBehaviour
{
    [SerializeField] private int n;
    [SerializeField] private float e;

    public float GridPos (int index) {
        if (e == 0 || n == 0) return transform.position.x;
        float Ec = e / n;
        // Calcule - moitié espacement + diff col + centre Col(player)
        return transform.position.x - (e / 2) + index * Ec + (Ec / 2);
        
    }

    [Button]
    private void Update() {
        for (int i = 0; i < n; i++) {
            float x = GridPos(i);
            Debug.DrawLine(new Vector2(x, -100), new Vector2(x, 100), Color.red, 2.0f, false);
        }
        Debug.DrawLine(new Vector2(transform.position.x - (e / 2), -100), new Vector2(transform.position.x - (e / 2), 100), Color.green, 2.0f, false);
        Debug.DrawLine(new Vector2(transform.position.x + (e / 2), -100), new Vector2(transform.position.x + (e / 2), 100), Color.green, 2.0f, false);
    }
}
