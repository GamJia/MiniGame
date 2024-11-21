using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColorFill;

namespace ColorFill
{
    public class ColorChange : MonoBehaviour
    {
        [SerializeField] private Color targetColor; // 변경할 색상

        private Collider2D collider;

        void Awake()
        {
            collider = GetComponent<Collider2D>(); 
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    // 색상 변경
                    spriteRenderer.color = targetColor;
                }

                collider.enabled = false;

                ColorFillManager.Instance.UpdateCount();
            }
        }
    }
}

