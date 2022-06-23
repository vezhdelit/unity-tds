using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSquadMember : MonoBehaviour
{
    private SquadManager sm;

    void Start()
    {
        sm = FindObjectOfType<SquadManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("SquadMember"))
        {
            sm.AddMember();
            this.gameObject.SetActive(false);
        }
    }
}
