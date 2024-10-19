using UnityEngine;

namespace ShootEmUp
{
    public class ShootController : IShootable
    {
        public bool Shoot()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                return true;
            return false;
        }
    }
}