using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
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
        [Header("Tile Infos")]
        [SerializeField, ReadOnly] private List<TileBehaviour> _tilesPool;
        [SerializeField] private int _sizeOfTilesPool;
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField, ReadOnly] private bool[] _isTileUsed;
        [Space(7)]
        [SerializeField] private List<TileData> _tileDatas;
        [SerializeField, ReadOnly] private int _lastTile;
        [SerializeField, ReadOnly] private int _lastCol;
        [Header("Tile Falling")]
        [SerializeField, Tooltip("Min & Max falling speed")] private Vector2 _tileSpeed;
        [SerializeField, Range(0, 1)] private float _speedProgression;
        public float TileSpeed { get => Mathf.Abs(Mathf.Lerp(_tileSpeed.x, _tileSpeed.y, _speedProgression)); }
        [SerializeField] private float _topTilePos;
        public float TileTopHeight { get => _topTilePos; }
        [SerializeField] private float _bottomTilePos;
        public float TileBottomHeight { get => _bottomTilePos; }
        [Header("Spawning")]
        [SerializeField] private bool _canSpawn;
        public bool CanSpawn
        {
            set
            {
                if (_canSpawn == value) return;

                if (value) {
                    timer = 0f;
                    _canSpawn = true;
                }
                else _canSpawn = false;
            }
        }
        [SerializeField, Tooltip("Min & Max tiles/minutes")] private Vector2 _spawnFrequency;
        [SerializeField, Range(0, 1)] private float _spawnProgression;
        public float SpawnFrequency { get => 60f / Mathf.Abs(Mathf.Lerp(_spawnFrequency.x, _spawnFrequency.y, _spawnProgression)); }
        private float timer = 0;
        [Header("Collision")]
        [SerializeField] private Vector2 _tileColliderSize = Vector2.one;
        [SerializeField, Range(0, 1)] private float _tileColliderTolerance = 0.8f;
        public Vector2 TileColliderWTolerance
        {
            get => new Vector2(_tileColliderSize.x * _tileColliderTolerance * transform.localScale.x,
                _tileColliderSize.y * _tileColliderTolerance * transform.localScale.y);
        }
        public Vector2 TileCollider
        {
            get => new Vector2(_tileColliderSize.x * transform.localScale.x,
                _tileColliderSize.y * transform.localScale.y);
        }
        
        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(this);
                return;
            }
            else instance = this;

            _lastTile = -1;
            _lastCol = -1;
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
            
            GeneratePool();
        }

        private void FixedUpdate() {
            if (_canSpawn) {
                if (timer <= 0) {
                    SpawnTile();
                    timer += SpawnFrequency;
                }
                timer -= Time.fixedDeltaTime;

                //Debug.Log($"timer : {timer} / {SpawnFrequency}");
            }
        }
        
        [Button("Generate Tile Pool")]
        private void GeneratePool() {
            foreach (TileBehaviour tile in _tilesPool) {
                if (tile) DestroyImmediate(tile.gameObject);
            }
            _tilesPool.Clear();

            for (int i = 0; i < _sizeOfTilesPool; i++) {
                GameObject tile = Instantiate(_tilePrefab);
                tile.name = $"TileNb[{i}]";

                if (tile.TryGetComponent(out TileBehaviour behaviour)) {
                    _tilesPool.Add(behaviour);
                }
                else {
                    Debug.LogError($"Missing component TileBehaviour on tile prefab", tile);
                    break;
                }
            }

            _isTileUsed = new bool[_tilesPool.Count];
            Debug.Log($"Generated {_tilesPool.Count} tiles of {_sizeOfTilesPool}");

            foreach (var tile in _tilesPool) {
                GiveBackTile(tile);
            }
        }

        public void GiveBackTile(TileBehaviour a_tile) {
            int index = _tilesPool.IndexOf(a_tile);
            _isTileUsed[index] = false;
            a_tile.ResetData();
            a_tile.transform.position = transform.position;
            a_tile.transform.SetParent(transform);
        }

        public void SpawnTile() {
            Debug.Log("Spawning Start");
            int[] tilesIndex = new int[_grids.Count];
            Array.Fill(tilesIndex, -1);
            
            for (int i = 0; i < _grids.Count; i++) {
                for (int j = 0; j < _isTileUsed.Length; j++)
                {
                    if (_isTileUsed[j] == false && !tilesIndex.Contains(j))
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
            
            int tile;
            do
            {
                tile = Random.Range(0, _tileDatas.Count);
            } while (tile == _lastTile);
            int col;
            do
            {
                col = Random.Range(0, NbCol);
            } while (col == _lastCol);
            
            for (int i = 0; i < _grids.Count; i++) {
                _tilesPool[tilesIndex[i]].ApplyData(_tileDatas[tile], col);
                _tilesPool[tilesIndex[i]].transform.position = Vector3.up * _topTilePos;
                _grids[i].AddChildTile(_tilesPool[tilesIndex[i]], col);
                _isTileUsed[tilesIndex[i]] = true;
            }
            
            _lastTile = tile;
            _lastCol = col;
        }
    }
}
