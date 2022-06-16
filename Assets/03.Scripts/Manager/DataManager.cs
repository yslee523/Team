using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
public class DataManager : Singleton<DataManager> { // 여기서는 json 파일로 저장만 // 테이블 마다의 json 파일을 여기서 다 만들어주면 됨
    string path;

    public TextAsset jsonData = null;
    public string strJsonData = null;

    void Start() {
        path = Path.Combine(Application.dataPath + "/Resources/", "database.json");
        SimpleJsonLoad();
        //StartCoroutine(JsonSave());
    }

    public void JsonLoad() { // 웹서버에서 유니티 파일 로드 // 수정 필요

        SaveData saveData = new SaveData();

        if (!File.Exists(path)) { // 외부에서 JsonLoad를 실행했을 시 파일이 없다면 게임매니저의 player골드와 파워에 값을 저장하고 // JsonSave를 호출한다.
            GameManager.Instance.playerGold = 100;
            GameManager.Instance.playerPower = 4;
            JsonSave(); // JsonSave호출
        } else {
            string loadJson = File.ReadAllText(path); // 파일이 있다면 경로에 있는 모든 Text를 가져와서 loadJson 스트링에 담는다.
            saveData = JsonUtility.FromJson<SaveData>(loadJson); // JsonUtility를 통해 loadJson 파일을 SaveData클래스의 데이터 형식으로 변환한다.
                                                                // saveData의 내부에 있는 변수에 자동으로 저장이 됨

            if (saveData != null) {
                for (int i = 0; i < saveData.testDataA.Count; i++) {
                    GameManager.Instance.testDataA.Add(saveData.testDataA[i]); // 게임매니저의 Instance중 testDataA 에 방금 가져온 데이터를 넣어준다.
                }
                for (int i = 0; i < saveData.testDataB.Count; i++) {
                    GameManager.Instance.testDataB.Add(saveData.testDataB[i]); // 게임매니저의 Instance중 testDataB 에 방금 가져온 데이터를 넣어준다.
                }
                GameManager.Instance.playerGold = saveData.gold;  // saveData에 입력된 gold를 게임매니저의 gold에 넣어준다.
                GameManager.Instance.playerPower = saveData.power;// saveData에 입력된 power를 게임매니저의 power에 넣어준다. 
            }
        }
    }
    public void SimpleJsonLoad()
    {
        jsonData = Resources.Load<TextAsset>("user_info"); 
        strJsonData = jsonData.text;
        var json = JSON.Parse(strJsonData); //배열형태로 자동 파싱.

        string user_id = json["이름"].ToString();
        string user_nickname = json["닉네임"].ToString();
        string user_class = json["직업"].ToString();
        int user_level = json["능력치"]["레벨"].AsInt;
        int user_life = json["능력치"]["체력"].AsInt;
        int user_mana = json["능력치"]["마나"].AsInt;
        Debug.Log(user_id);
        Debug.Log(user_nickname);
        Debug.Log(user_class);

        for (int i=0; i<json["보유스킬"].Count; i++)
        {
            Debug.Log(json["보유스킬"][i].ToString());
        }
    }

    public IEnumerator JsonSave() {

        while(true)
        {
            SaveData saveData = new SaveData(); // 새로운 SaveData 객체를 만들어서

            for (int i = 0; i < 10; i++) {
                saveData.testDataA.Add("테스트 데이터 no " + i); // saveData의 testDataA에 다음과 같은 데이터를 넣어주고
            }

            for (int i = 0; i < 10; i++) {
                saveData.testDataB.Add(i); // saveData의 testDataA에 다음과 같은 데이터를 넣어준다.
            }

            saveData.gold = GameManager.Instance.playerGold;  // 현재 게임매니저의 플레이어 골드를 saveData의 골드에 넣어주고
            saveData.power = GameManager.Instance.playerPower;// 현재 게임매니저의 플레이어 파워를 saveData의 파워에 넣어준다. 

            string json = JsonUtility.ToJson(saveData, true); // 현재 세이브 데이터를 Json 형식으로 변환하고
            
            File.WriteAllText(path, json); // 경로에 다음 json 파일을 넣어준다.
            yield return new WaitForSeconds(5.0f);
        }
    }
}

[System.Serializable]
public class SaveData { // 플레이어에 필요한 변수를 저장하기위한 맞춤형 클래스
    public List<string> testDataA = new List<string>(); // 일단 변수 초기화
    public List<int> testDataB = new List<int>(); // 일단 변수 초기화

    public int gold;
    public int power;
}


[System.Serializable]
public class LoginData { // 플레이어에 필요한 변수를 저장하기위한 맞춤형 클래스
    public List<string> id = new List<string>(); // 일단 변수 초기화
    public List<string> pass = new List<string>(); // 일단 변수 초기화
}



// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.IO;
// public class DataManager : Singleton<DataManager> {
//     string path;

//     void Start() {
//         path = Path.Combine(Application.dataPath + "/Data/", "database.json");
//         JsonLoad();
//     }

//     public void JsonLoad() {
//         SaveData saveData = new SaveData();

//         if (!File.Exists(path)) { // 외부에서 JsonLoad를 실행했을 시 파일이 없다면 게임매니저의 player골드와 파워에 값을 저장하고 // JsonSave를 호출한다.
//             GameManager.Instance.playerGold = 100;
//             GameManager.Instance.playerPower = 4;
//             JsonSave(); // JsonSave호출
//         } else {
//             string loadJson = File.ReadAllText(path); // 파일이 있다면 경로에 있는 모든 Text를 가져와서 loadJson 스트링에 담는다.
//             saveData = JsonUtility.FromJson<SaveData>(loadJson); // JsonUtility를 통해 loadJson 파일을 SaveData클래스의 데이터 형식으로 변환한다.
//                                                                  // saveData의 내부에 있는 변수에 자동으로 저장이 됨

//             if (saveData != null) {
//                 for (int i = 0; i < saveData.testDataA.Count; i++) {
//                     GameManager.Instance.testDataA.Add(saveData.testDataA[i]); // 게임매니저의 Instance중 testDataA 에 방금 가져온 데이터를 넣어준다.
//                 }
//                 for (int i = 0; i < saveData.testDataB.Count; i++) {
//                     GameManager.Instance.testDataB.Add(saveData.testDataB[i]); // 게임매니저의 Instance중 testDataB 에 방금 가져온 데이터를 넣어준다.
//                 }
//                 GameManager.Instance.playerGold = saveData.gold;  // saveData에 입력된 gold를 게임매니저의 gold에 넣어준다.
//                 GameManager.Instance.playerPower = saveData.power;// saveData에 입력된 power를 게임매니저의 power에 넣어준다. 
//             }
//         }
//     }

//     public void JsonSave() {
//         SaveData saveData = new SaveData(); // 새로운 SaveData 객체를 만들어서

//         for (int i = 0; i < 10; i++) {
//             saveData.testDataA.Add("테스트 데이터 no " + i); // saveData의 testDataA에 다음과 같은 데이터를 넣어주고
//         }

//         for (int i = 0; i < 10; i++) {
//             saveData.testDataB.Add(i); // saveData의 testDataA에 다음과 같은 데이터를 넣어준다.
//         }

//         saveData.gold = GameManager.Instance.playerGold;  // 현재 게임매니저의 플레이어 골드를 saveData의 골드에 넣어주고
//         saveData.power = GameManager.Instance.playerPower;// 현재 게임매니저의 플레이어 파워를 saveData의 파워에 넣어준다. 

//         string json = JsonUtility.ToJson(saveData, true); // 현재 세이브 데이터를 Json 형식으로 변환하고
        
//         File.WriteAllText(path, json); // 경로에 다음 json 파일을 넣어준다.
//     }

// }

// [System.Serializable]
// public class SaveData { // 플레이어에 필요한 변수를 저장하기위한 맞춤형 클래스
//     public List<string> testDataA = new List<string>(); // 일단 변수 초기화
//     public List<int> testDataB = new List<int>(); // 일단 변수 초기화

//     public int gold;
//     public int power;

    
// }
