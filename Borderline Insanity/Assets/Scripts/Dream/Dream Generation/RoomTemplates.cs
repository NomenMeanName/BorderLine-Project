using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {

    public bool spawnedMrX;

    public GameObject[] bottomRooms;
	public GameObject[] topRooms;
	public GameObject[] leftRooms;
	public GameObject[] rightRooms;

	public GameObject closedRoom;

	public List<GameObject> rooms;

	public float waitTime;
	public GameObject mrX;

	void Update(){

		if(waitTime <= 0 && spawnedMrX == false)
        {
			for (int i = 0; i < rooms.Count; i++)
            {
				if(i == rooms.Count-1)
                {
					Instantiate(mrX, rooms[i].transform.position, Quaternion.identity);
                    spawnedMrX = true;

                    GameObject x_Room = rooms[i];

                    x_Room.name = "MrX_Room";
				}
			}
		} else {
			waitTime -= Time.deltaTime;
		}
	}
}
