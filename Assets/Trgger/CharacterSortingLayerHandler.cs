using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSortingLayerHandler : MonoBehaviour
{
    // List<SpriteRenderer> defualtLayerSpriteRenderers = new List<SpriteRenderer>();
    List<Collider2D> upHillColliders = new List<Collider2D>();
    List<Collider2D> downHillColliders = new List<Collider2D>();

    Collider2D charCollider;

    bool isUpHill = false;
    SpriteRenderer sr;
    void Awake() {
        // foreach (SpriteRenderer sR in gameObject.GetComponentsInChildren<SpriteRenderer>())
        // {
        //     if (sR.sortingLayerName == "Defualt") {
        //         defualtLayerSpriteRenderers.Add(sR);
        //     }
        // }

        foreach (GameObject upHillCollider in GameObject.FindGameObjectsWithTag("Up Hill Collider")) {
            upHillColliders.Add(upHillCollider.GetComponent<Collider2D>());
        }
        foreach (GameObject downHillCollider in GameObject.FindGameObjectsWithTag("Untagged")) {
            downHillColliders.Add(downHillCollider.GetComponent<Collider2D>());
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

    void SetSortingLayers(string layerName) {
        // foreach (SpriteRenderer sR in defualtLayerSpriteRenderers)
        // {
        //     sR.sortingLayerName = layerName;
        //     Debug.Log("Here");
        // }
        sr.sortingLayerName = layerName;
    }

    void SetUpHillCollisions() {
        foreach (Collider2D collider in upHillColliders) {
            Physics2D.IgnoreCollision(charCollider, collider, !isUpHill);
        }

        foreach (Collider2D collider in downHillColliders) {
            Physics2D.IgnoreCollision(charCollider, collider, isUpHill);
        }
    }

    void UpdateSortingAndCollisionLayers() {
        if (isUpHill) {
            SetSortingLayers("Hill Layer");
        } else
        {
            SetSortingLayers("Default");
        }

        SetUpHillCollisions();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Up Hill Trigger")) {
            isUpHill = true;
        }

        if (collider.CompareTag("Down Hill Trigger")) {
            isUpHill = false;
        }
        UpdateSortingAndCollisionLayers();
        
    }

}

