using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType { START, CORRIDOR, PLAY, HORIZONTAL_CORRIDOR, }

public class SpawnRooms : MonoBehaviour
{
    public bool isLeadingToX, isLeft;

    public GameObject[] rooms;

    public int length;

    public RoomType type;

    public Transform spawnPoint;

    public GameObject spawnedObj;

    private void Start()
    {
        SpawnRoom();
    }

    void SpawnRoom()
    {
        //int direction = Random.Range(0,4);
        int randomRoom = Random.Range(0, rooms.Length);
        GameObject room = rooms[randomRoom];

        //switch (type)
        //{
           // case RoomType.START:
        if (length > 0)
        {
            var obj = Instantiate(room);
            spawnedObj = obj;
            if (isLeft)
                obj.transform.position = spawnPoint.position + new Vector3(room.transform.Find("Walls").localScale.x / -15.75f, 0);
            else
                obj.transform.position = spawnPoint.position + new Vector3(room.transform.Find("Walls").localScale.x / 15.75f, 0);

            room.GetComponentInChildren<SpawnRooms>().length = length - 1;
        }
                
                //break;
                
        //}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<SpawnRooms>())
        {
            collision.GetComponent<SpawnRooms>().enabled = false;
        }
    }
}
