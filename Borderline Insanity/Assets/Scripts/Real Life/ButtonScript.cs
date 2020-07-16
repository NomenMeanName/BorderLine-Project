using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    public int howMuchTime;

    private void OnMouseDown()
    {
        switch (gameObject.name)
        {
            case "Work":
                if (PlayerPrefs.GetInt("actualTime") >= 6 && PlayerPrefs.GetFloat("actualTime") < 7)
                {
                    SceneManager.LoadScene(1);
                }
                else
                {
                    Debug.Log("Too late/early");
                }
                break;
            case "Hangout":
                if (PlayerPrefs.GetInt("actualTime") >= 17 && PlayerPrefs.GetFloat("actualTime") <= 22)
                {
                    SceneManager.LoadScene(3);
                    PlayerPrefs.SetInt("takenTime", howMuchTime);
                }
                else
                {
                    Debug.Log("Too late/early");
                }
                break;
        }
    }

    private void Update()
    {
        switch (gameObject.name)
        {
            case "Time":

                GetComponent<TextMeshPro>().text = howMuchTime.ToString("00") + ":00";

                if (Input.GetKeyDown("return"))
                {
                    //PlayerPrefs.SetInt("timesWorked", howMuchTime);
                    PlayerPrefs.SetInt("takenTime", howMuchTime);
                    SceneManager.LoadScene(2);
                }

                if (Input.GetKeyDown("w"))
                {
                    howMuchTime += 1;
                    if (howMuchTime > 15)
                    {
                        howMuchTime = 14;
                    }
                }
                else if (Input.GetKeyDown("s"))
                {
                    howMuchTime -= 1;
                    if (howMuchTime <= 0)
                    {
                        howMuchTime = 1;
                    }
                }
                break;
                // Multiply by 3 to know how much minutes in real life the dream will last
        }
    }
}
