using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Enemy : MonoBehaviour, IEnemy
    {

        public int Health
        {
            get => _health;
            set => _health = value;
        }

        public bool IsPlayer => _isPlayer;

        public Transform Transform => transform;

        public IPlayer Target { get ; set; }

        [SerializeField]
        public Transform firePoint;

        [SerializeField]
        public Rigidbody2D _rigidbody;

        [SerializeField]
        public float speed = 5.0f;

        [SerializeField]
        private bool _isPlayer;

        [SerializeField]
        private int _health;

        [SerializeField]
        private float countdown;

        [SerializeField]
        private IBulletManager bulletManager;

        private Vector2 destination;
        private float currentTime;
        private bool isPointReached;

        public void Reset()
        {
            this.currentTime = this.countdown;
        }

        public void SetDestination(Vector2 endPoint)
        {
            this.destination = endPoint;
            this.isPointReached = false;
        }
        private void Start()
        {
            bulletManager = GameObject.FindWithTag(Consts.BULLETMANAGER).GetComponent<BulletManager>();
        }

        private void FixedUpdate()
        {
            if (this.isPointReached)
            {
                Attack();
            }
            else
            {
                Move();
            }
        }

        private void Move()
        {
            //Move:
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

        private void Attack()
        {
            //Attack:
            if (this.Target.Health <= 0)
                return;

            this.currentTime -= Time.fixedDeltaTime;
            if (this.currentTime <= 0)
            {
                Vector2 startPosition = this.firePoint.position;
                Vector2 vector = (Vector2)this.Target.Transform.position - startPosition;
                Vector2 direction = vector.normalized;
                Fire(startPosition, direction);

                this.currentTime += this.countdown;
            }
        }

        private void Fire(Vector2 position, Vector2 direction)
        {
            bulletManager.SpawnBullet(
                position,
                Color.red,
                (int)PhysicsLayer.ENEMY_BULLET,
                1,
                false,
                direction * 2
            );
        }
    }
}