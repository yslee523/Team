using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour // 클래스 템플릿 // MonoBehaviour 상속 규제
{
    private static T _instance = null; // T타입의 인스턴스 // m_Singleton1을 T에 대입하면 객체 생성이 완료된다.
    private static GameObject _singletonGameObject;
    public static T Instance
    {
        get
        {
            if(_instance != null)
            return _instance;
            _instance = SingletonGameObject.GetComponent<T>(); // MSingleton1 스크립트가 있으면 스크립트를 가져오고 없으면 null을 반환
            if(_instance != null)                               // 인스턴스가 null이면 패스
            return _instance;                                       
            _instance = SingletonGameObject.AddComponent<T>(); // GameObject에 T 스크립트를 추가한 후 그 MSingleton1 스크립트를 _instace에 대입한다. // 
            DontDestroyOnLoad(_instance);                       // _instance 스크립트를 삭제하지 않는다.

            return _instance;
        }
    }
    //GameObject AAA = MonoSingleton2<MSingleton1>.SingletonGameObject;
    //GameObject BBB = MonoSingleton2<MSingleton1>.SingletonGameObject;
    private static GameObject SingletonGameObject // SingletonGameObject. 을 하는 순간 프로퍼티의 get이 호출되며 _singletonGameObject가 초기화된다.
    {
        get
        {
            if(!_singletonGameObject)
            {
                _singletonGameObject = new GameObject("Singleton Script"); // 싱글톤 스크립트라는 이름의 빈게임 오브젝트 생성
            }
            return _singletonGameObject;    // _singletonGameObject 반환
        }
        set{ _singletonGameObject = value;}
    }

    public void Test1()
    {
        Debug.Log(123);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
