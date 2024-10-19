using UnityEngine;

namespace ShootEmUp
{
    public class BallLightning : Bullet, IBullet
    {
        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);
        }

        public BallLightning SetupBullet<T>(Transform entity, T owner) where T : class
        {
            this.position = entity.position;
            velocity = entity.rotation * Vector3.up * 3;
            isPlayer = owner is Player;

            return this;
        }

        public BallLightning SetupBullet<T>(Vector2 position, Vector2 rotation, T owner) where T : class
        {
            this.position = position;
            velocity = position * Vector3.up * 3;
            isPlayer = true;
      
            return this;
        }

    }
}