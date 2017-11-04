using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceWrangler : Singleton<ServiceWrangler> {

    protected ServiceWrangler() { }


    [SerializeField]
    private GameObject loadManager, saveLoad, eventManager;

    private Dictionary<string, PrefabBool> singletonPrefabRegistry;
    private Dictionary<string, PrefabBool> SingletonPrefabRegistry
    {
        get
        {
            if (singletonPrefabRegistry == null)
            {
                singletonPrefabRegistry = new Dictionary<string, PrefabBool>()
                {
                    { typeof(LevelToLoad).ToString(),            new PrefabBool(ref loadManager) },
                    { typeof(SaveLoad).ToString(),               new PrefabBool(ref saveLoad) },
                    { typeof(EventManager).ToString(),           new PrefabBool(ref eventManager) }
                };
            }
            return singletonPrefabRegistry;
        }
    }


    private void OnEnable()
    {
        foreach (KeyValuePair<string, PrefabBool> singletonRegistered in SingletonPrefabRegistry)
        {
            if (!singletonRegistered.Value.isRegistered)
            {
                GameObject singletonGameObject = Instantiate(singletonRegistered.Value.prefab, gameObject.transform, true) as GameObject;
                singletonGameObject.transform.position = Vector3.zero;
                singletonGameObject.transform.rotation = Quaternion.identity;

                string debugMessage = singletonRegistered.Key + " required Loading. Place this prefab in your scene";
                Debug.LogFormat("<color=#ffff00>" + debugMessage + "</color>");
            }
        }
    }


    public void RegisterSingleton<T>(T singleton) where T : MonoBehaviour
    {
        string typeString = typeof(T).ToString();

        if (SingletonPrefabRegistry.ContainsKey(typeString) && !SingletonPrefabRegistry[typeString].isRegistered)
        {
            SingletonPrefabRegistry[typeString].isRegistered = true;
        }
        else if (typeString != (typeof(ServiceWrangler)).ToString())
        {
            string debugMessage = typeString + " attempting duplicate or unprepared service registry. Add this singleton to the singletonPrefabRegistry";
            Debug.LogFormat("<color=#ffff00>" + debugMessage + "</color>");
        }
    }

    class PrefabBool {
        public GameObject prefab;
        public bool isRegistered;
        public PrefabBool(ref GameObject prefab) {
            this.prefab = prefab;
        }
    }
}
