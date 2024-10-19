using UnityEngine;

namespace ShootEmUp
{
    public class MoveController : IController
    {
        private int _moveDirection;

        public int Move()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                return _moveDirection = -1;

            else if (Input.GetKey(KeyCode.RightArrow))
                return _moveDirection = 1;

            else
                return _moveDirection = 0;
        }
    }
}