using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityButtonContainer : MonoBehaviour {

    //gets called with the new BallType
    public event Action<BallType> OnBallTypeChanged;

    [SerializeField]
    private AbilityButton[] abilityButtons;

    private AbilityButton activeButton;

    public AbilityButton[] AbilityButtons {
        get { return abilityButtons; }
    }

    public bool IsAnyUnlocked {
        get {
            foreach (AbilityButton abilityButton in abilityButtons) {
                if (abilityButton.IsUnlocked) {
                    return true;
                }
            }
            return false;
        }
    }

    public void Init() {
        foreach (AbilityButton abilityButton in abilityButtons) {
            abilityButton.Init();
            abilityButton.OnClick += UpdateBallType;
            abilityButton.Deactivate();
        }
    }

    public void Activate(int level, int attemptTokens) {
        if (activeButton != null) {
            UpdateBallType(false, activeButton);
        }
        gameObject.SetActive(true);
        foreach (AbilityButton abilityButton in abilityButtons) {
            abilityButton.Activate(level, attemptTokens);
        }
    }

    public void Deactivate() {
        gameObject.SetActive(false);
        foreach (AbilityButton abilityButton in abilityButtons) {
            abilityButton.Deactivate();
        }
    }

    private void UpdateBallType(bool turnOn, AbilityButton abilityButton) {
        if (turnOn) {
            activeButton?.SetSelected(false);
            activeButton = abilityButton;
            OnBallTypeChanged?.Invoke(abilityButton.BallType);
            return;
        }

        if (!turnOn && abilityButton == activeButton) {
            activeButton.SetSelected(false);
            activeButton = null;
            OnBallTypeChanged?.Invoke(BallType.Normal);
        }
    }

}