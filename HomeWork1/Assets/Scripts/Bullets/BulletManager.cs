using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletManager : MonoBehaviour
    {
        [SerializeField]
        public Bullet prefab;

        [SerializeField]
        public Transform worldTransform;

        [SerializeField]
        private LevelBounds levelBounds;

        [SerializeField]
        private Transform container;

        public readonly HashSet<Bullet> m_activeBullets = new();
        public readonly Queue<Bullet> m_bulletPool = new();
        private readonly List<Bullet> m_cache = new();

        private void Awake()
        {
            prefab.OnCollisionEntered += RemoveBullet;

            for (var i = 0; i < 10; i++)
            {
                Bullet bullet = Instantiate(this.prefab, this.container);
                this.m_bulletPool.Enqueue(bullet);
            }
        }

        private void FixedUpdate()
        {
            this.m_cache.Clear();
            this.m_cache.AddRange(this.m_activeBullets);

            for (int i = 0, count = this.m_cache.Count; i < count; i++)
            {
                Bullet bullet = this.m_cache[i];
                if (!this.levelBounds.InBounds(bullet.transform.position))
                {
                    this.RemoveBullet(bullet);
                }
            }
        }

        public void SpawnBullet(BallLightning bulletSetup)
        {
            if (this.m_bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(this.worldTransform);
            }
            else
            {
                bullet = Instantiate(this.prefab, this.worldTransform);
            }

            bullet.transform.position = bulletSetup.position;
            bullet.spriteRenderer.color = bulletSetup.color;
            bullet.gameObject.layer = bulletSetup.physicsLayer;
            bullet.damage = bulletSetup.damage;
            bullet.isPlayer = bulletSetup.isPlayer;
            bullet.rigidbody2D.velocity = bulletSetup.velocity;

            if (m_activeBullets.Add(bullet))
            {
            }
        }

        private void RemoveBullet(Bullet bullet)
        {
            Debug.Log(bullet);
            prefab.OnCollisionEntered -= RemoveBullet;

            if (this.m_activeBullets.Remove(bullet))
            {
               // bullet.OnCollisionEntered -= this.OnBulletCollision;
                bullet.transform.SetParent(this.container);
                this.m_bulletPool.Enqueue(bullet);
            }
        }
    }
}