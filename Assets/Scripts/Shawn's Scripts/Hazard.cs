using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public EnemyScript enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            enemy.CheckPlayerMask();
        }
    }
}
