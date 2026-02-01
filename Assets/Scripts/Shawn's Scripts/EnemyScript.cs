using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public PlayerController player;
    public GameObject playerSprite;
    public bool chasePlayer = false;
    public float speed = 10f;

    public float distanceForChase = 4f;

    public NavMeshAgent nav;

    public Transform target;

    public Vector2 startPosition;
    private void Start()
    {
        playerSprite = GameObject.Find("Player");
        Debug.Log("PlayerFound");
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerSprite.transform.position);
        float distanceChasedCombat = Vector2.Distance(transform.position, startPosition);

        if (distanceToPlayer <= distanceForChase)
        {
            CheckPlayerMask();
            if (chasePlayer)
            {
                target = playerSprite.transform;
                nav.SetDestination(target.transform.position);
                nav.speed = speed;
            }
        }
        else
        {
            chasePlayer = false;
        }
    }

    public void CheckPlayerMask()
    {
        if (player.maskEquipped)
        {
            chasePlayer = false;
        }
        else
        {
            chasePlayer = true;
        }
    }

    public void ChasePlayer()
    {
        CheckPlayerMask();
        if (chasePlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerSprite.transform.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = transform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            player.Respawn();
        }
    }
}
