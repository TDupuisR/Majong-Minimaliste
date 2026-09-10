using UnityEngine;

[CreateAssetMenu(fileName = "ComboDecrease", menuName = "Scriptable Objects/ComboDecrease")]
public class ComboDecrease : ComboData
{


    public override bool CheckCombo (TileData[] tiles) {
        int m = 1;

        for (int i = tiles.Length - 1; i > 0; i--) {
            if (tiles[i].Value == tiles[i - 1].Value - 1) {
                m++;
            }
            else m = 1;

            if (m >= n) return true;
        }
        return false;
    }
}
