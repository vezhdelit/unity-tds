using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
     [SerializeField] private int maxHealth;
     
     private int health;
     private float timeBtwAttack;
    [SerializeField]private float startTimeBtwAttack;
    [SerializeField]private int attackDamage;
    [SerializeField]private float attackRange;
    [SerializeField]private Transform attackPos;
    [SerializeField]private LayerMask playerLayer;

    private SquadManager sm;
    private GameManager gm;
    private GameObject target;
    private NavMeshAgent agent;
    private Healthbar hb;
    

    void Start()
    {
        health = maxHealth;
        sm = FindObjectOfType<SquadManager>();
        gm = FindObjectOfType<GameManager>();
        hb = GetComponentInChildren<Healthbar>();
        hb.SetMaxHealth(health);
        hb.SetHealth(health);
        
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
    }

    void FixedUpdate()
    {
        OnEnemyAttack();
        EnemyMovement();
        OnEnemyDeath();
    }
    
    private void OnEnable()
    {
        health = maxHealth;
        if (hb != null)
        {
            hb.SetHealth(health);
        }
    }
    
    void EnemyMovement()
    {
        Vector3 difference = new Vector3(1000, 1000, 1000);
        foreach (var member in sm.squad)
        {
            if ((member.transform.position - transform.position).magnitude <= difference.magnitude)
            {
                difference = member.transform.position - transform.position;
                target = member;
            }
        }
        agent.SetDestination(target.transform.position);
        
        difference = target.transform.position - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }

    void OnEnemyAttack()
    {
        if(timeBtwAttack <= 0)
        {
            Collider2D[] players = Physics2D.OverlapCircleAll(attackPos.position, attackRange, playerLayer);
            
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].CompareTag("Player"))
                {
                    players[i].GetComponent<PlayerConroller>().TakeDamage(attackDamage);
                }

                if (players[i].CompareTag("SquadMember"))
                {
                    players[i].GetComponent<SquadMemberController>().TakeDamage(attackDamage);
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
    
    void OnEnemyDeath(){
        if(health<=0)
        {
            gm.score++;
            this.gameObject.SetActive(false);
        }    
    }
    public  void TakeDamage(int damage){
        health -= damage;
        hb.SetHealth(health);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
