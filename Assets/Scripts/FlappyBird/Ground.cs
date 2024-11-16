using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlappyBird;

namespace FlappyBird
{
    public class Ground : MonoBehaviour
    {
        private float speed=1.5f;

        void Update()
        {
            transform.position+=Vector3.left*speed*Time.deltaTime;
            // 현재 객체의 위치를 왼쪽으로 speed 속도로 이동 시킵니다
        }
    }

}
