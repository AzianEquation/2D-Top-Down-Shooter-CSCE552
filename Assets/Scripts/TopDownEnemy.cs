using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// UI Text
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TopDownEnemy : MonoBehaviour
{
    public enum States { Default, Follow }
    public List<Transform> waypoints;
    public States state;
    public int currWayPoint = 0;
    public Transform player;
    public GameObject ghoul;
    public GameObject skeleton;
    private float speed;
    private float health;
    private EnemyStats enStats;
    private Rigidbody2D rigid;
    public float threshold = 0.05f;
    private Quaternion lookRotation;
    private CharacterStats plstats;
    // text
    public Text levelText;
    private int level;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        enStats = GetComponent<EnemyStats>();
        speed = enStats.speed;
        health = enStats.health;
        level = 1;
        Debug.Log("Initial Health" + health);
        if (this.gameObject.name.Equals("Enemy"))
            levelText.text = "Level 1: The Half-Eaten Zombie";
        if (this.gameObject.name.Equals("ghoul"))
            levelText.text = "Level 2: The Hungry Ghoul";
        if (this.gameObject.name.Equals("skeleton"))
            levelText.text = "Level 3: The Handsy Skeleton";
    }

    // Update is called once per frame
    void FixedUpdate()
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
        // check for health of object
        if (health <= 0)
        {
            // spawned enemy is killed so deactivate
            this.gameObject.SetActive(false);
            levelText.text = "DEFEATED!";
            // spawn next enemy
            Invoke("spawn", 3);
            //SetLevelText();
        }

        if ((waypoints[currWayPoint].position - transform.position).magnitude < threshold)
        {
            currWayPoint++;
            if (currWayPoint >= waypoints.Count)
            {
                currWayPoint = 0;
            }
        }
    } // new void update
    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Collider Triggered by" + coll.gameObject.name);
        // if character archer gets within range
        if (coll.gameObject.name.Equals("characterArcher"))
        {
            state = States.Follow;
        }
        // if shot by projectile
        if (coll.gameObject.name.Equals("arrowRed(Clone)"))
        {
            Debug.Log("arrowRed Triggered Health" + health);
            // decrement health
            Projectile temp = coll.gameObject.GetComponent<Projectile>();
            health = health - temp.damage;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
        {
            if(coll.gameObject.name.Equals("characterArcher"))
            {
                state = States.Default;
            }
        }
    void spawn()
    {
        if (this.gameObject.name.Equals("Enemy"))
        {
            //Instantiate(ghoul, new Vector3(7.5f,5.5f,0f), Quaternion.identity);
            //this.gameObject = ghoul;
            ghoul.SetActive(true);
        }
        else if (this.gameObject.name.Equals("ghoul"))
        {
            skeleton.SetActive(true);
        }
        // last enemy killed
        else
        {
            // defeated last boss
            levelText.text = "Game Over \n Resetting in 5s";
            Invoke("restart",5);
        }
            
    }
    void restart()
    {
        SceneManager.LoadScene("2dTopDownScene");
    }
}
