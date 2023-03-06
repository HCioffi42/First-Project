using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float treeHealth; // The health of the tree.
    [SerializeField] private Animator anim; // The animator component of the tree.

    [SerializeField] private GameObject woodPrefab; // The prefab for the wood that will be spawned.
    [SerializeField] private int totalWood; // The total amount of wood that will be spawned.
    [SerializeField] private ParticleSystem leafs; // The particle system for the leaves.

    private bool isCut; // A bool variable that keeps track of whether the tree has been cut or not.

    public void OnHit()
    {
        treeHealth--; // Decrease the health of the tree.

        anim.SetTrigger("Hit"); // Trigger the "Hit" animation on the animator component.
        leafs.Play(); // Play the particle system for the leaves.

        if(treeHealth <= 0) // If the health of the tree is less than or equal to 0, the tree is cut down.
        {
            // Spawn wood based on the total amount of wood specified, with randomized positions around the tree.
            for (int i = 0; i < totalWood; i++)
            {
                Instantiate(woodPrefab, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f,1f), 0f), transform.rotation);
            }
            anim.SetTrigger ("Cut"); // Trigger the "Cut" animation on the animator component.

            isCut = true; // Set the bool variable to true.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Axe") && !isCut) // If the collider has been triggered by an axe and the tree has not been cut, trigger the OnHit method.
        {
            OnHit();
        }
    }
}
