using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI welcomeText;
    public TextMeshProUGUI endText;
    public Button startButton;
    public List<GameObject> images;

    private int currentIndex = 0;
    private bool canSwipe = false;
    private bool hasStarted = false;
    private bool hasShownStartButton = false;
    public float swipeCooldown = 0.5f;

    #region Singleton
    public static UIManager Instance;
    private void Awake()
    {
        if (Instance == null)
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
        ResetUI();
        startButton.onClick.AddListener(StartApp);
    }

    public void ActivateImage(string gesture)
    {
        if (gesture.Trim().Equals("Hi (Waving)", StringComparison.OrdinalIgnoreCase) && !hasStarted)
        {
            ShowStartButton();
        }
        else if (gesture.Trim().Equals("Like (Thumbs Up)", StringComparison.OrdinalIgnoreCase) && !hasStarted)
        {
            TriggerStartButton();
        }
        else if (gesture.Trim().Equals("Dislike (Thumbs Down)", StringComparison.OrdinalIgnoreCase))
        {
            EndApp();
        }
        else if (canSwipe && hasStarted)
        {
            if (gesture.Trim().Equals("Swipe Left", StringComparison.OrdinalIgnoreCase))
            {
                SwipeLeft();
            }
            else if (gesture.Trim().Equals("Swipe Right", StringComparison.OrdinalIgnoreCase))
            {
                SwipeRight();
            }
        }
    }

    private void ShowStartButton()
    {
        if (!hasShownStartButton)
        {
            Debug.Log("Hi Gesture detected. Showing Start Button.");
            startButton.gameObject.SetActive(true);
            welcomeText.gameObject.SetActive(false);
            endText.gameObject.SetActive(false);
            hasShownStartButton = true;
        }
    }

    private void TriggerStartButton()
    {
        if (startButton.gameObject.activeSelf)
        {
            Debug.Log("Like Gesture detected. Triggering Start Button.");
            StartApp();
        }
    }

    private void StartApp()
    {
        hasStarted = true;
        canSwipe = true;
        startButton.gameObject.SetActive(false);
        currentIndex = 0;
        UpdateImageDisplay();
    }

    private void EndApp()
    {
        Debug.Log("Dislike Gesture detected. Showing End Text.");

        hasStarted = false;
        canSwipe = false;
        hasShownStartButton = false; // Reset so "Hi" can start again

        startButton.gameObject.SetActive(false);
        welcomeText.gameObject.SetActive(false);
        endText.gameObject.SetActive(true);

        foreach (GameObject image in images)
        {
            image.SetActive(false);
        }
    }

    private void ResetUI()
    {
        welcomeText.gameObject.SetActive(true);
        endText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        hasStarted = false;
        canSwipe = false;
        hasShownStartButton = false;

        foreach (GameObject image in images)
        {
            image.SetActive(false);
        }
    }

    private void SwipeLeft()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            Debug.Log("Swiped Left, showing previous image.");
            UpdateImageDisplay();
            StartCoroutine(SwipeCooldown());
        }
    }

    private void SwipeRight()
    {
        if (currentIndex < images.Count - 1)
        {
            currentIndex++;
            Debug.Log("Swiped Right, showing next image.");
            UpdateImageDisplay();
            StartCoroutine(SwipeCooldown());
        }
    }

    private void UpdateImageDisplay()
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].SetActive(i == currentIndex);
        }
    }

    private IEnumerator SwipeCooldown()
    {
        canSwipe = false;
        yield return new WaitForSeconds(swipeCooldown);
        canSwipe = true;
    }
}
