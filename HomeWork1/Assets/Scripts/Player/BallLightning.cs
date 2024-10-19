using UnityEngine;

namespace ShootEmUp
{
    public class BallLightning : MonoBehaviour, IBullet
    {
        public Vector2 position;
        public Color color;
        //PhysicsLayer.PLAYER_BULLET = 14
        //PhysicsLayer.ENEMY_BULLET = 13,
        public int physicsLayer;
        public int damage;
        public bool isPlayer;
        public Vector2 velocity;

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