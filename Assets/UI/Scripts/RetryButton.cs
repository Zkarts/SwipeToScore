using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RetryButton : MonoBehaviour {

    public event Action OnRetry;

    //assign this in the editor
    //this structure seems redundant but helps a lot in tracking through code
    public void Click() {
        OnRetry?.Invoke();
    }

    public void Init() {
        Deactivate();
    }

    public void Activate() {
        gameObject.SetActive(true);
    }

    public void Deactivate() {
        gameObject.SetActive(false);
    }

}