using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (EventManager.OnEnemyDestroyed != null)
            {
                EventManager.OnEnemyDestroyed(other.gameObject.name, other.transform);
            }
            Destroy(other.gameObject);

            
        }
    }
}
