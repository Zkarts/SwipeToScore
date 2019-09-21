﻿using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class BallBehaviour : MonoBehaviour {

    private const string obstacleLayerName = "Obstacle";

    [SerializeField]
    private LayerMask fieldLayer;

    [SerializeField]
    private float pushPower = 500;

    public event Action OnShoot;
    public event Action OnScore;

    private BallCanvas ballCanvas;
    private SwipeHelper swipeHelper;
    private BoxCollider goalTrigger;
    private Rigidbody ballRigidbody;
    private Camera mainCamera;
    private bool holdingBall = false, alreadyShot = false;
    private int obstacleLayer;
    private BallType ballType;

    public bool AlreadyShot {
        get { return alreadyShot; }
    }

    public void Init(BallCanvas ballCanvas, SwipeHelper swipeHelper, BoxCollider goalTrigger, Camera mainCamera) {
        this.swipeHelper = swipeHelper;
        this.goalTrigger = goalTrigger;
        this.mainCamera = mainCamera;

        ballRigidbody = GetComponent<Rigidbody>();

        swipeHelper.OnSwipe += Shoot;
        swipeHelper.OnRelease += ReleaseBall;

        this.ballCanvas = ballCanvas;
        ballCanvas.Init(this);
        ballCanvas.Activate();
        ballCanvas.OnSelect += SetBallHeld;

        obstacleLayer = LayerMask.NameToLayer(obstacleLayerName);



        SetBallType(BallType.Fireball);
    }

    private void SetBallType(BallType ballType) {
        this.ballType = ballType;
    }

    private void OnCollisionEnter(Collision collision) {
        GameObject otherObj = collision.collider.gameObject;

        if (ballType == BallType.Fireball && otherObj.layer == obstacleLayer) {
            Obstacle obstacle = collision.collider.GetComponent<Obstacle>();

            if (obstacle == null) {
                Debug.LogError("Could not find obstacle script on collision!");
                Destroy(otherObj);
            }
            else {
                obstacle.Destroy();
            }

            Destroy(this.gameObject); //TODO: imagine cool fireball effects
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other == goalTrigger) {
            OnScore?.Invoke();
        }
    }

    private void SetBallHeld() {
        holdingBall = true;
    }

    private void ReleaseBall() {
        holdingBall = false;
    }

    private void Shoot(Vector2 screenSpaceDirection, Vector2 screenSpacePosition) {
        if (!holdingBall) {
            Debug.LogError("wasn't holding ball");
            return;
        }

        if (alreadyShot) {
            Debug.LogError("already shot");
            return;
        }

        //cast a ray to find the target position on the field
        Ray endPosRay = mainCamera.ScreenPointToRay(screenSpacePosition);
        RaycastHit endPosHit;

        if (!Physics.Raycast(endPosRay, out endPosHit, float.MaxValue, fieldLayer.value)) {
            Debug.LogError("did not hit field");
            return;
        }

        Vector3 worldDirection = endPosHit.point - transform.position;
        //hit is at ground level, so make sure the ball doesn't move down
        worldDirection.y = 0;

        ballRigidbody.AddForce(pushPower * worldDirection.normalized);
        OnShoot?.Invoke();

        alreadyShot = true;
        ballCanvas.Deactivate();
    }

    private void OnDestroy() {
        swipeHelper.OnSwipe -= Shoot;
        swipeHelper.OnRelease -= ReleaseBall;
        ballCanvas.OnSelect -= SetBallHeld;
    }

}