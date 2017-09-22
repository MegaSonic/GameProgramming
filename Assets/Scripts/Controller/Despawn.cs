using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (EventManager.OnEnemyDespawned != null)
            {
                EventManager.OnEnemyDespawned(other.gameObject.name, other.transform);
            }
            Destroy(other.gameObject);

            
        }
    }
}
