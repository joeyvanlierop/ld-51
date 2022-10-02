using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoatManager : MonoBehaviour
{
    List<GameObject> Boats = new List<GameObject>();
    public List<GameObject> PossibleItems = new List<GameObject>();
    public GameObject BoatPrefab;
    float betweenBoatTime = 10f;
    float startTime = 10f;
    public Vector3 SpawnLocation = new Vector3(13.29f, -2.36f, 0.02834536f);
    // Start is called before the first frame update
    void Start()
    {
        // foreach (var go in GameObject.FindGameObjectsWithTag("Wanted Item")) {
        //     PossibleItems.Add(go);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBoat() {
        GameObject newBoat = Instantiate(BoatPrefab, SpawnLocation, Quaternion.identity);
        foreach (GameObject PossibleItem in PossibleItems) {
            newBoat.GetComponent<boatSoliciting>().wantedItems.Add(Instantiate(PossibleItem), Random.Range(0, 2));
        }
        Boats.Add(newBoat);
    }
    
    void FixedUpdate() {
        if (startTime > betweenBoatTime) {
            startTime = 0;
            SpawnBoat();
        } else {
            startTime += Time.deltaTime;
        }

        if (Boats.Count > 2) {
            var boat = Boats.First();
            boat.GetComponent<boatSoliciting>().DeleteAll();
            Boats.RemoveAt(0);
            Destroy(boat);
        }
    }
}
