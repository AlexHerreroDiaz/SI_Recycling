using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideShow : MonoBehaviour
{
    // Start is called before the first frame update
    public Image slideshowImage; // UI Image component to display the slideshow
    public Sprite[] slideshowSprites; // Array of sprites to display in the slideshow
    private int currentIndex = 0; // Current index in the slideshow

    void Start()
    {
        // Ensure the first image is displayed initially
        if (slideshowSprites.Length > 0)
        {
            slideshowImage.sprite = slideshowSprites[currentIndex];
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPreviousImage()
    {
        if (slideshowSprites.Length > 0)
        {
            currentIndex = (currentIndex - 1 + slideshowSprites.Length) % slideshowSprites.Length;
            slideshowImage.sprite = slideshowSprites[currentIndex];
        }
    }

    public void ShowNextImage()
    {
        if (slideshowSprites.Length > 0)
        {
            currentIndex = (currentIndex + 1) % slideshowSprites.Length;
            slideshowImage.sprite = slideshowSprites[currentIndex];
        }
    }
}
