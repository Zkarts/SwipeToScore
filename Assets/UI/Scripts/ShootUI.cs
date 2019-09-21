using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShootUI : MonoBehaviour {

    //redirect to retryButton
    public event Action OnRetry {
        add { retryButton.OnRetry += value; }
        remove { retryButton.OnRetry -= value; }
    }

    [SerializeField]
    private RetryButton retryButton;
    
    public void Init() {
        retryButton.Init();
        Deactivate();
    }

    public void Activate() {
        gameObject.SetActive(true);
        retryButton.Activate();
    }

    public void Deactivate() {
        gameObject.SetActive(false);
        retryButton.Deactivate();
    }

}