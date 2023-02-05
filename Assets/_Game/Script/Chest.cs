using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private MeshRenderer openingChest;
    [SerializeField] private MeshRenderer closingChest;

    private void OnTriggerEnter(Collider other)
    {
        //Invoke(nameof(OpenChest), 1f);
        OpenChest();
        UIManager.instance.ActiveWonPanel();
    }

    void OpenChest()
    {
        closingChest.enabled = false;
        openingChest.enabled = true;
    }
}
