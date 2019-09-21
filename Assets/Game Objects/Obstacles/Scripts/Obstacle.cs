using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    //use parent as pivot/anchor to make placement easier
    [SerializeField]
    private ObstacleBase parentObject;

    public void Destroy() {
        Destroy(parentObject.gameObject);
    }

}
