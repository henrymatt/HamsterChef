using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UI_AlertPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _alertText;
    [SerializeField] private Image _visiblePanel;
    [SerializeField] private float _secondsToDisplay = 3f;
    private float _timeSinceDisplay = 0f;
    private bool _isDisplaying;
    
    private void OnEnable()
    {
        EventHandler.DidFailAttemptToHideEvent += DisplayCheeksTooFullToHideAlert;
        EventHandler.DidHideEvent += HideAlert;
    }

    private void OnDisable()
    {
        EventHandler.DidFailAttemptToHideEvent -= DisplayCheeksTooFullToHideAlert;
    }

    private void Update()
    {
        if (_isDisplaying)
        {
            _timeSinceDisplay += Time.deltaTime;
            if (_timeSinceDisplay > _secondsToDisplay)
            {
                HideAlert();
            }
        }
    }

    private void DisplayCheeksTooFullToHideAlert()
    {
        if (_isDisplaying) return;

        _isDisplaying = true;
        _visiblePanel.gameObject.SetActive(true);
        _alertText.text = "Cheeks too full to hide!";
    }

    private void HideAlert()
    {
        _visiblePanel.gameObject.SetActive(false);
        _timeSinceDisplay = 0f;
        _isDisplaying = false;
    }
}
