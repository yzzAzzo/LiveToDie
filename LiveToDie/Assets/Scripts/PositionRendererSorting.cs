using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRendererSorting : MonoBehaviour
{
    [SerializeField]
    private int sortingOrderBase = 5000;

    [SerializeField]
    private int offset = 0;

    private Renderer myRenderer;

    private void Awake()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
    }

    private void lateUpdate()
    {
        myRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y - offset);
    }
}
