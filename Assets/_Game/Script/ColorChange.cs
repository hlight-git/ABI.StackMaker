using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField] private Renderer animRenderer;
    private static readonly Color[] colors =
    {
        Color.clear,
        Color.white,
        Color.red,
        Color.yellow,
        Color.green,
        Color.cyan,
        Color.blue,
        Color.magenta,
        Color.white,
        Color.gray,
        Color.black
    };
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int icolor = other.GetComponent<Player>().StackCount;
            animRenderer.material.color = colors[icolor];
        }
    }
}
