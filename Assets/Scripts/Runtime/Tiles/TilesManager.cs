using System;
using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Managers
{
    public class TilesManager : MonoBehaviour
    {
        private static TilesManager instance;
        public static TilesManager Instance
        {
            get => instance;
        }

        [SerializeField] private List<TileGrid> _grids;
        [SerializeField] private int _nbCol = 5;
        public int NbCol { get => _nbCol; }
        [Space(7)]
        [SerializeField, ReadOnly] private List<TileBehaviour> _tilesPool;
        [SerializeField] private int _sizeOfTilesPool;
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private bool[] _isTileUsed;
        [Space(7)]
        [SerializeField] private List<TileData> _tileDatas;
        [SerializeField, ReadOnly] private int _lastTile;
        [Space(7)]
        [SerializeField] private Vector2 _tileSpeed;
        [SerializeField, Range(0, 1)] private float _speedProgression;
        public float TileSpeed { get => Mathf.Abs(Mathf.Lerp(_tileSpeed.x, _tileSpeed.y, _speedProgression)); }
        [SerializeField] private float _topTilePos;
        public float TileTopHeight { get => _topTilePos; }
        [SerializeField] private float _bottomTilePos;
        public float TileBottomHeight { get => _bottomTilePos; }
        [Space(7)]
        [SerializeField] private Vector2 _spawnFrequency;
        [SerializeField, Range(0, 1)] private float _spawnProgression;
        public float SpawnFrequency { get => Mathf.Abs(Mathf.Lerp(_spawnFrequency.x, _spawnFrequency.y, _spawnProgression)); }
        
        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(this);
                return;
            }
            else instance = this;
        }

        private void OnValidate() {
            foreach (var grid in _grids) {
                grid.SetNbCol = NbCol;
            }
        }
        private void Start()
        {
            foreach (var grid in _grids) {
                grid.SetNbCol = NbCol;
            }
        }

        [Button("Generate Tile Pool")]
        private void GeneratePool()
        {
            foreach (TileBehaviour tile in _tilesPool) {
                Destroy(tile.gameObject);
            }
            _tilesPool.Clear();

            for (int i = 0; i < _sizeOfTilesPool; i++) {
                GameObject tile = Instantiate(_tilePrefab);

                if (tile.TryGetComponent(out TileBehaviour behaviour)) {
                    _tilesPool.Add(behaviour);
                }
                else {
                    Debug.LogError($"Missing component TileBehaviour on tile prefab", tile);
                    break;
                }
                
                GiveBackTile(behaviour);
            }

            _isTileUsed = new bool[_tilesPool.Count];
            Debug.Log($"Generated {_tilesPool.Count} tiles of {_sizeOfTilesPool}");
        }

        public void GiveBackTile(TileBehaviour a_tile) {
            int index = _tilesPool.IndexOf(a_tile);
            _isTileUsed[index] = false;
            a_tile.ApplyData(null);
            a_tile.transform.position = transform.position;
        }

        public void SpawnTile() {
            int tile;
            do
            {
                tile = Random.Range(0, _tileDatas.Count);
            } while (tile == _lastTile);

            int[] tilesIndex = new int[_grids.Count];
            Array.Fill(tilesIndex, -1);
            
            for (int i = 0; i < _grids.Count; i++) {
                for (int j = 0; j < _isTileUsed.Length; j++)
                {
                    if (_isTileUsed[j] == false)
                    {
                        tilesIndex[i] = j;
                        break;
                    }
                }

                if (tilesIndex[i] < 0) {
                    Debug.LogWarning($"Couldn't spawn tiles, not enough unused", gameObject);
                    return;
                }
            }

            int pos = Random.Range(0, NbCol);
            for (int i = 0; i < _grids.Count; i++) {
                _tilesPool[tilesIndex[i]].transform.position = Vector3.up * _topTilePos;
                _grids[i].AddChildTile(_tilesPool[tilesIndex[i]], pos);
            }
        }
    }
}
