using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class AbilityButton : MonoBehaviour {

    //Shared between all ability buttons
    private static Color costTextEnabledColor = new Color(1f, 0.61f, 0);
    private static Color costTextDisabledColor = new Color(0.6f, 0.36f, 0);
    private static Color selectedColor = new Color(0.75f, 0.75f, 0.75f);
    private static Color deselectedColor = new Color(1f, 1f, 1f);

    public event Action<bool, AbilityButton> OnClick;

    private bool isUnlocked;

    [SerializeField]
    private TextMeshProUGUI costText;

    [SerializeField]
    private int cost, unlockLevel = -1;

    [SerializeField]
    private BallType ballType;

    private Button button;
    private bool isActive;

    public BallType BallType {
        get { return ballType; }
    }

    public void Click() {
        SetSelected(!isActive);

        OnClick?.Invoke(isActive, this);
    }

    public void SetSelected(bool active) {
        isActive = active;
        button.image.color = isActive ? selectedColor : deselectedColor;
    }

    public void Init() {
        costText.text = cost.ToString();
        button = GetComponent<Button>();
    }

    public void Activate(int level, int attemptTokens) {
        if (unlockLevel > 0 && level >= unlockLevel) {
            isUnlocked = true;
        }

        if (!isUnlocked) {
            return;
        }

        gameObject.SetActive(true);
        if (attemptTokens >= cost) {
            costText.color = costTextEnabledColor;
            button.interactable = true;
        }
        else {
            costText.color = costTextDisabledColor;
            button.interactable = false;
        }
    }

    public void Deactivate() {
        gameObject.SetActive(false);
    }

}