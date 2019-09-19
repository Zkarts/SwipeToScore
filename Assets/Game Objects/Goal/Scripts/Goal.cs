using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    [SerializeField]
    private Transform leftAnchor, rightAnchor, bar, leftTop, rightTop;

    [SerializeField]
    private BoxCollider goalTrigger;

    public BoxCollider GoalTrigger { get => goalTrigger; }

    //set the positions of all elements to keep poles and bar thickness constant
    public void Init(float width, float height) {
        float halfWidth = 0.5f * width;
        float halfHeight = 0.5f * height;

        //set pole positions and sizes
        Vector3 anchorPos = leftAnchor.position;

        anchorPos.x = -halfWidth;
        leftAnchor.position = anchorPos;

        anchorPos.x = halfWidth;
        rightAnchor.position = anchorPos;

        Vector3 anchorScale = leftAnchor.localScale;
        anchorScale.y = halfHeight;
        leftAnchor.localScale = anchorScale;
        rightAnchor.localScale = anchorScale;

        //set bar position and scale
        Vector3 barPos = bar.position;
        barPos.y = height;
        bar.position = barPos;

        Vector3 barScale = bar.localScale;
        barScale.y = halfWidth;
        bar.localScale = barScale;

        //set the corner positions
        anchorPos.y = height;
        anchorPos.x = -halfWidth;
        leftTop.position = anchorPos;

        anchorPos.x = halfWidth;
        rightTop.position = anchorPos;

        //scale the collider a bit smaller to avoid pole shots triggering goals
        goalTrigger.transform.localScale = new Vector3(0.85f * width, height, 1);
        goalTrigger.transform.localPosition = new Vector3(0, 0.5f * height, goalTrigger.transform.localPosition.z);
    }

}
