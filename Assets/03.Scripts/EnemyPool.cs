using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    //Enemy 프리팹
    public GameObject enemy1Pre;
    public GameObject enemy2Pre;
    public GameObject enemy3Pre;

    //랜덤 캐릭을 찾기위한 변수
    private int randomCount = 0;
    //Enemy activeSelf=true 마릿수를 체크
    private int activeCount = 0;
    //스폰 Pos위치
    private Transform[] spawnPoints;
    //오브젝트 풀을 위한 GameObject 배열
    private GameObject[] gameObjects = new GameObject[18];


    [Header("Max Enemy 최대 18마리")]
    public int maxEnemy;

    void Awake()
    {
        //EnemyPool 아래 있는 Pos 위치 찾기
        spawnPoints = GameObject.Find("EnemyPool").GetComponentsInChildren<Transform>();
    }

    void Start()
    {
        //초기화
        gameObjects[0] = Instantiate(enemy1Pre, spawnPoints[1]);
        gameObjects[1] = Instantiate(enemy2Pre, spawnPoints[1]);
        gameObjects[2] = Instantiate(enemy3Pre, spawnPoints[1]);

        gameObjects[3] = Instantiate(enemy1Pre, spawnPoints[2]);
        gameObjects[4] = Instantiate(enemy2Pre, spawnPoints[2]);
        gameObjects[5] = Instantiate(enemy3Pre, spawnPoints[2]);

        gameObjects[6] = Instantiate(enemy1Pre, spawnPoints[3]);
        gameObjects[7] = Instantiate(enemy2Pre, spawnPoints[3]);
        gameObjects[8] = Instantiate(enemy3Pre, spawnPoints[3]);

        gameObjects[9] = Instantiate(enemy1Pre, spawnPoints[4]);
        gameObjects[10] = Instantiate(enemy2Pre, spawnPoints[4]);
        gameObjects[11] = Instantiate(enemy3Pre, spawnPoints[4]);

        gameObjects[12] = Instantiate(enemy1Pre, spawnPoints[5]);
        gameObjects[13] = Instantiate(enemy2Pre, spawnPoints[5]);
        gameObjects[14] = Instantiate(enemy3Pre, spawnPoints[5]);

        gameObjects[15] = Instantiate(enemy1Pre, spawnPoints[6]);
        gameObjects[16] = Instantiate(enemy2Pre, spawnPoints[6]);
        gameObjects[17] = Instantiate(enemy3Pre, spawnPoints[6]);

        //전체 SetActive(false)로 시작
        for (int i = 0; i < 18; i++)
        {
            gameObjects[i].SetActive(false);
        }

        StartCoroutine("SpawnEnemy");
    }

    void Update()
    {
        //0~17 총 18개
        randomCount = Random.Range(0, 18);
    }

    IEnumerator SpawnEnemy()
    {
        //1초 마다 생성
        yield return new WaitForSeconds(1.0f);

        //전체 
        for (int i = 0; i < gameObjects.Length; i++)
        {
            //activeSelf 확인후 activeCount에 카운트
            if (gameObjects[i].activeSelf == true)
            {
                activeCount++;
            }
            //Enemy SetActive(false) 이후 위치 다시 초기화
            if (gameObjects[i].activeSelf == false)
            {
                if (i >= 0 && i < 3)
                {
                    gameObjects[i].transform.position=spawnPoints[1].position;
                }
                else if (i >= 3 && i < 6)
                {
                    gameObjects[i].transform.position=spawnPoints[2].position;
                }
                else if (i >= 6 && i < 9)
                {
                    gameObjects[i].transform.position=spawnPoints[3].position;
                }
                else if (i >= 9 && i < 12)
                {
                    gameObjects[i].transform.position=spawnPoints[4].position;
                }
                else if (i >= 12 && i < 15)
                {
                    gameObjects[i].transform.position=spawnPoints[5].position;
                }
                else
                {
                    gameObjects[i].transform.position=spawnPoints[6].position;
                }
            }
        }
        //최대 마릿수 제한
        if (activeCount < maxEnemy)
        {
            gameObjects[randomCount].SetActive(true);
        }
        //0으로 초기화
        activeCount = 0;

        StartCoroutine("SpawnEnemy");
    }
}
