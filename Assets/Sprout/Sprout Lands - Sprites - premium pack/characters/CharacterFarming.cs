using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterFarming : MonoBehaviour
{
  public InputAction tillAction;

  void Awake()
  {
    tillAction.performed += _ => Till();
  }

  void Till()
  {
    Debug.Log("Till");
  }

  private void OnEnable()
  {
    tillAction.Enable();
  }

  private void OnDisable()
  {
    tillAction.Disable();
  }
}
