using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private float velocity = 5f;
    private float speed=4f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            rigidbody.velocity=Vector2.up*velocity;
        }
    }

    void FixedUpdate() {
        transform.rotation=Quaternion.Euler(0,0,rigidbody.velocity.y*speed);
    }
}
