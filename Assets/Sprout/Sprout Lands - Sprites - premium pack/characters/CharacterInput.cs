using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{

  public Animator animator;
  public float velocityX;
  public float velocityY;
  public float moveX;
  public float moveY;
  public float forceX;
  public float forceY;
  public InputAction movementAction;

  private Rigidbody2D rb;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    this.moveX = Input.GetAxisRaw("Horizontal");
    this.moveY = Input.GetAxisRaw("Vertical");

    animator.SetBool("movingLeft", moveX < 0);
    animator.SetBool("movingRight", moveX > 0);
    animator.SetBool("movingUp", moveY > 0);
    animator.SetBool("movingDown", moveY < 0);
  }

  void FixedUpdate()
  {

    if (this.forceX != 0) rb.AddForce(new Vector2(this.forceX, 0) * Time.deltaTime);
    if (this.forceY != 0) rb.AddForce(new Vector2(this.forceY, 0) * Time.deltaTime);
  }

  private void OnEnable()
  {
    movementAction.Enable();
  }

  private void Disable()
  {
    movementAction.Disable();
  }
}
