using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
  public Vector3 velocity;
  public float slowdownCoefficient = 0.3f;
  private Rigidbody2D rb;

  void Awake()
  {
    rb = gameObject.GetComponent<Rigidbody2D>();
  }

  public void Throw(Vector2 force)
  {
    rb.AddForce(force, ForceMode2D.Impulse);
  }
}
