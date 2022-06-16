using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class SetUserLoginData: MonoBehaviour {

    public InputField id; // 아이디
    public InputField pass; // 패스워드
    public InputField passRe; // 패스워드 재입력
    public Text Msg;

    private string memberSaveURL = "http://192.168.0.25:8080/Set_User_Login_Data.json.php";

    void Start()
    {
        //
        //id = GameObject.Find("Id").GetComponent<InputField>();
        //pass = GameObject.Find("Pass").GetComponent<InputField>();
        //passRe = GameObject.Find("PassRe").GetComponent<InputField>();
        //Msg = GameObject.Find("Msg").GetComponent<Text>();

    }
    
    public void StartCoroutineSaveMember()
    {
        //if (id.text.Contains("@"))
        //{
        //Debug.Log("이메일");
        if (pass.text.Length != 0 && pass.text==passRe.text)
        {
            //Debug.Log("가입");
            StartCoroutine("SaveMember");
        }
        else
        {
            Msg.text = "비번을 확인해주세요.";
            //Debug.Log("비번을 확인해주세요..");
        }
        //}
        //else
        //{
        //    Msg.text = "이메일 형식이 틀립니다.";
        //    Debug.Log("형식이 틀립니다.");
        //}
        
    }

    public void StartLogin()
    {
        StartCoroutineSaveMember();
    }

    public IEnumerator SaveMember()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id.text);
        form.AddField("pass", pass.text);
        form.AddField("SecretCode", 123);
        var www = new WWW(memberSaveURL, form);

        yield return www;
        //오류검증.
        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.text); //웹페이지를 그대로 출력
            DisplaySaveResult(www.text); // 저장 결과를 출력한다.
        }
        else
        {
            Debug.Log("Error : " + www.error);
        }
    }

    void DisplaySaveResult(string strJsonData)
    {
        var jSon = JSON.Parse(strJsonData); // 웹에 띄워진 Json 문장을 나눠담아서

        //int returnCode = jSon["returnCode"].AsInt; // 리턴코드와 
        //string returnMsg = jSon["returnMsg"].ToString(); // 리턴메세지를 담아서
        //Msg.text = returnMsg; // Msg에 출력해준다.

        //Debug.Log("결과코드: " + returnCode);
        //Debug.Log("메세지: " + returnMsg);
    }

    public void Cancel()
    {
        // SceneManager.LoadScene("99.login"); // 취소하면 로그인 씬을 다시 불러들인다.
    }
}
