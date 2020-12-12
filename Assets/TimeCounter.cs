using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
float second;//時間計測をするための変数
void Start()
{
    Invoke("Counts",15.1f);
}

/*
void Update()
{
second += Time.deltaTime;//時間をカウント
if (second >= 0.5555)//もし1秒たったら
{
//一秒経過するとカウントした時間をリセット
second = 0;
//ログに、1秒経過しました！と表示させる
Debug.Log("1拍経過しました!");
}
}
*/
void Counts()
{
second += Time.deltaTime;//時間をカウント
if (second >= 0.5555)//もし1秒たったら
{
//一秒経過するとカウントした時間をリセット
second = 0;
//ログに、1秒経過しました！と表示させる
Debug.Log("1拍経過しました!");
}
}
}