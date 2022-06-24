using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private  float speed;
    [SerializeField] private  float lifetime;
    [SerializeField] private  float distance;
    [SerializeField] private  int damage;
    [SerializeField] private  LayerMask hardSurface;

    void OnEnable()
    {
        this.StartCoroutine("LifeRoutine");
    }
    void OnDisable()
    {
        this.StopCoroutine("LifeRoutine");
    }
    IEnumerator LifeRoutine()
    {
        yield return new WaitForSecondsRealtime(lifetime);
        DeactivateBullet();
    }

    void FixedUpdate(){
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, hardSurface);
        if(hitInfo.collider != null)
        {
            if(hitInfo.collider.CompareTag("Enemy")){
                hitInfo.collider.GetComponent<EnemyController>().TakeDamage(damage);
            }
            DeactivateBullet();
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    public void DeactivateBullet()
    {
        this.gameObject.SetActive(false);
    }
}
