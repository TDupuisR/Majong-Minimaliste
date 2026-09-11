using UnityEngine;
using System.Collections.Generic;

namespace Managers
{
    public class ComboManager : MonoBehaviour
    {
        private static ComboManager instance;
        public static ComboManager Instance
        {
            get => instance;
        }
        
       [SerializeField] private List<ComboData> _combos;

       private void Awake() {
           if (Instance != null && Instance != this) {
               Destroy(this);
               return;
           }
           else instance = this;
       }

       public int CheckCombos(TileBehaviour[] tile)
       {
           TileData[] result = GetAllTileData(tile);
           int n = 0;

           foreach (var combo in _combos) {
               if (combo.CheckCombo(result)) {
                   n = combo.GetN > n ? combo.GetN : n;
               }
           }

           return n;
       }

       public TileData[] GetAllTileData(TileBehaviour[] tiles)
       {
           TileData[] result = new TileData[tiles.Length];

           for (int i = 0; i < tiles.Length; i++) {
               result[i] = tiles[i].GetData;
           }
           
           return result;
       }
    }
}

