using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneMoveButton : MonoBehaviour
{
    // private SceneManager2 sceneManager;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>(); // UI 사용해야 켜짐
        button.onClick.AddListener(()=> {SceneManager2.Instance.OnSceneButtonClicked();});
    }

    // Update is called once per frame
    void Update()
    {

    }
}
