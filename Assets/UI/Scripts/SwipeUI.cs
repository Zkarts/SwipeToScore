using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwipeUI : MonoBehaviour {

    public event Action<BallType> OnBallTypeChanged {
        add { abilityButtonContainer.OnBallTypeChanged += value; }
        remove { abilityButtonContainer.OnBallTypeChanged -= value; }
    }

    [SerializeField]
    private TextMeshProUGUI levelText, attemptsText;

    [SerializeField]
    private AbilityButtonContainer abilityButtonContainer;

    public void Init() {
        abilityButtonContainer.Init();

        Deactivate();
    }

    public void Activate(int level, int attempts) {
        gameObject.SetActive(true);
        abilityButtonContainer.Activate(level, attempts);
        UpdateStatsText(level, attempts);

        //don't show "Attempt Token" text without skills to use them for
        attemptsText.gameObject.SetActive(abilityButtonContainer.IsAnyUnlocked);
    }

    public void UpdateStatsText(int level, int attempts) {
        levelText.text = "Level " + level;
        attemptsText.text = attempts + " Attempt Tokens";
    }

    public void Deactivate() {
        abilityButtonContainer.Deactivate();
        gameObject.SetActive(false);
    }

}