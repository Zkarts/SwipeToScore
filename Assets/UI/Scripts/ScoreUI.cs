using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour {

    //redirect to retryButton
    public event Action OnRetry {
        add { retryButton.OnRetry += value; }
        remove { retryButton.OnRetry -= value; }
    }

    [SerializeField]
    private TextMeshProUGUI goalText;

    [SerializeField]
    private RetryButton retryButton;

    private SwipeToScoreUI swipeToScoreUI;

    public void Init(SwipeToScoreUI swipeToScoreUI) {
        retryButton.Init();
    }

    public void Activate() {
        gameObject.SetActive(true);
        retryButton.Activate();
    }

    public void Deactivate() {
        gameObject.SetActive(false);
    }

}