using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{
    List<GameObject> Boats = new List<GameObject>();
    public GameObject BoatPrefab;
    float betweenBoatTime = 10f;
    float startTime = 0;
    public Vector3 SpawnLocation = new Vector3(13.29f, -2.36f, 0.02834536f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void FixedUpdate() {
        if (startTime > betweenBoatTime) {
            startTime = 0;
            GameObject newBoat = Instantiate(BoatPrefab, SpawnLocation, Quaternion.identity);
            newBoat.transform.SetPositionAndRotation(SpawnLocation, Quaternion.identity);
        } else {
            startTime += Time.deltaTime;
        }
    }
}
