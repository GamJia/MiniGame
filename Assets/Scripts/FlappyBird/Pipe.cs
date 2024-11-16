using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlappyBird;

namespace FlappyBird
{
    public class Pipe : MonoBehaviour
    {
        private Collider2D collider;
        private float speed=1.5f;

        void Start() {
            collider = GetComponent<Collider2D>();
        }

        void Update()
        {
            transform.position+=Vector3.left*speed*Time.deltaTime;
            // 현재 객체의 위치를 왼쪽으로 speed 속도로 이동 시킵니다
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            UIManager.Instance.UpdateScore();
            collider.enabled=false;
        }
    }
}

