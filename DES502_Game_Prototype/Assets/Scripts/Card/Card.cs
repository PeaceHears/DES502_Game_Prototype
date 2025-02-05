using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
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

    private void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            transform.localPosition = newPosition;
        }
        else
        {
            transform.localPosition = initialPosition;
        }
    }
}
