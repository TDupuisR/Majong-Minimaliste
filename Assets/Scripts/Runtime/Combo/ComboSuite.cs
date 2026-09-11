using UnityEngine;

[CreateAssetMenu(fileName = "ComboSuite", menuName = "Scriptable Objects/ComboSuite")]
public class ComboSuite : ComboData
{
    public override bool CheckCombo (TileData[] tiles) {
        int m = 1;
        if (tiles.Length <= 1) return false;

        for (int i = tiles.Length - 1; i > tiles.Length - n && i > 0; i--) {
            Debug.Log($"i : {i} | l - n : {tiles.Length - n}");
            if (tiles[i].Value == tiles[i - 1].Value + 1) {
                m++;
            }
            else m = 1;

            if (m >= n) return true;
        }
        return false;
    }
}
