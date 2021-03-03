using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
public class StopJudgeClone : MonoBehaviour
{

    int head = 0;
    int last;

    public Text Judge;
    public Text Hipsjudge;
    public Text Spine2judge;
    public Text RightArmjudge;
    public Text RightForeArmjudge;
    public Text LeftArmjudge;
    public Text LeftForeArmjudge;

    public Transform Hips;
    public Transform Spine2;
    public Transform RightArm;
    public Transform RightForeArm;
    public Transform LeftArm;
    public Transform LeftForeArm;

    Transform[] Bones;
    Text[] Texts;
    string[] BoneNames;
    public Vector3[][] Store;
    int count = 0;
    double Stoptime = 0;

    void Start()
    {
        StartCoroutine("store");

        StartCoroutine("cut");

        StartCoroutine("file");
        Bones = new Transform[6] { Hips, Spine2, RightArm, RightForeArm, LeftArm, LeftForeArm };
        Texts = new Text[6] { Hipsjudge, Spine2judge, RightArmjudge, RightForeArmjudge, LeftArmjudge, LeftForeArmjudge };
        Store = new Vector3[6][];
        BoneNames = new string[6] { "腰", "上半身", "右腕", "右前腕", "左腕", "左前腕" };

        for (int i = 0; i < Bones.Length; i++)
        {
            Debug.Log(Bones[i].name + ":" + Bones[i].position);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator store()
    {

        Debug.Log("store");
        yield return new WaitForSeconds(14.9f);
        bool flag = false;
        for (; ; )
        {
            yield return new WaitForSeconds(0.011f);

            for (int i = 0; i < Bones.Length; i++)
            {
                if (flag)
                {
                    Array.Resize(ref Store[i], Store[i].Length + 1);
                    Store[i][Store[i].Length - 1] = Bones[i].position;
                }
                else
                {
                    Store[i] = new Vector3[1] { Bones[i].position };
                    if (i == Bones.Length - 1)
                    {
                        flag = !flag;
                    }
                }
            }
        }

    }

    IEnumerator cut()
    {

        Debug.Log("cut");
        yield return new WaitForSeconds(14.9f);
        while (Time.time < 64.0f)
        {

            yield return new WaitForSeconds(1.111f);
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            float starttime = Time.time;
            last = Store[0].Length - 1;
            Vector3[][] Routine = new Vector3[Bones.Length][];
            float ave = 0;
            for (int i = 0; i < Bones.Length; i++)
            {
                Routine[i] = new Vector3[last - head];
                Array.Copy(Store[i], head, Routine[i], 0, last - head);
            }

            for (int j = 0; j < Bones.Length; j++)
            {
                int judge = 0;
                int maxstop = 0;
                for (int i = 1; i < Routine[j].Length; i++)
                {
                    float disp = (float)Math.Sqrt(Math.Pow((Routine[j][i].x - Routine[j][i - 1].x), 2)
                                                 + Math.Pow((Routine[j][i].y - Routine[j][i - 1].y), 2)
                                                 + Math.Pow((Routine[j][i].z - Routine[j][i - 1].z), 2));
                    if (disp < 0.3)
                    {
                        judge += 1;
                    }
                    else
                    {
                        if (judge > maxstop)
                        {
                            maxstop = judge - 1;
                        }
                        judge = 0;
                    }
                }
                ave += maxstop;

                if (maxstop > 13)
                {
                    Texts[j].text = string.Format("{0}:◎", BoneNames[j]);
                    Texts[j].color = new Color(0.88f, 0.66f, 0.0f, 1.0f);
                }
                else if (maxstop > 10)
                {
                    Texts[j].text = string.Format("{0}:●", BoneNames[j]);
                    Texts[j].color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
                }
                else if (maxstop > 7)
                {
                    Texts[j].text = string.Format("{0}:▲", BoneNames[j]);
                    Texts[j].color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                }
                else
                {
                    Texts[j].text = string.Format("{0}:✖", BoneNames[j]);
                    Texts[j].color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                }
            }
            ave = ave / (float)Bones.Length;

            if (ave > 13)
            {
                Judge.text = string.Format("Perfect!!{0}", count);
                Judge.color = new Color(0.88f, 0.66f, 0.0f, 1.0f);
            }
            else if (ave > 10)
            {
                Judge.text = string.Format("Greatt!!{0}", count);
                Judge.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
            }
            else if (ave > 7)
            {
                Judge.text = string.Format("Nice!!{0}", count);
                Judge.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            }
            else
            {
                Judge.text = string.Format("もっと長く!!", count);
                Judge.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            }
            head = last + 1;
            count += 1;
            Stoptime += ave;
            sw.Stop();
            Debug.Log(sw.ElapsedMilliseconds + "ms");
        }

    }


    IEnumerator file()
    {

        Debug.Log("file");
        yield return new WaitForSeconds(66.0f);
        double x = (Stoptime * 0.011) / (double)count;
        Debug.Log(x.GetType());
        Judge.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        Judge.text = string.Format("平均ストップ時間\r\n{0:f4}s", x);
    }


}
