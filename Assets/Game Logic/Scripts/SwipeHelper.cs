using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.EventSystems;
using TMPro;

public class SwipeHelper : MonoBehaviour {

    //gets called with screenSpaceDirection and screenSpacePosition
    public event Action<Vector2, Vector2> OnSwipe;

    [SerializeField]
    private float screenWidthFactorForSwipe = 0.25f, swipeTimeThreshold = 1f;

    private float swipeDistanceThreshold = 0f;
    private float velocity = 0f;
    private Vector2 prevPosition;
    private bool swiping = false;





    public TextMeshProUGUI debugText;





    private void Start() {
        //use factor of screen width instead of set value for scalability
        swipeDistanceThreshold = Screen.width * screenWidthFactorForSwipe;
        Debug.Log(swipeDistanceThreshold);
    }

    private void Update() {
        //separate input to allow for testing in editor as well
#if     UNITY_EDITOR
        CheckSwipeEditor();
#elif   UNITY_ANDROID || UNITY_IOS
        CheckSwipeTouch();
#endif
        UpdateTimer();
    }

    private void CheckSwipeTouch() {
        if (Input.touches.Length > 0) {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase) {
                case TouchPhase.Began:
                    break;
                case TouchPhase.Moved:
                    swiping = touch.deltaPosition.magnitude > swipeDistanceThreshold;
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    if (swiping) {
                        OnSwipe?.Invoke(touch.deltaPosition, touch.position);
                    }
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }
        }
        debugText.text = "swiping: " + swiping;
    }

    private void CheckSwipeEditor() {
        if (Input.GetMouseButtonDown(0)) { //pressed
            prevPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0)) { //held
            Vector2 newPosition = Input.mousePosition;
            Vector2 deltaPos = newPosition - prevPosition;
            swiping = deltaPos.magnitude > swipeDistanceThreshold;
            prevPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0)) { //de-pressed
            if (swiping) {
                Vector2 newPosition = Input.mousePosition;
                Vector2 deltaPos = newPosition - prevPosition;
                OnSwipe?.Invoke(deltaPos, newPosition);
            }
        }
        debugText.text = "swiping: " + swiping;
    }




    float deltaTime = 0.0f;

    void UpdateTimer() {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI() {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}