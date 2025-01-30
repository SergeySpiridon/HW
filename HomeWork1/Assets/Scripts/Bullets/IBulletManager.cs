using UnityEngine;

namespace ShootEmUp
{
    public interface IBulletManager
    {
        void SpawnBullet(Vector2 position, Color color, int physicsLayer, int damage, bool isPlayer, Vector2 velocity);
    }
}