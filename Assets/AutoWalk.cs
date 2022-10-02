using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWalk : MonoBehaviour
{
  public GameStateManager gameStateManager;
  private CharacterMovement characterMovement;

  void Start()
  {
    characterMovement = gameObject.GetComponent<CharacterMovement>();
    characterMovement.autoWalk = true;
    StartCoroutine(Walk());
  }

  IEnumerator Walk()
  {
    var walkDirection = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
    var walkTime = Random.Range(0, 3);
    characterMovement.moveDirection = walkDirection;
    yield return new WaitForSeconds(walkTime);
    characterMovement.moveDirection = Vector2.zero;
    var waitTime = Random.Range(2, 4);
    yield return new WaitForSeconds(waitTime);
    if (gameStateManager.gameState == GameState.MAIN_MENU)
      StartCoroutine(Walk());
    else
      characterMovement.autoWalk = false;
  }
}
