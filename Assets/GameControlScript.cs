using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public NeuronTransformsInstance NI;

public class GameControlScript : MonoBehaviour
{
    // UI Text指定用
    public Text judgeText;
    // 表示する変数
    private int frame;

    private string popup;

    void Start()
    {
        frame = 0;
        StartCoroutine("sleep");
    }
    //「コルーチン」で呼び出すメソッド
    IEnumerator sleep()
    {

        Debug.Log("開始");
        yield return new WaitForSeconds(13.8f);  //14.9秒待つ
        Debug.Log("14.9秒経ちました");

        for (; ; ) // 無限ループ。一定の条件で抜けたい場合は代わりにwhileを使う
        {
            yield return new WaitForSeconds(1.1111f);

            if (frame%3 == 0) {
                popup = "Match the rhythm!!";
            }else if(frame % 3 == 1)
            {
                popup = "bad!!";
            }
            else if (frame % 3 == 2)
            {
                popup = "Nice!!!!";
            }
            judgeText.text = string.Format("{0:00000}{1}", frame,popup);
            frame++;
        }

    }
    /*
    //　経過時間
    private float nowTime;
    //loop時間
    private float routineTime;
    // UI Text指定用
    public Text TextFrame;
    // 表示する変数
    private int frame;

    void Start()
    {
        nowTime = 0f;
        routineTime = 0f;
        frame = 0;

    }

    void Update()
    {

        //　Time.deltaTimeを足す
        nowTime += Time.deltaTime;

        //　経過時間を表示
        //Debug.Log(nowTime);

        //　10秒を超えたら0に戻す
        if (nowTime >= 13.8f)
        {
            routineTime += Time.deltaTime;
            if (routineTime >= 1.1111f)
            {
                TextFrame.text = string.Format("{0:0000}", frame);
                routineTime = 0f;
                frame++;
            }
        }
    }
    */
}
