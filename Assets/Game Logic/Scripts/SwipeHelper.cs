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
    public event Action OnRelease;

    [SerializeField]
    private float screenWidthFactorForSwipe = 0.25f, swipeTimeThreshold = 0.2f;

    private float swipeDistanceThreshold = 0f;

    private Vector2 startPos;
    private float holdTime = 0;

    private void Start() {
        //use factor of screen width instead of set value for scalability
        swipeDistanceThreshold = Screen.width * screenWidthFactorForSwipe;
        //Debug.Log(swipeDistanceThreshold);
    }

    private void Update() {
        //separate input to allow for testing in editor as well
#if     UNITY_EDITOR
        CheckSwipeEditor();
#elif   UNITY_ANDROID || UNITY_IOS
        CheckSwipeTouch();
#endif
    }

    private void CheckSwipeTouch() {
        if (Input.touches.Length > 0) {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase) {
                case TouchPhase.Began:
                    startPos = touch.position;
                    holdTime = 0;
                    break;
                case TouchPhase.Moved:
                    holdTime += Time.deltaTime;
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    Vector2 endPos = touch.position;
                    Vector2 deltaPos = endPos - startPos;
                    if (deltaPos.magnitude > swipeDistanceThreshold && holdTime < swipeTimeThreshold) {
                        OnSwipe?.Invoke(deltaPos, endPos);
                    }
                    else {
                        OnRelease?.Invoke();
                    }
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }
        }
    }

    private void CheckSwipeEditor() {
        if (Input.GetMouseButtonDown(0)) { //pressed
            startPos = Input.mousePosition;
            holdTime = 0;
        }
        else if (Input.GetMouseButton(0)) { //held
            holdTime += Time.deltaTime;
        }
        else if (Input.GetMouseButtonUp(0)) { //de-pressed
            Vector2 endPos = Input.mousePosition;
            Vector2 deltaPos = endPos - startPos;

            //Debug.Log("" + (deltaPos.magnitude > swipeDistanceThreshold) + " and " + (holdTime < swipeTimeThreshold));
            if (deltaPos.magnitude > swipeDistanceThreshold && holdTime < swipeTimeThreshold) {
                OnSwipe?.Invoke(deltaPos, endPos);
            }
            else {
                OnRelease?.Invoke();
            }
        }
    }

}