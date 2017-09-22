using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelPath : MonoBehaviour {

    public GameObject pathGO;

    public List<GameObject> pathToFollow;
    public float speed;
    public float nodeDistanceTolerance;

    public GameObject goingTo;

    private Rigidbody rigid;
    private int i = 0;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start() {
        if (pathToFollow != null && pathToFollow[i] != null)
        {
            goingTo = pathToFollow[i];
            rigid.velocity = (goingTo.transform.position - transform.position).normalized * speed;
        }


	}
	
	// Update is called once per frame
	void Update () {
        if (goingTo == null) return;

        Vector3 myPositionWithZeroY = transform.position;
        myPositionWithZeroY.y = 0f;

        Vector3 nodePositionWithZeroY = goingTo.transform.position;
        nodePositionWithZeroY.y = 0f;

        if (Vector3.Distance(myPositionWithZeroY, nodePositionWithZeroY) < nodeDistanceTolerance)
        {

            if (i < pathToFollow.Count - 1)
            {
                if (EventManager.OnEnemyReachedNode != null)
                {
                    EventManager.OnEnemyReachedNode(transform.name, transform, pathGO.name, i);
                }
                i++;
                goingTo = pathToFollow[i];
               

            }
        }

        rigid.velocity = (goingTo.transform.position - transform.position).normalized * speed;



    }
}
