using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnCollisionEntered;

        [NonSerialized]
        public bool isPlayer;

        [NonSerialized]
        public int damage;

        [SerializeField]
        public new Rigidbody2D rigidbody2D;

        [SerializeField]
        public SpriteRenderer spriteRenderer;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            DealDamage(collision.gameObject);
            this.OnCollisionEntered?.Invoke(this);
        }

        private void DealDamage(GameObject other)
        {
            if (damage <= 0)
                return;

            if (other.TryGetComponent(out IPlayer player))
            {
                if (isPlayer != player.IsPlayer)
                {
                    if (player.Health <= 0)
                        return;

                    player.Health = Mathf.Max(0, player.Health - damage);

                }
            }
            else if (other.TryGetComponent(out IEnemy enemy))
            {
                if (isPlayer != enemy.IsPlayer)
                {
                    if (enemy.Health > 0)
                    {
                        enemy.Health = Mathf.Max(0, enemy.Health - damage);
                    }
                }
            }
        }
    }
}