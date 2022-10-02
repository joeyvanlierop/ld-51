using UnityEngine;

public class PlayButtonStuff : MonoBehaviour
{
  public float hoverSpeed = 4f;
  public float hoverAmplitude = 3f;

  void FixedUpdate()
  {
    // transform.localPosition = Vector2.up * Mathf.Cos(Time.time * hoverSpeed) * hoverAmplitude;
  }
}
