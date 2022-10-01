using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatSoliciting : MonoBehaviour
{

    public Dictionary<int, GameObject> wantedItems = new Dictionary<int, GameObject>();
    public Animator animator;
    public List<GameObject> RequestedIemsBubbles = new List<GameObject>();
    public GameObject RequestedIemsBubble;
    bool soliciting = false;


    // Start is called before the first frame update
    void Start()
    {
        int distinctItems = wantedItems.Count;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetBool("isCruisin")) {
            if (!soliciting) {
                soliciting = true;
                StartSoliciting();
            }
        } else {
            if (soliciting) {
                soliciting = false;
                StopSoliciting();
            }
        }

        
    }

    void StartSoliciting() {
        RequestedIemsBubble.GetComponent<SpriteRenderer>().enabled = true;
        foreach (var wantedItemPair in wantedItems) {
            var wantedItem = wantedItemPair.Value;
            wantedItem.transform.SetParent(RequestedIemsBubble.transform);
            wantedItem.GetComponent<SpriteRenderer>().sortingOrder = 1000;
            wantedItem.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            wantedItem.GetComponent<BoxCollider2D>().enabled = false;
            wantedItem.transform.localPosition = new Vector2(0, 0);
        }
    }

    void StopSoliciting() {
        RequestedIemsBubble.GetComponent<SpriteRenderer>().enabled = false;
    }
}
