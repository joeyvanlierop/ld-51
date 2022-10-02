using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{

    public static Event Instance;

    public delegate void EatCallbackType();


    public List<EatCallbackType> EatCallbacks = new List<EatCallbackType>();
    // Start is called before the first frame update
    public Event() 
    { 
        // If there is an instance, and it's not me, delete myself.
        if (Instance == null) {
            Instance = this; 
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Eat() {
        Debug.Log("Here");
        foreach (var eat in EatCallbacks) {
            eat();
        }
    }
}
