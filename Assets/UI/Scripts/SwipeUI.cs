using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwipeUI : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI levelText, attemptsText;

    [SerializeField]
    private AbilityButtonContainer abilityButtonContainer;

    public void Init() {
        abilityButtonContainer.Init();
        //abilityButtonContainer.OnBallTypeChanged += 

        Deactivate();
    }

    public void Activate(int level, int attempts) {
        gameObject.SetActive(true);
        abilityButtonContainer.Activate(level, attempts);
        levelText.text = "Level " + level;
        attemptsText.text = attempts + " Attempt Tokens";
    }

    public void Deactivate() {
        abilityButtonContainer.Deactivate();
        gameObject.SetActive(false);
    }

}