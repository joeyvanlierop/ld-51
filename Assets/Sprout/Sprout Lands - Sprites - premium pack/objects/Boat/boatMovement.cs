using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 10;
    public Animator animator;
    bool stopBoat = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }


    void Update() {
        if (rb.velocity.x == 0) {
            animator.SetBool("isCruisin", false);
        } else {
            animator.SetBool("isCruisin", true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if (stopBoat) {
            rb.velocity = Vector2.Lerp(new Vector2(-moveSpeed, 0), new Vector2(0, 0), 0.6f);
            return;
        }
        rb.velocity = new Vector2(-moveSpeed, 0);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Boat Trigger")) {
            stopBoat = true;
        }
    }
}
