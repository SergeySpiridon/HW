using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace ShootEmUp
{
    public sealed class Player : MonoBehaviour, IPlayer
    {
        public Action<Player> OnHealthEmpty;

        public int Health
        {
            get => _health;
            set => _health = value;
        }

        public Transform Transform => transform;

        public bool IsPlayer => isPlayer;

        [SerializeField]
        public Rigidbody2D _rigidbody;

        [SerializeField]
        public float speed = 5.0f;

        [SerializeField]
        private bool isPlayer;

        [SerializeField]
        private Transform firePoint;

        [SerializeField]
        private int _health;

        [SerializeField]
        private IBulletManager bulletManager;


        private void Start()
        {
            bulletManager = GameObject.FindWithTag(Consts.BULLETMANAGER).GetComponent<BulletManager>();
        }

        private void FixedUpdate()
        {
            if (_health == 0)
            {
                OnHealthEmpty?.Invoke(this);
            }
        }

        public void ShootBullet()
        {
            bulletManager.SpawnBullet(
                                firePoint.position,
                                Color.blue,
                                (int)PhysicsLayer.PLAYER_BULLET,
                                1,
                                true,
                                firePoint.rotation * Vector3.up * 3
                            );
        }

        public void MovePlayer(float _moveDirection)
        {
            Vector2 moveDirection = new Vector2(_moveDirection, 0);
            Vector2 moveStep = moveDirection * Time.fixedDeltaTime * speed;
            Vector2 targetPosition = _rigidbody.position + moveStep;
            _rigidbody.MovePosition(targetPosition);
        }
    }
}
