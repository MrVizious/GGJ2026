using System;

using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    [SerializeField] 
    Image catchPj1_bg;
    [SerializeField] 
    Image catchPj2_bg;
    [SerializeField] 
    Image catchPj1;
    [SerializeField] 
    Image catchPj2;
    [SerializeField] 
    Image searchImageChar_1;
    [SerializeField] 
    Image searchImageBg_1;
    [SerializeField] 
    Image searchImageSeal_1;
    [SerializeField] 
    Image searchImageChar_2;
    [SerializeField] 
    Image searchImageBg_2;
    [SerializeField] 
    Image searchImageSeal_2;
    [SerializeField] 
    Image searchImageChar_3;
    [SerializeField] 
    Image searchImageBg_3;
    [SerializeField] 
    Image searchImageSeal_3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTargetSprite(Sprite imageTarget, int numTarget)
    {
        switch (numTarget)
        {
            case 0:
                searchImageSeal_1.GetComponent<Image>().sprite = imageTarget;
                break;
            case 1:
                searchImageSeal_2.GetComponent<Image>().sprite = imageTarget;
                break;
            case 2:
                searchImageSeal_3.GetComponent<Image>().sprite = imageTarget;
                break;
        }
    }

    public void TargetCaught(Sprite imageTarget, int numTarget){
        switch (numTarget)
        {
            case 0:
                searchImageSeal_1.GetComponent<Image>().sprite = imageTarget;
                searchImageSeal_1.enabled = true;
                break;
            case 1:
                searchImageSeal_2.GetComponent<Image>().sprite = imageTarget;
                searchImageSeal_2.enabled = true;
                break;
            case 2:
                searchImageSeal_3.GetComponent<Image>().sprite = imageTarget;
                searchImageSeal_3.enabled = true;
                break;
        }
    }
}
