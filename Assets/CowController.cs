using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowController : MonoBehaviour
{

  Rigidbody2D rb;
  SpriteRenderer sr;
  Animator anim;
  Vector2 moveDirection = Vector2.zero;
  float moveSpeed = 1;

  void Start()
  {
    rb = gameObject.GetComponent<Rigidbody2D>();
    sr = gameObject.GetComponent<SpriteRenderer>();
    anim = gameObject.GetComponent<Animator>();
    StartCoroutine(Walk());
  }

  void Update()
  {
    if (moveDirection.x < 0)
      sr.flipX = true;
    else if (moveDirection.x > 0)
      sr.flipX = false;
    anim.SetBool("moving", moveDirection != Vector2.zero);
  }

  void FixedUpdate()
  {
    rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
  }

  IEnumerator Walk()
  {
    var walkDirection = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
    var walkTime = Random.Range(0, 3);
    moveDirection = walkDirection;
    yield return new WaitForSeconds(walkTime);
    moveDirection = Vector2.zero;
    var waitTime = Random.Range(2, 4);
    yield return new WaitForSeconds(waitTime);
    StartCoroutine(Walk());
  }
}
