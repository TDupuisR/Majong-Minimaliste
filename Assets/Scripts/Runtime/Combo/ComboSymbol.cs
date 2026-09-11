using UnityEngine;

[CreateAssetMenu(fileName = "ComboSymbol", menuName = "Scriptable Objects/ComboSymbol")]
public class ComboSymbol : ComboData
{


    public override bool CheckCombo (TileData[] tiles) {
        int m = 1;

        for (int i = tiles.Length - 1; i > 0; i--) {
            if (tiles[i].TileFamily == tiles[i - 1].TileFamily) {
                m++;
            }
            else m = 1;

            if (m >= n) return true;
        }
        return false;
    }
}
