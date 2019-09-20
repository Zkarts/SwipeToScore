using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCanvas : MonoBehaviour {

    //redirect to ballSelectionButton's OnSelect
    public event Action OnSelect {
        add { ballSelectionButton.OnSelect += value; }
        remove { ballSelectionButton.OnSelect -= value; }
    }

    [SerializeField]
    private BallSelectionButton ballSelectionButton;

    public void Init(BallBehaviour ball) {
        Vector3 position = ball.transform.position;
        position.y = 0.01f;
        transform.position = position;
    }

    public void Activate() {
        gameObject.SetActive(true);
    }

    public void Deactivate() {
        gameObject.SetActive(false);
    }

}
