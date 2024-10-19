using System;
using UnityEngine;

namespace ShootEmUp
{
    public abstract class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnCollisionEntered;


        [NonSerialized]
        public bool isPlayer;
        
        [SerializeField]
        public int damage;

        [SerializeField]
        public new Rigidbody2D rigidbody2D;

        [SerializeField]
        public SpriteRenderer spriteRenderer;

        public Vector2 position;
        public Color color;
        public int physicsLayer;
        public Vector2 velocity;

        //PhysicsLayer.PLAYER_BULLET = 14
        //PhysicsLayer.ENEMY_BULLET = 13,

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            DealDamage(collision.gameObject);
            OnCollisionEntered?.Invoke(this);
        }


        protected void DealDamage(GameObject other)
        {
            Debug.Log(other);
            if (damage <= 0)
                return;

            if (other.TryGetComponent(out Player player))
            {
                if (isPlayer != player.isPlayer)
                {
                    if (player.health <= 0)
                        return;

                    player.health = Mathf.Max(0, player.health - damage);
                    player.OnHealthChanged?.Invoke(player, player.health);

                    if (player.health <= 0)
                        player.OnHealthEmpty?.Invoke(player);
                }
            }
            else if (other.TryGetComponent(out Enemy enemy))
            {
                if (isPlayer != enemy.isPlayer)
                {
                    if (enemy.health > 0)
                    {
                        enemy.health = Mathf.Max(0, enemy.health - damage);
                    }
                }
            }
        }
    }
}