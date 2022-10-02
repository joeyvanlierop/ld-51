using System;
using UnityEngine;

public class Item : MonoBehaviour
{
  private Rigidbody2D rb;
  public Boolean consumable = false;

  void Start()
  {
    rb = gameObject.GetComponent<Rigidbody2D>();
  }

  public void Throw(Vector2 force)
  {
    rb.AddForce(force, ForceMode2D.Impulse);
  }

  public virtual void Consume(Vector3Int target)
  {
    throw new NotImplementedException();
  }
}
