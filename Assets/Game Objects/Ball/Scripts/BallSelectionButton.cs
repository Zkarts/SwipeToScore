using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BallSelectionButton : MonoBehaviour {

    public event Action OnSelect;

    public void Select() {
        OnSelect?.Invoke();
    }

    public void Init(BallBehaviour ballObject) {

    }

    public void Activate() {
        gameObject.SetActive(true);
    }

    public void Deactivate() {
        gameObject.SetActive(false);
    }

}