using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveTheDog
{
    [AddComponentMenu("Save The Dog/Save The Dog Player")]
    public class Player : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other) {
            // 충돌한 객체에 Bee 컴포넌트가 있는지 확인
            Bee beeComponent = other.gameObject.GetComponent<Bee>();
            if (beeComponent != null)
            {
                LineManager.Instance.StopTimer();
            }
        }
    }
}
