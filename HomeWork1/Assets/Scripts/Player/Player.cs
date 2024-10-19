using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

namespace ShootEmUp
{
    public sealed class Player : Entity
    {
        
        public Action<Player, int> OnHealthChanged;
        public Action<Player> OnHealthEmpty;
        public Action OnHealthEmptyz;
        public event Action<BallLightning> OnPlayerShoot;

        private IController _moveController = new MoveController();
        private IShootable _shootController = new ShootController();
        private IBullet _bullet;

        private bool _fireRequired;
        private int _moveDirection;

        private void Start()
        {
           _bullet = GetComponent<BallLightning>();
        }

        private void Update()
        {
            _fireRequired = _shootController.Shoot();
            _moveDirection = _moveController.Move();
            Shoot();
            Move();

            if (health <= 0)
            {
                OnHealthEmptyz?.Invoke();
            }
        }

        public Transform GetPosition()
        {
            return this.transform;
        }

        protected override void Move()
        {

            Vector2 moveDirection = new Vector2(this._moveDirection, 0);
            Vector2 moveStep = moveDirection * Time.fixedDeltaTime * this.speed;
            Vector2 targetPosition = this._rigidbody.position + moveStep;
            this._rigidbody.MovePosition(targetPosition);
        }

        protected override void Shoot()
        {

            if (_fireRequired)
            {
                OnPlayerShoot?.Invoke(_bullet.SetupBullet(firePoint, this));

                _fireRequired = false;
            }
        }
    }
}