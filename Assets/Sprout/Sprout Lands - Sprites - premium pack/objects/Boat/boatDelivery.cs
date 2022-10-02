using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatDelivery : MonoBehaviour
{

    public List<GameObject> RequestedIemsBubbles = new List<GameObject>();
    public Animator animator;
    bool delivering = false;
    public List<GameObject> choicesPrefab = new List<GameObject>();
    List<GameObject> choices = new List<GameObject>();

    public GameObject Timer;

    GameObject timerRef;

    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        foreach(var prefab in choicesPrefab) {
            choices.Add(Instantiate(prefab));
        }
        SetBubbles();
    }


    void SetBubbles() {
        for (int i = 0; i < choices.Count; i++) {
            choices[i].transform.SetParent(RequestedIemsBubbles[i].transform);
            choices[i].transform.localPosition = new Vector2(0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetBool("isCruisin")) {
            if (!delivering) {
                delivering = true;
                StartDelivering();
            }
        } else {
            if (delivering) {
                delivering = false;
                StopDelivering();
            }
        }
    }


    void StartDelivering() {
        for (int i = 0; i < choices.Count; i++) {
            choices[i].GetComponent<SpriteRenderer>().enabled = true;
            RequestedIemsBubbles[i].GetComponent<SpriteRenderer>().enabled = true;
        }
        timerRef = Instantiate(Timer, new Vector2(rb.transform.position.x, rb.transform.position.y - 1.5f), Quaternion.identity);
    }

    void StopDelivering() {
        for (int i = 0; i < choices.Count; i++) {
            choices[i].GetComponent<SpriteRenderer>().enabled = false;
            RequestedIemsBubbles[i].GetComponent<SpriteRenderer>().enabled = false;
        }
        Destroy(timerRef);
    }

    public void DeleteAll() {
        foreach(var choice in choices) {
            Destroy(choice);
        }
    }
}
