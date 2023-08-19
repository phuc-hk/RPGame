using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGame.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;
        static bool hasSpawn = false;

        private void Awake()
        {
            if (hasSpawn) return;

            SpawnPersistentObject();

            hasSpawn = true;
        }

        private void SpawnPersistentObject()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }

}

