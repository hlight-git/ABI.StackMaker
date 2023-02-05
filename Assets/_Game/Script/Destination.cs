using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    [SerializeField] private GameObject leftFirework;
    //[SerializeField] private GameObject rightFirework;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            leftFirework.SetActive(true);
            //rightFirework.SetActive(true);
        }
    }
}
