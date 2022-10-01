using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInventory : MonoBehaviour
{
  public GameObject heldItem;
  public GameObject heldItemBubble;
  public InputAction inventoryAction;
  public float pickupRadius = 0.5f;
  public float throwSpeed = 1f;

  private CharacterMovement characterMovement;

  private int oldSortingOrder;

  void Awake()
  {
    inventoryAction.performed += _ => PerformAction();
    characterMovement = gameObject.GetComponent<CharacterMovement>();
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
        oldSortingOrder = heldItem.GetComponent<SpriteRenderer>().sortingOrder;
        heldItem.GetComponent<SpriteRenderer>().sortingOrder = heldItemBubble.GetComponent<SpriteRenderer>().sortingOrder + 1;
        heldItem.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        heldItem.GetComponent<BoxCollider2D>().enabled = false;
        heldItem.transform.localPosition = new Vector2(0, 0);
      }
    }
  }

  private void ThrowItem()
  {
    heldItem.transform.SetParent(null);
    heldItem.GetComponent<SpriteRenderer>().sortingOrder = oldSortingOrder;
    heldItem.GetComponent<BoxCollider2D>().enabled = true;
    heldItem.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    Vector2 velocity = Vector2.zero;
    switch (characterMovement.direction)
    {
      case Direction.UP:
        velocity = new Vector2(0, throwSpeed);
        break;
      case Direction.DOWN:
        velocity = new Vector2(0, -throwSpeed);
        break;
      case Direction.LEFT:
        velocity = new Vector2(-throwSpeed, 0);
        break;
      case Direction.RIGHT:
        velocity = new Vector2(throwSpeed, 0);
        break;
    }
    heldItem.GetComponent<Item>().Throw(velocity);
    heldItem = null;
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
