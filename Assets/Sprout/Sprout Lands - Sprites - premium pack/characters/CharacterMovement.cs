using UnityEngine;
using UnityEngine.InputSystem;

public enum Direction
{
  UP,
  DOWN,
  LEFT,
  RIGHT
}

public class CharacterMovement : MonoBehaviour
{

  public Animator animator;
  public Vector2 velocity;
  public float moveX;
  public float moveY;
  public float moveSpeedX = 5f;
  public float moveSpeedY = 5f;
  public InputAction movementAction;
  public Direction direction = Direction.DOWN;
  private bool idle = true;

  Rigidbody2D rb;
  Vector2 moveDirection = Vector2.zero;

  void Start()
  {
    rb = gameObject.GetComponent<Rigidbody2D>();
  }

  //Update is called once per frame
  void Update()
  {

    moveDirection = movementAction.ReadValue<Vector2>();
    idle = false;

    if (moveDirection.x < 0)
      direction = Direction.LEFT;
    else if (moveDirection.x > 0)
      direction = Direction.RIGHT;
    else if (moveDirection.y < 0)
      direction = Direction.DOWN;
    else if (moveDirection.y > 0)
      direction = Direction.UP;
    else
      idle = true;

    if (idle)
    {
      switch (direction)
      {
        case Direction.UP:
          animator.Play("idleUp");
          break;
        case Direction.DOWN:
          animator.Play("idleDown");
          break;
        case Direction.LEFT:
          animator.Play("idleLeft");
          break;
        case Direction.RIGHT:
          animator.Play("idleRight");
          break;
      }
    }
    else
    {
      switch (direction)
      {
        case Direction.UP:
          animator.Play("moveUp");
          break;
        case Direction.DOWN:
          animator.Play("moveDown");
          break;
        case Direction.LEFT:
          animator.Play("moveLeft");
          break;
        case Direction.RIGHT:
          animator.Play("moveRight");
          break;
      }
    }

    UpdateSortingLayer();
  }

  private void FixedUpdate()
  {
    rb.velocity = new Vector2(moveDirection.x * moveSpeedX, moveDirection.y * moveSpeedY);
  }

  void UpdateSortingLayer()
  {

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
