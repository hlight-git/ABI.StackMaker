using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject openingChest;
    [SerializeField] private GameObject closingChest;

    private void OnTriggerEnter(Collider other)
    {
        Invoke(nameof(OpenChest), 1f);
        UIManager.Instance.ActiveWonPanel();
    }

    void OpenChest()
    {
        closingChest.SetActive(false);
        openingChest.SetActive(true);
    }
}
