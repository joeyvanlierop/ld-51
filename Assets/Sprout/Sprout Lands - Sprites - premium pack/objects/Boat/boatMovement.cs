using UnityEngine;

public class boatMovement : MonoBehaviour
{
  Rigidbody2D rb;
  public float moveSpeed = 5;
  public Animator animator;
  bool stopBoat = false;
  bool boatStopped = false;
  bool startBoat = false;
  public float slowDownTime = 1f;
  float timeElapsed = 0;
  string boatStopName;



  // Start is called before the first frame update
  void Start()
  {
    rb = gameObject.GetComponent<Rigidbody2D>();
    animator = gameObject.GetComponent<Animator>();
    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10;
    if (Random.value < .5)
      boatStopName = "BoatTriggerClose";
    else
      boatStopName = "BoatTriggerFar";
  }


  void Update()
  {
    if (rb.velocity.x == 0)
    {
      animator.SetBool("isCruisin", false);
    }
    else
    {
      animator.SetBool("isCruisin", true);
    }
  }

  public void ResetTimer()
  {
    if (boatStopped && timeElapsed < 10)
      timeElapsed = 0;
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (stopBoat)
    {
      if (timeElapsed < slowDownTime)
      {
        rb.velocity = Vector2.Lerp(new Vector2(-moveSpeed, 0), new Vector2(0, 0), timeElapsed / slowDownTime);
        timeElapsed += Time.deltaTime;
      }
      else
      {
        boatStopped = true;
        stopBoat = false;
        timeElapsed = 0;
      }
      return;
    }

    if (boatStopped)
    {
      if (timeElapsed < 10)
      {
        timeElapsed += Time.deltaTime;
      }
      else
      {
        timeElapsed = 0;
        boatStopped = false;
        startBoat = true;
      }
      return;
    }

    if (startBoat)
    {
      if (timeElapsed < slowDownTime)
      {
        rb.velocity = Vector2.Lerp(new Vector2(0, 0), new Vector2(-moveSpeed, 0), timeElapsed / slowDownTime);
        timeElapsed += Time.deltaTime;
      }
      else
      {
        boatStopped = false;
        stopBoat = false;
        startBoat = false;
        timeElapsed = 0;
      }
      return;
    }
    rb.velocity = new Vector2(-moveSpeed, 0);
  }

  void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.CompareTag("Boat Trigger") && collider.name == boatStopName)
    {
      stopBoat = true;
    }
  }
}
