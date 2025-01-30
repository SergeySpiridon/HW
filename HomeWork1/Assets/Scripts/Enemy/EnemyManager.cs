using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private Transform[] spawnPositions;

        [SerializeField]
        private Transform[] attackPositions;
        
        [SerializeField]
        private IPlayer character;

        [SerializeField]
        private Transform worldTransform;

        [SerializeField]
        private Transform container;

        //Тут можно будет использовать абстрактный класс, если будут другие енеми обжекты, для абстракции
        [SerializeField]
        private Enemy prefab;
        
        
        private readonly HashSet<IEnemy> m_activeEnemies = new();
        private readonly Queue<IEnemy> enemyPool = new();
        
        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag(Consts.PLAYER).GetComponent<Player>();
            for (var i = 0; i < 7; i++)
            {
                IEnemy enemy = Instantiate(this.prefab, this.container);
                this.enemyPool.Enqueue(enemy);
            }
        }

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(1, 2));
                
                if (!this.enemyPool.TryDequeue(out IEnemy enemy))
                {
                    enemy = Instantiate(this.prefab, this.container);
                }

                enemy.Transform.SetParent(this.worldTransform);

                Transform spawnPosition = this.RandomPoint(this.spawnPositions);
                enemy.Transform.position = spawnPosition.position;

                Transform attackPosition = this.RandomPoint(this.attackPositions);
                enemy.SetDestination(attackPosition.position);
                enemy.Target = this.character;

                this.m_activeEnemies.Add(enemy);
            }
        }

        private void FixedUpdate()
        {
            foreach (IEnemy enemy in m_activeEnemies.ToArray())
            {
                if (enemy.Health <= 0)
                {
                    enemy.Transform.SetParent(this.container);

                    m_activeEnemies.Remove(enemy);
                    this.enemyPool.Enqueue(enemy);
                }
            }
        }

        private Transform RandomPoint(Transform[] points)
        {
            int index = Random.Range(0, points.Length);
            return points[index];
        }
    }
}