using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomContent : MonoBehaviour
{
    public GameObject enemy, treasure;// platformRef;
    public Transform [] enemySpawnPoints;
    public GameObject[] platformRef;

    float spawnContent_time = 1f;
    bool spawnedContent;

    private void Start()
    {
        /*if (name == "TB")
        {
            if (transform.localEulerAngles == new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 90))
            {
                foreach(GameObject plat in platformRef)
                {
                    plat.SetActive(true);
                }
            }
        }*/

    }

    private void Update()
    {
        spawnContent_time -= Time.deltaTime;
        if (spawnContent_time <= 0 && spawnedContent == false)
        {
            SpawnContent();
            spawnedContent = true;
        }

    }

    void SpawnContent()
    {
        var howMuchEnemies = Random.Range(0, 3);
       // Debug.Log(howMuchEnemies);

        if (name == "MrX_Room")//&& type == RoomType.REGULAR)
        {
            foreach (GameObject plat in platformRef)
            {
                plat.SetActive(false);
            }

            foreach (Transform enem in enemySpawnPoints)
            {
                enem.gameObject.SetActive(false);
            }
        }        

        //if (type == RoomType.REGULAR)
        //{
            // Enemy Spawning part   
            if (enemySpawnPoints.Length > 1)
            {
                for (int i = 0; i <= howMuchEnemies; i++)
                {
                    Instantiate(enemy, enemySpawnPoints[i].position, Quaternion.identity);
                }
            }
            else if (enemySpawnPoints.Length == 1)
            {
                Instantiate(enemy, enemySpawnPoints[0].position, Quaternion.identity);
            }
        //}
        /*else if (type == RoomType.LOOT)
        {
            treasure.SetActive(true);
        }*/
        if (name != "MrX_Room")
        {
            if (name.Contains("T") || name.Contains("B"))
            {
                float rand = Random.Range(0, 6);

                if (rand == 5)
                {
                    treasure.SetActive(true);
                }
            }
        }
    }
}
