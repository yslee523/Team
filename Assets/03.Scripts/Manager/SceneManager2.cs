using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneManager2 : Singleton<SceneManager2>
{
    Scene activeScene;
    private Color A;
    private Color B;
    int max = 0;
    void Awake(){
        // DontDestroyOnLoad (this.gameObject);
        StartCoroutine("SceneMove");
        Debug.Log("Awake 활성화");
        A = new Color(1,1,1,1);
        //A = new Color(Random.Range(0.0f, 1.0f),Random.Range(0.0f, 1.0f),Random.Range(0.0f, 1.0f),1);
        B = new Color(1,1,1,0);
       
    }
    IEnumerator SceneMove()
    {
        yield return new WaitForSeconds(5.0f);
        if(activeScene.name == "1. Logo")
        {
            // if else 문 추가
            SceneManager.LoadScene("2. Login");
        }
        else if(activeScene.name == "2. Login")
        {
            // if else 문 추가
            SceneManager.LoadScene("3. Lobby");
        }
        else if(activeScene.name == "3. Lobby")
        {
            // if else 문 추가
            SceneManager.LoadScene("4. CharacterSelect");
        }
        else if(activeScene.name == "4. CharacterSelect")
        {
            // if else 문 추가
            SceneManager.LoadScene("5. Billage");
        }
    }
    public void OnSceneButtonClicked()
    {
        StartCoroutine("SceneMove");
    }
    void Update()
    {
        if(activeScene.name == "1. Logo")
        {
            if( max == 0)
            {
                GameObject.FindGameObjectWithTag("LogoCanvas").GetComponent<Image>().color = Color.LerpUnclamped(GameObject.FindGameObjectWithTag("LogoCanvas").GetComponent<Image>().color, A , Time.deltaTime);
                if(GameObject.FindGameObjectWithTag("LogoCanvas").GetComponent<Image>().color.a > 0.92f)
                {
                    max = 1;
                }
            }
            else if (max == 1)
            {
                GameObject.FindGameObjectWithTag("LogoCanvas").GetComponent<Image>().color = Color.LerpUnclamped(GameObject.FindGameObjectWithTag("LogoCanvas").GetComponent<Image>().color, B , Time.deltaTime);
            }
        }
        activeScene = SceneManager.GetActiveScene();
    }
}
