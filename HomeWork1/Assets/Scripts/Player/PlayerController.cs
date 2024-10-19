using System;
using UnityEngine;

namespace ShootEmUp
{

    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Player character;

        [SerializeField]
        private EnemyManager enemyManager;

        [SerializeField]
        private BulletManager bulletManager;



        private void Start() 
        {
         //   this.character.OnHealthEmpty += _ => Time.timeScale = 0;

            character.OnPlayerShoot += bulletManager.SpawnBullet;
            enemyManager.OnEnemyShoot += bulletManager.SpawnBullet;
        }
    }
}