using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField] private Renderer animRenderer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTag.PLAYER))
        {
            int icolor = other.GetComponent<Player>().PlayerStack.CollectedBrickCount % GameConstant.Bridge.WINZONE_COLORS.Length;
            animRenderer.material.color = GameConstant.Bridge.WINZONE_COLORS[icolor];
        }
    }
}
