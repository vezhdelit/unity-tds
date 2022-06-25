using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SquadMemberController : MonoBehaviour
{
    [SerializeField] private  int health;
    [SerializeField] private  float speed;
    [SerializeField] private Transform shotPoint;
    public float startTimeBtwShots;
    private float timeBtwShots;
    public int id;

    private AudioSource shootSound;
    private SquadManager sm;
    private GameObject target;
    private BulletPool bp;
    private Healthbar hb;

    void Start()
    {
        shootSound = GetComponentInChildren<AudioSource>();
        bp = FindObjectOfType<BulletPool>();
        sm = FindObjectOfType<SquadManager>();
        hb = GetComponentInChildren<Healthbar>();
        hb.SetMaxHealth(health);
        hb.SetHealth(health);
        
        target = sm.squad[id-1];

        startTimeBtwShots = Random.Range(startTimeBtwShots - 0.1f, startTimeBtwShots + 0.2f);
    }
    void FixedUpdate()
    {
        target = sm.squad[id-1];
        AlternativeMovement();
        OnSquadMemberDeath();
        RotateSprite();
        Shooting();
    }

    void AlternativeMovement()
    {
        if (Vector2.Distance(transform.position, target.transform.position) > 2)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }
    void Shooting()
    {
                if(timeBtwShots <= 0){
                    if(Input.GetMouseButton(0))
                    {
                        shootSound.pitch = Random.Range(0.9f, 1.1f);
                        shootSound.Play();
                        bp.CreateBullet(shotPoint, transform);
                        timeBtwShots = startTimeBtwShots;
                    }
                }
                else{
                    timeBtwShots -= Time.deltaTime;
                }
    }
    void OnSquadMemberDeath(){
        if(health<=0){
            sm.RemoveMemberAt(id);
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
