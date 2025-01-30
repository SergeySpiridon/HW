using UnityEngine;

namespace ShootEmUp
{
    public class SceneWatcher : MonoBehaviour
    {
        [SerializeField]
        public Player player;
        private void Start()
        {
            this.player.OnHealthEmpty += _ => Time.timeScale = 0;
        }
    }
}