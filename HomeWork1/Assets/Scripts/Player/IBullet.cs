using UnityEngine;

namespace ShootEmUp
{
    public interface IBullet
    {
        BallLightning SetupBullet<T>(Transform entity, T owner) where T : class;
        BallLightning SetupBullet<T>(Vector2 entity, Vector2 rotation, T owner) where T : class;

    }
}