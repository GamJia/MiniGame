using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuikaGame;

namespace SuikaGame 
{
    public class Fruit : MonoBehaviour
    {
        public bool isMerged=false;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // 충돌한 객체에서 Fruit 컴포넌트를 가져옵니다.
            Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();

            if (otherFruit != null) // Fruit 컴포넌트가 있는 경우
            {
                if(collision.gameObject.name==this.gameObject.name)
                {
                    if(isMerged||otherFruit.isMerged)
                    {
                        return;
                    }

                    isMerged=true;

                    GameManager.Instance.DestroyFruit(this.gameObject,collision.gameObject);
                }
                
            }
        }

        public void Destroy()
        {
            Destroy(this.gameObject); 
        }

    }
}
