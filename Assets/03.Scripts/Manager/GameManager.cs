using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> // 플레이어에게 필요한 변수들
{
    public List<string> testDataA = new List<string>();
    public List<int> testDataB = new List<int>();

    public int playerGold;
    public int playerPower;

}