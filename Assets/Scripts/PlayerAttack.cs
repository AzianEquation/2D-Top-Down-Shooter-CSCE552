using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // allows to attach a prefab to playerAttack
    public GameObject projectile;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // makes a new fireball instance(prefab)
           // GameObject attackHolder = Instantiate(fireball, transform.position, new Quaternion());
            // sets its direction equal to Attacks direction 
           // attackHolder.GetComponent<Attack>().direction = transform.up;
            GameObject attackHolder = Instantiate(projectile, transform.position, new Quaternion());
            attackHolder.GetComponent<Attack>().direction = transform.up;
            //attackHolder.GetComponent<Attack>().direction = transform.right + new Vector3(1,1,0);
        }
    }
}
