using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkShooter : MonoBehaviour
{
    [SerializeField] private GameObject firework;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            firework.SetActive(true);
        }
    }
}
