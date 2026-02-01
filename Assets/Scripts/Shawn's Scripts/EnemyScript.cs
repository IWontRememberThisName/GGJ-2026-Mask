using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform player;
    public float Range = 8f;
    public float Speed = 8f;
    public float jumpForce = 8f;
    public int Health = 1;
    public PlayerController playerController;

    public LayerMask obstacleMask;
    public LayerMask playerMask;

    private Rigidbody2D RB;
    

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        //Find our player and where they are, basicly setting up the update function.
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        // Set layer masks (if not assigned in Inspector)
        obstacleMask = LayerMask.GetMask("Obstacle");
        playerMask = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate to face the player
        Vector3 lookDir = (player.position - transform.position).normalized;
        lookDir.y = 0; //locks the position so it doesnt move/bug out if the player runs intot he enemy 
        transform.forward = lookDir;
        if (player == null) return;

        Vector3 dir = player.position - transform.position;
        float Distance = dir.magnitude;
        // 
        if (Distance > Range)
        {
            RB.velocity = Vector3.zero;
            return;
        }

        //combine the layers so it only chcks for a player or obsticals at the same time
        int CombinedLayers = obstacleMask | playerMask;
        RaycastHit hit;
        //Raycast for the trace back onto the player
        bool hasHit = Physics.Raycast(transform.position, dir.normalized, out hit, Distance, CombinedLayers);

        Debug.DrawRay(transform.position, dir.normalized * Distance, Color.cyan);
        //when the player is visible and no raycast has hit a layer that has the Obstical tag
        if (hasHit && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            RB.velocity = dir.normalized * Speed;
        }
        else
        {
            // Wall in the way or no line of sight
            RB.velocity = Vector3.zero;
        }
    }
}
