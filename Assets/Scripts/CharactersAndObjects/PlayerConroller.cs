using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConroller : MonoBehaviour
{
    [SerializeField] private  int maxHealth;
    private  int health;
    [SerializeField] private float speed;
    
    [SerializeField] private Transform shotPoint;
    [SerializeField] private float startTimeBtwShots;
    private float timeBtwShots;
    
    private Rigidbody2D rb;
    private BulletPool bp;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Healthbar hb;
    private SquadManager sm;


    void Start()
    {
        bp = FindObjectOfType<BulletPool>();
        rb = GetComponent<Rigidbody2D>();
        sm = FindObjectOfType<SquadManager>();
        hb = GetComponentInChildren<Healthbar>();
        hb.SetMaxHealth(health);
        hb.SetHealth(health);

        health = maxHealth;
    }

    void FixedUpdate()
    {
        OnPlayerDeath();
        RotateSprite();
        Shooting();
        Movement();
    }

    void Movement()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
    void Shooting()
    {
        if(timeBtwShots <= 0){
            if(Input.GetMouseButton(0)){
                bp.CreateBullet(shotPoint, transform);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else{
            timeBtwShots -= Time.deltaTime;
        }
    }
    void OnPlayerDeath(){
        if(health<=0){
            sm.RemoveMember();
            health = maxHealth;
        }    
    }
    void RotateSprite()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        hb.SetHealth(health);
    }
    
    
}
