using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGame.Saving;
using System;

namespace RPGame.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        static string defaultSaving = "save";
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaving);
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaving);
        }
    }

}

