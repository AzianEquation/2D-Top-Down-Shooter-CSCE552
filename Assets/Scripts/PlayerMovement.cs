using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private float maxVel;
    private float accel;
    float xVel;
    float yVel;
    private Rigidbody2D rigid;
    private CharacterStats plstats;
    void Start()
    {
        // get the character stats object
        // GetComponent looks at player and sees is this is attacthed
        plstats = GetComponent<CharacterStats>();
        rigid = GetComponent<Rigidbody2D>();
        maxVel = plstats.maxVel;
        accel = plstats.accel;
    }

    // Update is called once per frame
    // update vs fixed update
    // update runs every frame
    // fixed update runs every physics frame. (60fps)
    void FixedUpdate()
    {
        xVel = Input.GetAxis("Horizontal");
        yVel = Input.GetAxis("Vertical");
        rigid.velocity = new Vector2(xVel * maxVel, yVel * maxVel);
        Vector2 newUp = new Vector2(xVel, yVel).normalized;
        // when would newUp be zero.. when not pressing buttons 
        if (newUp != Vector2.zero)
        {
            // angle gives unsigned [0,180] signed gives you which side (negative too)
            // what is the angle between vector2.up and transform.up
            float angle1 = Vector2.SignedAngle(Vector2.up, transform.up); //angle to current
            float angle2 = Vector2.SignedAngle(Vector2.up, newUp); //angle to new
            // take the two above angles. make a rotation factor toward where we want to go
            float newAngle = Mathf.MoveTowardsAngle(angle1, angle2, plstats.rotationFactor * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, newAngle);



        }
    }
}
