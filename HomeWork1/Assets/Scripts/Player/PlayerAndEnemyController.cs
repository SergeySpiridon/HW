using UnityEngine;

namespace ShootEmUp
{
    public class PlayerAndEnemyController: MonoBehaviour
    {
        [SerializeField]
        private Player _player;
        
        private Enemy _enemy;

        //public void Subscribe(Enemy enemy)
        //{
        //    _enemy = enemy;
        //    _player.OnHealthEmptyz += _enemy.ChangeAvailableShoot;
        //    _enemy.OnPlayerFound += _player.GetPosition;
        //}
        //public void Unscribe(Enemy enemy)
        //{
        //    _enemy = enemy;
        //    //_player.OnHealthEmptyz -= _enemy.ChangeAvailableShoot;
        //    _enemy.OnPlayerFound -= _player.GetPosition;
        //}
    }
}