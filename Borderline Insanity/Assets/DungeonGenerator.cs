using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public bool bothWays; // If the doors will lead to both ways
    public GameObject[] doors;
    public int wayLength;

    public RoomType type = RoomType.START; 
    void Start()
    {
        int rand = Random.Range(0,2);

        wayLength = Random.Range(3,6);

        // Decide if both doors will lead to mr.x
        switch (rand)
        {
            case 0:
                bothWays = false;
                break;
            case 1:
                bothWays = true;
                break;
            default:
                Debug.Log("Error : 'both ways' indexer is out of range");
                break;
        }

        switch (bothWays)
        {
            case true:
                Debug.Log("BothWays");                
                break;

            case false:
                var randTwo = Random.Range(0, doors.Length);
                GameObject door = doors[randTwo];
                Debug.Log(door.name);

                var spawn = door.GetComponent<SpawnRooms>();
                break;
        }

        foreach (GameObject dor in doors)
        {
            var spawn = dor.GetComponent<SpawnRooms>();
            spawn.length = wayLength;
            spawn.type = type;
        }
    }
}
