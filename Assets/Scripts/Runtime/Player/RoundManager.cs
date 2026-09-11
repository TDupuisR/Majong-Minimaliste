using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

namespace Managers
{
    public class RoundManager : MonoBehaviour
    {
        private static RoundManager instance;
        public static RoundManager Instance
        {
            get => instance;
        }
        
        [SerializeField] private PlayerHealth _player1;
        [SerializeField] private PlayerHealth _player2;
        [Space(7)]
        [SerializeField] private GameObject _buttonStart;
        [SerializeField] private GameObject _winner1Sprite;
        [SerializeField] private GameObject _winner2Sprite;
        [SerializeField, ReadOnly]private bool _player1Ready = false;
        [SerializeField] private Image _button1;
        [SerializeField, ReadOnly] private bool _player2Ready = false;
        [SerializeField] private Image _button2;

        private void Start() {
            ResetLives();
            
            _buttonStart.SetActive(true);
            _button1.color = Color.white;
            _player1Ready = false;
            _button2.color = Color.white;
            _player2Ready = false;
            
            _winner1Sprite.SetActive(false);
            _winner2Sprite.SetActive(false);
        }
        
        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(this);
                return;
            }
            else instance = this;
        }

        public void SendDamage(PlayerId id) {
            if (id == PlayerId.P1) _player2.TakeDamage();
            else if (id == PlayerId.P2) _player1.TakeDamage();
            
            CheckWinner();
        }
        private void CheckWinner() {
            if (_player1.Health <= 0) {
                TilesManager.Instance.CanSpawn = false;
                
                _winner2Sprite.SetActive(true);
                
                _buttonStart.SetActive(true);
                _button1.color = Color.white;
                _player1Ready = false;
                _button2.color = Color.white;
                _player2Ready = false;
            }
            else if (_player2.Health <= 0) {
                TilesManager.Instance.CanSpawn = false;
                
                _winner2Sprite.SetActive(true);
                
                _buttonStart.SetActive(true);
                _button1.color = Color.white;
                _player1Ready = false;
                _button2.color = Color.white;
                _player2Ready = false;
            }
        }

        public void ResetLives() {
            _player1.ResetHealt();
            _player2.ResetHealt();
        }

        public void Player1Ready() {
            _player1Ready = !_player1Ready;
            _button1.color = _player1Ready ? Color.green : Color.white;
            PlayersReady();
        }
        public void Player2Ready() {
            _player2Ready = !_player2Ready;
            _button2.color = _player2Ready ? Color.green : Color.white;
            PlayersReady();
        }
        public void PlayersReady() {
            if (_player1Ready && _player2Ready) {
                ResetLives();
                _buttonStart.SetActive(false);
                _winner1Sprite.SetActive(false);
                _winner2Sprite.SetActive(false);
                TilesManager.Instance.CanSpawn = true;
            }
        }
    }
}

