using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class SetUserData : MonoBehaviour {

    public string user_name;
    public string user_email;
    public string user_subject;
    public string user_contents;

    private string boardSaveURL = "http://192.168.0.25:8080/Set_User_Data.json.php";
    private string SecretCode = "123";

    void Start ()
    {
        StartCoroutine("SaveBoard");
	}
	
    public IEnumerator SaveBoard()
    {
        WWWForm form = new WWWForm();
        form.AddField("SecretCode", SecretCode);
        form.AddField("name", user_name);
        form.AddField("email", user_email);
        form.AddField("subject", user_subject);
        form.AddField("contents", user_contents);
        

        var www = new WWW(boardSaveURL, form);
        yield return www;
        //Debug.Log(www.text);
        /*
        * 1. string.IsNullOrEmpty
        * - 지정된 문자열이 null이거나 Empty문자열인지 여부를 나타냄
        * 
        * 2. string.IsNullOrWhiteSpace
        * - 지정된 문자열이 null이거나 비어있거나 공백문자로만 구성되어 있는지 여부를 나타냄
        */

        //오류검증.
        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.text); //웹페이지를 그대로 출력
            DisplaySaveResult(www.text); //
        }
        else
        {
            Debug.Log("Error : " + www.error);
        }
    }

    void DisplaySaveResult(string strJsonData)
    {
        var jSon = JSON.Parse(strJsonData);
        Debug.Log(jSon);
        //int returnCode = jSon["returnCode"].AsInt; //성공.실패 확인용.
        Debug.Log(jSon["returnCode"].AsInt);
        //string returnMsg = jSon["returnMsg"].ToString();
        Debug.Log(jSon["returnMsg"].ToString());
        //Debug.Log("결과코드: " + returnCode);
        //Debug.Log("메세지: " + returnMsg);
    }
}
