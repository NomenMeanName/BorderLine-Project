using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay, nameDisplay;

    public TextMeshProUGUI goodOption, badOption, neutralOption;
    [Header("Choices")]
    [TextArea(3,10)]
    public string goodOption_text, badOption_text, neutralOption_text;

    [Header("Choices")]
    [TextArea(3, 10)]
    public string goodOption_reaction, badOption_reaction, neutralOption_reaction;

    public string speakerName;

    [TextArea(3,10)]
    public string[] sentences;

    private int index;

    public GameObject continueButton, optionsWindow;

    public float typingSpeed;

    bool timer_activated; float timer = 6;

    private void Start()
    {
        textDisplay.text = "";
        //StartCoroutine(Type());
        //continueButton.SetActive(false);

        Type();

        nameDisplay.text = speakerName + " :";

    }

    private void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
            if (Input.GetKeyDown("space"))
            {
                NextSentance();
            }
        }      
        
        if (timer_activated)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }


    void Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            //yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentance()
    {
        //continueButton.SetActive(false);

        if (index < sentences.Length -1)
        {
            index++;
            textDisplay.text = "";
           Type();
        }
        else if (index == sentences.Length -1)
        {
            Debug.Log("end of dialogue");
            //textDisplay.text = "";
            optionsWindow.SetActive(true);
            goodOption.text = goodOption_text;
            badOption.text = badOption_text;
            neutralOption.text = neutralOption_text;
        }
    }
    
    public void GoodOption()
    {
        textDisplay.text = goodOption_reaction;
        optionsWindow.SetActive(false);
        continueButton.SetActive(false);
        timer_activated = true;
    }
    public void BadOption()
    {
        textDisplay.text = badOption_reaction;
        optionsWindow.SetActive(false);
        continueButton.SetActive(false);
        timer_activated = true;
    }
    public void NeutralOption()
    {
        textDisplay.text = neutralOption_reaction;
        optionsWindow.SetActive(false);
        continueButton.SetActive(false);
        timer_activated = true;
    }
}
