using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletPool : MonoBehaviour
{
    [SerializeField] private int poolCount = 5;
    [SerializeField] private bool autoExpand = false;
    [SerializeField] private BulletBehaviour bulletPrefab;
    
    private Pool<BulletBehaviour> pool;
    private void Start()
    {
        pool = new Pool<BulletBehaviour>(this.bulletPrefab, this.poolCount, this.transform);
        this.pool.autoExpand = this.autoExpand;
    }

    public void CreateBullet(Transform shotpoint, Transform player)
    {
        var bullet = this.pool.GetFreeElement();
        if (bullet != null)
        {
            bullet.transform.position = shotpoint.position;
            bullet.transform.rotation = player.rotation;
        }
    }
}
