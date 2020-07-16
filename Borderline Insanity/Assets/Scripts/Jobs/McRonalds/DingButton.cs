using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DingButton : MonoBehaviour
{
    public Image v, x, topBun;
    public RandomOrder randomOrder;
    public Order order;
    public GameObject Orders;

    public float scoreValue;
    public TextMeshProUGUI score;

    private Vector3 topBun_position;

    public GameObject [] ingredients;

    public bool clientSatisfied;

    private void Start()
    {
        score.text = "Score: 0";
        topBun_position = topBun.rectTransform.localPosition;
    }

    private void Update()
    {
        score.text = "Score: " + scoreValue;
    }

    public void OnClick()
    {

        if ((randomOrder.isCheese == order.isCheese) && (randomOrder.isLettuce == order.isLettuce) && (randomOrder.isTomato == order.isTomato) && (randomOrder.isPickle == order.isPickle) && (randomOrder.isOnion == order.isOnion))
        {
            scoreValue += 1;           
            v.enabled = true;
            clientSatisfied = true;
            //sound
        }
        else
        {
            x.enabled = true;
            //xSound.Play();
        }        
        StartCoroutine(DisableOrder());

    }
    public IEnumerator DisableOrder()
    {
        yield return new WaitForSeconds(2f);
        //Orders.SetActive(false);
        v.enabled = false;
        x.enabled = false;

        topBun.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; // Set topBun's body type to static
        topBun.rectTransform.localPosition = topBun_position;
        clientSatisfied = false;

        order.OReset();
        randomOrder.ResetOrder();
        //randomOrder.OnClick();

        foreach (GameObject ingri in ingredients)
        {
            ingri.SetActive(true);
        }


        Debug.Log("Order Disabled");
    }
}   
