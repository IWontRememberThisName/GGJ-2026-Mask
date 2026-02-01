using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 direction;
    private Rigidbody2D rb;

    bool maskEquipped = false;

    public Renderer rend;

    public Color originalColor;

    public Image maskGauge;

    public float gauge;
    public float maxGauge;
    public float maskCost;

    public float rechargeRate;

    public Coroutine recharge;


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
        DecreaseGauge();
        IncreaseGauge();
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
            if (!maskEquipped && gauge != 0)
            {
                maskEquipped = true;
                rend.material.color = Color.red;
                //print("test");
            }
            
        }
    }
    /// <summary>
    /// Controls the deactivation of the mask
    /// </summary>
    public void MaskOff()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift) || gauge == 0)
        {
            if (maskEquipped)
            {
                maskEquipped = false;
                rend.material.color = originalColor;
            }
        }
    }

    private void DecreaseGauge()
    {
        if (maskEquipped)
        {
            gauge -= maskCost * Time.deltaTime;
            if (gauge < 0) gauge = 0;
            maskGauge.fillAmount = gauge / maxGauge;
        }

    }
    private void IncreaseGauge()
    {
        if (!maskEquipped)
        {
            if (recharge != null) StopCoroutine(recharge);
            recharge = StartCoroutine(RechargeMask());
            
        }
    }

    public IEnumerator RechargeMask()
    {
        yield return new WaitForSeconds(1f);
        while (gauge < maxGauge)
        {
            gauge += rechargeRate / 10f;
            print("test");
            if(gauge > maxGauge) gauge = maxGauge;
            maskGauge.fillAmount = gauge / maxGauge;
            yield return new WaitForSeconds(.1f);
        }
    }

 }
