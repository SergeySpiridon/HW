using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Enemy : Entity
    {
        public delegate void FireHandler(Vector2 position, Vector2 direction);
        
        public event FireHandler OnFire;

        [SerializeField]
        private float countdown;

        [NonSerialized]
        public Player target;

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
            if (this.target.health <= 0)
                return;

            this.currentTime -= Time.fixedDeltaTime;
            if (this.currentTime <= 0)
            {
                Vector2 startPosition = this.firePoint.position;
                Vector2 vector = (Vector2)this.target.transform.position - startPosition;
                Vector2 direction = vector.normalized;
                this.OnFire?.Invoke(startPosition, direction);

                this.currentTime += this.countdown;
            }
        }
    }
}