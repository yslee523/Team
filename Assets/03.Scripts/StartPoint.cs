using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class StartPoint : MonoBehaviour
    {
        private ThirdPersonCharacter thePlayer;
        public string startPoint;
        public string linkScene;
        
        void Awake()
        {
            thePlayer = FindObjectOfType<ThirdPersonCharacter>();
        }

        void Start()
        {
            if (startPoint == thePlayer.currentMapName)
            {
                if(linkScene==thePlayer.checkScene)
                {
                    thePlayer.transform.position = this.transform.position;
                }  
            }
        }
    }
}


