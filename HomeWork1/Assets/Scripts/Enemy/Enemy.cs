using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Enemy : Entity
    {

        public delegate Transform TransformHandler();
        public event TransformHandler OnPlayerFound;

        public event Action<BallLightning> OnEnemyShoot;

        [SerializeField]
        private float countdown;

        private IBullet _bullet;

        private Vector2 destination;
        private float currentTime;
        private bool isPointReached;
        private bool _availableShoot;
        private Player _player;

        private void Start()
        {
            _player = FindObjectOfType<Player>();
            _bullet = GetComponent<BallLightning>();
        }

        private void FixedUpdate()
        {
            if (this.isPointReached)
            {
                Shoot();
            }
            else
            {
                Move();
            }
        }
        public void Reset()
        {
            this.currentTime = this.countdown;
        }

        public void SetDestination(Vector2 endPoint)
        {
            this.destination = endPoint;
            this.isPointReached = false;
        }

        public void ChangeAvailableShoot() =>
            _availableShoot = false;

        protected override void Move()
        {
            Vector2 vector = this.destination - (Vector2)this.transform.position;
            if (vector.magnitude <= 0.25f)
            {
                this.isPointReached = true;
                return;
            }

            Vector2 direction = vector.normalized * Time.fixedDeltaTime;
            Vector2 nextPosition = _rigidbody.position + direction * speed;
            _rigidbody.MovePosition(nextPosition);
        }

        protected override void Shoot()
        {
            if (_availableShoot)
            {
                return;

            }

            this.currentTime -= Time.fixedDeltaTime;
            if (this.currentTime <= 0)
            {
                Vector2 startPosition = this.firePoint.position;
                // Debug.Log(_player.GetPosition().position);
                //Debug.Log(OnPlayerFound?.Invoke().position);
                //Vector2 vector = (Vector2)OnPlayerFound?.Invoke().position - startPosition;
                Vector2 vector = (Vector2)_player.GetPosition().position - startPosition;

                Vector2 direction = vector.normalized;
                OnFire(startPosition, direction);

                this.currentTime += this.countdown;
            }
        }
        private void OnFire(Vector2 position, Vector2 direction)
        {
            OnEnemyShoot?.Invoke(_bullet.SetupBullet(position, direction * 2, this));

           // Debug.Log("33");

        }

    }
}