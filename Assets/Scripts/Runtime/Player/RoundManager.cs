using UnityEngine;

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
        }

        public void ResetLives() {
            _player1.ResetHealt();
            _player2.ResetHealt();
        }
    }
}

