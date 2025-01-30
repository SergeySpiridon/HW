using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Player character;

        public bool FireRequired { get; set;}
        public float moveDirection {get; private set;}

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                FireRequired = true;

            if (Input.GetKey(KeyCode.LeftArrow))
                this.moveDirection = -1;
            else if (Input.GetKey(KeyCode.RightArrow))
                this.moveDirection = 1;
            else
                this.moveDirection = 0;
        }
        private void FixedUpdate()
        {
            if (FireRequired)
            {
                character.ShootBullet();
                FireRequired = false;
            }

            character.MovePlayer(moveDirection);
        }
    }
}
