using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace UnityStandardAssets.Characters.ThirdPerson
{

    public class Portal : MonoBehaviour
    {
        public string transferMapName;
        private bool check = false;
        private bool startCheck = false;
        private ThirdPersonCharacter thePlayer;

        void Start()
        {
            StartCoroutine(StartCheck());
            StartCoroutine(CheckScene());
        }

        void Awake()
        {
            thePlayer = FindObjectOfType<ThirdPersonCharacter>();
        }

        void Update()
        {
            if (startCheck == true)
            {
                if (Input.GetKeyDown(KeyCode.G) && check == true)
                {
                    thePlayer.currentMapName = transferMapName;
                    SceneManager.LoadScene(transferMapName);
                }
            }
        }

        IEnumerator StartCheck()
        {
            yield return new WaitForSeconds(2f);
            startCheck = true;
        }

        IEnumerator CheckScene()
        {
            yield return new WaitForSeconds(0.5f);
            thePlayer.checkScene = SceneManager.GetActiveScene().name;
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag == "Player")
            {
                check = true;
            }
        }

        void OnTriggerExit(Collider col)
        {
            check = false;
        }
    }
}