using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class GetUserLoginData : MonoBehaviour
{

    public InputField id;
    public InputField pass;
    public Text Msg;

    private string memberLoginURL = "http://192.168.0.25:8080/Get_User_Login_Data.json.php";

    void Start()
    {
        //StartCoroutine("SaveMember");
        //id = GameObject.Find("Id").GetComponent<InputField>();
        //pass = GameObject.Find("Pass").GetComponent<InputField>();
        //Msg = GameObject.Find("Msg").GetComponent<Text>();

    }

    public void StartCoroutineLoginMember()
    {
        if (pass.text.Length != 0)
        {
            StartCoroutine("LoginMember");
        }
        else
        {
            Msg.text = "비번을 확인해주세요.";
        }
    }

    public IEnumerator LoginMember()
    {
        WWWForm form = new WWWForm();
        //form.AddField("id", id.text);
        //form.AddField("pass", pass.text);
        form.AddField("SecretCode", 123);
        var www = new WWW(memberLoginURL, form);
        yield return www;

        //오류검증.
        if (string.IsNullOrEmpty(www.error))
        {
            //Debug.Log(www.text + "hihi"); //웹페이지를 그대로 출력
            DisplayLoginResult(www.text); //
        }
        else
        {
            // Debug.Log("Error : " + www.error);
        }
    }

    void DisplayLoginResult(string strJsonData)
    {
        // var jSon = JsonUtility.FromJson<LoginData>(strJsonData);
        // var jSon = JsonUtility.ToJson(strJsonData);
        var jSon = JSON.Parse(strJsonData);
        List<string> id1 = new List<string>();
        List<string> pass1 = new List<string>();
        if (jSon.Count != 0)
        {
            Debug.Log(jSon.Count);
            for(int i = 0; i<jSon.Count; i++)
            {
                id1.Add(jSon[i]["id"].ToString().Substring(1, jSon[i]["id"].ToString().Length-2));
                pass1.Add(jSon[i]["pass"].ToString().Substring(1, jSon[i]["pass"].ToString().Length-2));
            }
            //string regist_date = jSon[0]["regist_date"].ToString();
            for(int i = 0; i<jSon.Count; i++)   
            {
                if(id1[i] == id.text && pass1[i] == pass.text)
                {
                    // 데이터매니저에서 해당 정보를 활용해 데이터를 로드함
                    Debug.Log("로그인 성공");
                    return;
                }
            }
            //Debug.Log("가입일: " + regist_date);
            //SceneManager.LoadScene("99.Lobby");
        }
        else
        {
            Msg.text = "접속정보를 확인해 주세요.";
        }
        // for(int i = 0; i<jSon.id.Count ; i++)
        // {
        //     if(jSon.id[i] == id.text && jSon.pass[i] == pass.text)
        //     {
        //         {
        //             // 데이터매니저에서 해당 정보를 활용해 데이터를 로드함
        //             Debug.Log("로그인 성공");
        //             return;
        //         }
        //     }
        // }
    }

    public void MemberJoin()
    {
        SceneManager.LoadScene("99.Join");
    }
}
