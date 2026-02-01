using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 direction;
    private Rigidbody2D rb;

    bool maskEquipped = false;

    public Renderer rend;

    public Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        MaskOn();
        MaskOff();
    }

    /// <summary>
    /// Handles the movement of the player with WASD controls
    /// </summary>
    private void Movement()
    {
        Vector3 pos = transform.position;
        //WASD controls
        if (Input.GetKey(KeyCode.W))
        {
            pos.y += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos.y -= speed * Time.deltaTime;
        }
        transform.position = pos;
    }

    /// <summary>
    /// Controls the activation of the mask
    /// </summary>
    public void MaskOn()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!maskEquipped)
            {
                maskEquipped = true;
                rend.material.color = Color.red;
                print("test");
            }
        }
    }
    /// <summary>
    /// Controls the deactivation of the mask
    /// </summary>
    public void MaskOff()
    {

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (maskEquipped)
            {
                maskEquipped = false;
                rend.material.color = originalColor;
            }
        }
    }

 }
