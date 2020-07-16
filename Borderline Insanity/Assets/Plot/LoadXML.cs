using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class LoadXML : MonoBehaviour
{
    [SerializeField]
    private TextAsset dialogueFile;

    /*private string GetAttributeValue(string attributeName, XElement element);
    {
        XAttribute xAttribute = element.Attribute(attributeName);
        string value = string.Empty;
        if (xAttribute != null)
        {
            value = xAttribute.Value;
        }

        return value;
    }*/
}
