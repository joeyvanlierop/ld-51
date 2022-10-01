using UnityEngine;

public class Item : MonoBehaviour
{
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
