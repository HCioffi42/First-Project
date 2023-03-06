using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float speed; // The speed of the NPC's movement
    private float initialSpeed; // The initial speed of the NPC

    private int index; // The current index of the NPC's path
    private Animator anim; // The animator component of the NPC

    public List<Transform> paths = new List<Transform>(); // A list of Transform objects representing the NPC's path

    private void Start()
    {
        initialSpeed = speed; // Set the initial speed of the NPC to the current speed
        anim = GetComponent<Animator>(); // Get the animator component of the NPC
    }

    void Update()
    {
        // If a dialogue is being shown, stop the NPC's movement and set its "isWalking" animator parameter to false
        if(DialogueControl.instance.isShowing)
        {
            speed = 0f;
            anim.SetBool("isWalking", false);
        }
        // Otherwise, set the NPC's speed to its initial speed and set its "isWalking" animator parameter to true
        else
        {
            speed = initialSpeed;
            anim.SetBool("isWalking", true);
        }

        // Move the NPC towards the current point on its path
        transform.position = Vector2.MoveTowards(transform.position, paths[index].position, speed * Time.deltaTime);

        // If the NPC has reached the current point on its path, move on to the next point
        if(Vector2.Distance(transform.position, paths[index].position) < 0.1f)
        {
            if(index < paths.Count - 1)
            {
                index++;
                //index = Random.Range(0, paths.Count - 1); // (OPTIONAL) Randomize the NPC's path
            }
            else
            {
                index = 0;
            }
        }

        // Rotate the NPC towards the direction of its next point on the path
        Vector2 direction = paths[index].position - transform.position;

        if(direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0,0);
        }

        if(direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0,180);
        }
    }
}
