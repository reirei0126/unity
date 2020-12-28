using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public NeuronTransformsInstance NI;

public class GameControlScript : MonoBehaviour
{
    // UI Text�w��p
    public Text judgeText;
    // �\������ϐ�
    private int frame;

    private string popup;

    void Start()
    {
        frame = 0;
        StartCoroutine("sleep");
    }
    //�u�R���[�`���v�ŌĂяo�����\�b�h
    IEnumerator sleep()
    {

        Debug.Log("�J�n");
        yield return new WaitForSeconds(13.8f);  //14.9�b�҂�
        Debug.Log("14.9�b�o���܂���");

        for (; ; ) // �������[�v�B���̏����Ŕ��������ꍇ�͑����while���g��
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
    //�@�o�ߎ���
    private float nowTime;
    //loop����
    private float routineTime;
    // UI Text�w��p
    public Text TextFrame;
    // �\������ϐ�
    private int frame;

    void Start()
    {
        nowTime = 0f;
        routineTime = 0f;
        frame = 0;

    }

    void Update()
    {

        //�@Time.deltaTime�𑫂�
        nowTime += Time.deltaTime;

        //�@�o�ߎ��Ԃ�\��
        //Debug.Log(nowTime);

        //�@10�b�𒴂�����0�ɖ߂�
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
