using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickTaker : MonoBehaviour
{
    [SerializeField] private GameObject placedBrick;
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<BoxCollider>().enabled = false;
        placedBrick.SetActive(true);
    }
}
