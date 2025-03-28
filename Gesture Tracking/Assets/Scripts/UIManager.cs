using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;

public class UIManager : MonoBehaviour
{
    public GameObject swipeLeftImage;
    public GameObject swipeRightImage;

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
        // Ensure both images are initially disabled
        swipeLeftImage.SetActive(false);
        swipeRightImage.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateImage("Swipe Left");
        }
    }


    public void ActivateImage(string gesture)
    {
        swipeLeftImage.SetActive(false);
        swipeRightImage.SetActive(false);

        if (gesture.Trim().Equals("Swipe Left", StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("Swipe Left detected, activating image.");
            StartCoroutine(DelayedActivation(swipeLeftImage));
        }
        else if (gesture.Trim().Equals("Swipe Right", StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("Swipe Right detected, activating image.");
            StartCoroutine(DelayedActivation(swipeRightImage));
        }
    }

    IEnumerator DelayedActivation(GameObject image)
    {
        yield return new WaitForSeconds(0.1f);
        image.SetActive(true);
        Debug.Log($"{image.name} is now active.");
    }
}
