using UnityEngine;

[CreateAssetMenu(fileName = "ComboNum", menuName = "Scriptable Objects/ComboNum")]
public class ComboNum : ComboData
{


    public override bool CheckCombo (TileData[] tiles) {
        int m = 1;

        for (int i = tiles.Length - 1; i > 0; i--) {
            if (tiles[i].Value == tiles[i - 1].Value) {
                m++;
            }
            else m = 1;

            if (m >= n) return true;
        }
        return false;
    }
}
