using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnemyPath : MonoBehaviour {

    public List<GameObject> nodes;


	// Use this for initialization
	void Start () {
		if (nodes.Count < 1)
        {
            Debug.LogWarning("No nodes in Path " + gameObject.name);
        }

        
	}
	

	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
        DrawLines();
#endif
    }

    private void DrawLines()
    {
        if (nodes.Count >= 1)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (i < nodes.Count - 1 && nodes[i] != null && nodes[i+1] != null)
                {
                    Debug.DrawLine(nodes[i].transform.position, nodes[i + 1].transform.position, Color.white);
                }
            }
        }
    }
}
