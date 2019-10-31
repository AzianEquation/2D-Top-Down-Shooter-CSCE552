using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownEnemy : MonoBehaviour
{
    public enum States { Default, Follow }
    public List<Transform> waypoints;
    public States state;
    public int currWayPoint = 0;
    public Transform player; 
    private float speed;
    private EnemyStats enStats;
    private Rigidbody2D rigid;
    public float threshold = 0.05f;
    private CharacterStats plstats;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        enStats = GetComponent<EnemyStats>();
        speed = enStats.speed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveTo = new Vector2();
        switch (state)
        {
            case States.Default:
                // transform object, has position, rotation, and scale
                moveTo = waypoints[currWayPoint].position;

                break;
            case States.Follow:
                moveTo = player.position;
               break;
        }
        // normal vector pointing to the place We want to move
        Vector2 dir = (moveTo - transform.position).normalized;
        rigid.velocity = dir * speed;

       // float angle1 = Vector2.SignedAngle(Vector2.up, transform.up); //angle to current
      //  float angle2 = Vector2.SignedAngle(Vector2.up, waypoints[currWayPoint].position); //angle to new
      //  float newAngle = Mathf.MoveTowardsAngle(angle1, angle2, plstats.rotationFactor * Time.deltaTime);
        

        if ((waypoints[currWayPoint].position - transform.position).magnitude < threshold)
        {
            currWayPoint++;
            transform.rotation = Quaternion.Euler(waypoints[currWayPoint].position);
            if (currWayPoint >= waypoints.Count)
            {
                currWayPoint = 0;
            }
        }



        void OnTriggerEnter2D(Collider2D coll)
        {
            // if trigger enter -> something
            if (coll.gameObject.name.Equals("bluecircle"))
            {
                state = States.Follow;
            }
        }

        void OnTriggerExit2D(Collider2D coll)
        {
            if(coll.gameObject.name.Equals("bluecircle"))
            {
                state = States.Default;
            }
        }




    }
}
