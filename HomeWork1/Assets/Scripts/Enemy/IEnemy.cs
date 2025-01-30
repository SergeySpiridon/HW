using UnityEngine;

namespace ShootEmUp
{
    public interface IEnemy
    {
        int Health { get; set; }
        bool IsPlayer { get; }
        Transform Transform { get; }
        IPlayer Target { get; set; }
        void SetDestination(Vector2 endPoint);
    }
}