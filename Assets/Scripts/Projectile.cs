using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Attack
    // inherit from attack instead of MonoBehavior
{
    public float vel;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(vel * direction.x, vel * direction.y);
        transform.up = direction;
    }
    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        Destroy(this.gameObject);        
    }
}
