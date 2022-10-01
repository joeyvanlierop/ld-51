using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{

  public Animator animator;
  public Vector2 velocity;
  public float moveX;
  public float moveY;
  public float moveSpeedX = 5f;
  public float moveSpeedY = 5f;
  public InputAction movementAction;

  Rigidbody2D rb;
  Vector2 moveDirection = Vector2.zero;

  void Start()
  {
    rb = gameObject.GetComponent<Rigidbody2D>();
  }

  //   void Awake() {
  //     movementAction.performed += ctx => {
  //         Move(ctx.ReadValue<Vector2>());
  //         Animate(ctx.ReadValue<Vector2>());
  //     };
  //   }

  //   void Move(Vector2 velocity) {
  //     Debug.Log(velocity);
  //     rb.velocity = new Vector2(velocity.x * moveSpeedX, velocity.y * moveSpeedY);
  //   }

  //   void Animate(Vector2 moveDirection) {
  //     animator.SetBool("movingLeft", moveDirection.x < 0);
  //     animator.SetBool("movingRight", moveDirection.x > 0);
  //     animator.SetBool("movingUp", moveDirection.y > 0);
  //     animator.SetBool("movingDown", moveDirection.y < 0);
  //   }

  //Update is called once per frame
  void Update()
  {

    moveDirection = movementAction.ReadValue<Vector2>();

    animator.SetBool("movingLeft", moveDirection.x < 0);
    animator.SetBool("movingRight", moveDirection.x > 0);
    animator.SetBool("movingUp", moveDirection.y > 0);
    animator.SetBool("movingDown", moveDirection.y < 0);
  }

  private void FixedUpdate()
  {
    Debug.Log(moveDirection);

    rb.velocity = new Vector2(moveDirection.x * moveSpeedX, moveDirection.y * moveSpeedY);
  }

  private void OnEnable()
  {
    movementAction.Enable();
  }

  private void OnDisable()
  {
    movementAction.Disable();
  }
}
