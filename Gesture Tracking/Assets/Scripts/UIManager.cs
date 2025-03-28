using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public List<GameObject> images;
    private int currentIndex = 0;

    #region Singleton

    public static UIManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    private void Start()
    {
        UpdateImageDisplay();
    }

    public void ActivateImage(string gesture)
    {
        if (gesture.Trim().Equals("Swipe Left", StringComparison.OrdinalIgnoreCase))
        {
            SwipeLeft();
        }
        else if (gesture.Trim().Equals("Swipe Right", StringComparison.OrdinalIgnoreCase))
        {
            SwipeRight();
        }
        else
        {
            Debug.LogWarning($"Unknown gesture received: '{gesture}'");
        }
    }

    private void SwipeLeft()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            Debug.Log("Swiped Left, showing previous image.");
            UpdateImageDisplay();
        }
    }

    private void SwipeRight()
    {
        if (currentIndex < images.Count - 1)
        {
            currentIndex++;
            Debug.Log("Swiped Right, showing next image.");
            UpdateImageDisplay();
        }
    }

    private void UpdateImageDisplay()
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].SetActive(i == currentIndex);
        }
    }
}
