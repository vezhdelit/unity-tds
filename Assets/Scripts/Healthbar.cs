using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float maxHealth;

    public void SetHealth(float health)
    {
        slider.gameObject.SetActive(health<maxHealth);
        slider.value = health;
        slider.maxValue = maxHealth;
    }
    public void SetMaxHealth(float health)
    {
        maxHealth = health;
    }
    void FixedUpdate()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint( transform.parent.position + offset);
    }
}
