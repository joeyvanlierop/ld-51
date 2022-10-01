using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInventory : MonoBehaviour
{
  public GameObject heldItem;
  public GameObject heldItemBubble;
  public InputAction inventoryAction;
  public float pickupRadius = 0.5f;

  void Awake()
  {
    inventoryAction.performed += _ => PerformAction();
  }

  void PerformAction()
  {
    if (!heldItem)
      PickupItem();
    else
      ThrowItem();
  }


  private void PickupItem()
  {
    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickupRadius);
    foreach (Collider2D collider in colliders)
    {
      if (collider.tag == "Item")
      {
        heldItem = collider.gameObject;
        heldItem.transform.SetParent(heldItemBubble.transform);
        heldItem.transform.localPosition = new Vector2(0, 0);
      }
    }
  }

  private void ThrowItem()
  {
    // heldItem.
  }

  void Update()
  {
    heldItemBubble.GetComponent<SpriteRenderer>().enabled = heldItem == true;
  }

  private void OnEnable()
  {
    inventoryAction.Enable();
  }

  private void OnDisable()
  {
    inventoryAction.Disable();
  }
}
