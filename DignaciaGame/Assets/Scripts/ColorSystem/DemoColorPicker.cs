using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DmeoInfoGamerHubAssets
{
    public class DemoColorPicker : MonoBehaviour
    {
        public void SetColor(Color newColor)
        {
            GetComponent<SpriteRenderer>().material.color = newColor;
            Debug.Log(newColor);
        }
    }
}