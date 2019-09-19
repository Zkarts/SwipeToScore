using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class BallBehaviour : MonoBehaviour {

    [SerializeField]
    private float pushPower = 500;

    public event Action OnShoot;

    private GameHandler gameHandler;
    private SwipeHelper swipeHelper;
    private BoxCollider goalTrigger;
    private Rigidbody ballRigidbody;
    private Camera mainCamera;
    private bool shot = false;

    public void Init(GameHandler gameHandler, SwipeHelper swipeHelper, BoxCollider goalTrigger, Camera mainCamera) {
        this.gameHandler = gameHandler;
        this.swipeHelper = swipeHelper;
        this.goalTrigger = goalTrigger;
        this.mainCamera = mainCamera;

        ballRigidbody = GetComponent<Rigidbody>();

        swipeHelper.OnSwipe += Shoot;
    }

    private void OnTriggerEnter(Collider other) {
        if (other == goalTrigger) {
            gameHandler.Score();
        }
    }

    private void Shoot(Vector2 screenSpaceDirection, Vector2 screenSpacePosition) {
        if (shot) {
            return;
        }

        Ray endPosRay = mainCamera.ScreenPointToRay(screenSpacePosition);

        RaycastHit endPosHit;
        if (!Physics.Raycast(endPosRay, out endPosHit, LayerMask.NameToLayer("Field"))) {
            return;
        }

        Vector3 worldDirection = endPosHit.point - transform.position;
        //hit is at ground level, so make sure the ball doesn't move down
        worldDirection.y = 0;

        ballRigidbody.AddForce(pushPower * worldDirection.normalized);
        OnShoot?.Invoke();
        shot = true;
    }

    private void OnDestroy() {
        swipeHelper.OnSwipe -= Shoot;
    }

}