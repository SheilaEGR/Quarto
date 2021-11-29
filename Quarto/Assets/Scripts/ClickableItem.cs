using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ClickableItem : MonoBehaviour
{
    private bool selected = false;
    private bool enable = true;


    public void Deselect()
    {
        selected = false;
    }

    public bool Selected()
    {
        return selected;
    }

    public void Enable()
    {
        enable = true;
    }

    public void Disable()
    {
        enable = false;
    }

    private void OnMouseDown()
    {
        if (!enable) return;
        selected = true;
    }
}
