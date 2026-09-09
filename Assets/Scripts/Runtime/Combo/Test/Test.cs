using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
    [SerializeField] private List<TileData> listTile;
    [SerializeField] private ComboData combo;

    [Button]
    private void Teste() {
        Debug.Log(combo.CheckCombo(listTile.ToArray()));
    }
}
