using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlappyBird;

namespace FlappyBird
{
    [AddComponentMenu("Flappy Bird/Flappy Bird Player")]
    public class Player : MonoBehaviour
    {
        private Rigidbody2D rigidbody;
        private float velocity = 5f;
        private float speed=4f;

        void Start()
        {
            rigidbody=GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                if (this.rigidbody.bodyType == RigidbodyType2D.Kinematic)
                {
                    this.rigidbody.bodyType = RigidbodyType2D.Dynamic; // Kinematic이면 Dynamic으로 변경
                }
                rigidbody.velocity=Vector2.up*velocity;
            }
        }

        void FixedUpdate()
        {
            transform.rotation=Quaternion.Euler(0,0,rigidbody.velocity.y*speed);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            UIManager.Instance.GameOver();
        }
    }

}
