using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterSortingLayerHandler : MonoBehaviour
{
  // List<SpriteRenderer> defualtLayerSpriteRenderers = new List<SpriteRenderer>();
  List<Collider2D> upHillColliders = new List<Collider2D>();
  List<Collider2D> hillRidgeColliders = new List<Collider2D>();

  List<Collider2D> groundColliders = new List<Collider2D>();

  Collider2D charCollider;

  bool isUpHill = true;
  bool isOnLadder = false;
  SpriteRenderer sr;
  public Tilemap plantTilemap;

  void Awake()
  {
    // foreach (SpriteRenderer sR in gameObject.GetComponentsInChildren<SpriteRenderer>())
    // {
    //     if (sR.sortingLayerName == "Defualt") {
    //         defualtLayerSpriteRenderers.Add(sR);
    //     }
    // }

    foreach (GameObject upHillCollider in GameObject.FindGameObjectsWithTag("Up Hill Collider"))
    {
      upHillColliders.Add(upHillCollider.GetComponent<Collider2D>());
    }
    foreach (GameObject upHillCollider in GameObject.FindGameObjectsWithTag("Ladder Trigger"))
    {
      upHillColliders.Add(upHillCollider.GetComponent<Collider2D>());
    }

    foreach (GameObject hillRidgeCollider in GameObject.FindGameObjectsWithTag("Hill Collider"))
    {
      hillRidgeColliders.Add(hillRidgeCollider.GetComponent<Collider2D>());
    }

    foreach (GameObject groundCollider in GameObject.FindGameObjectsWithTag("Ground Collider"))
    {
      groundColliders.Add(groundCollider.GetComponent<Collider2D>());
    }
  }
  // Start is called before the first frame update
  void Start()
  {
    charCollider = gameObject.GetComponent<Collider2D>();
    sr = gameObject.GetComponent<SpriteRenderer>();
    UpdateSortingAndCollisionLayers();
  }

  // Update is called once per frame
  void Update()
  {

  }

  void SetSortingLayers(string layerName)
  {
    // foreach (SpriteRenderer sR in defualtLayerSpriteRenderers)
    // {
    //     sR.sortingLayerName = layerName;
    //     Debug.Log("Here");
    // }
    sr.sortingLayerName = layerName;
    plantTilemap.GetComponent<TilemapRenderer>().sortingLayerName = layerName;
  }

  void SetUpHillCollisions()
  {
    foreach (Collider2D collider in upHillColliders)
    {
      Physics2D.IgnoreCollision(charCollider, collider, !isUpHill);
    }

    foreach (Collider2D collider in hillRidgeColliders)
    {
      Physics2D.IgnoreCollision(charCollider, collider, isOnLadder);
    }

    foreach (Collider2D collider in groundColliders) {
        Physics2D.IgnoreCollision(charCollider, collider, isOnLadder);
    }
  }

  void UpdateSortingAndCollisionLayers()
  {
    if (isUpHill)
    {
      SetSortingLayers("Hill Layer");
    }
    else
    {
      SetSortingLayers("Default");
    }

    SetUpHillCollisions();
  }

  void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.CompareTag("Up Hill Trigger"))
    {
      isUpHill = true;
      UpdateSortingAndCollisionLayers();
    }

    if (collider.CompareTag("Down Hill Trigger"))
    {
      isUpHill = false;
      UpdateSortingAndCollisionLayers();
    }

    if (collider.CompareTag("Ladder Trigger"))
    {
      isOnLadder = true;
      UpdateSortingAndCollisionLayers();
    }


  }

  void OnTriggerExit2D(Collider2D collider)
  {
    if (collider.CompareTag("Ladder Trigger"))
    {
      isOnLadder = false;
      UpdateSortingAndCollisionLayers();
    }

  }

}

