using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector2 initialPosition;
    private Vector2 newPosition;
    private Ray ray;
    private RaycastHit hit;

    private void Start()
    {
        initialPosition = transform.localPosition;
        newPosition = new Vector2(initialPosition.x, initialPosition.y + 10.0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localPosition = newPosition;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localPosition = initialPosition;
    }
}
