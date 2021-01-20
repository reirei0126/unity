using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
//using System.Windows.Forms;

public class WorldLocal : MonoBehaviour
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

    // Use this for initialization
    void Start()
	{
		StartCoroutine("store");

        StartCoroutine("cut");
        /*
        StartCoroutine("Hips");
        StartCoroutine("Spine2");
        StartCoroutine("RightArm");
        StartCoroutine("RightForeArm");
        StartCoroutine("LeftArm");
        StartCoroutine("LeftForeArm");
        */
        StartCoroutine("file");
        Bones = new Transform[6]{ Hips,Spine2,RightArm,RightForeArm,LeftArm,LeftForeArm};
        Texts = new Text[6] { Hipsjudge, Spine2judge, RightArmjudge, RightForeArmjudge, LeftArmjudge, LeftForeArmjudge };
        Store = new Vector3[6][];
        BoneNames = new string[6] { "腰", "上半身", "右腕", "右前腕", "左腕", "左前腕" };
        //double y = 1.11111111;
        //Judge.text = string.Format("{0:f4}\r\n{01:f4}", y, y);
        //Debug.Log(":" + Store[0][0].GetType());
        //Debug.Log(Store.GetType() + ":" + Store[0].GetType());
        for (int i=0; i < Bones.Length; i++)
        {
            Debug.Log(Bones[i].name + ":" + Bones[i].position);
        }
        /*
         * Debug.Log(RightArm.position.GetType() + ":" + Bones[4].position.GetType());
		//　ワールド空間でのデータ
		Debug.Log("ParentWorld:" + parent.position + "|" + parent.eulerAngles + "|" + parent.lossyScale);
		Debug.Log("ChildWorld:" + child.position + "|" + child.eulerAngles + "|" + child.lossyScale);

		//　ローカル空間でのデータ
		Debug.Log("ParentLocal:" + parent.localPosition + "|" + parent.localEulerAngles + "|" + parent.localScale);
		Debug.Log("ChildLocal:" + child.localPosition + "|" + child.localEulerAngles + "|" + child.localScale);
		*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator store()
    {

        Debug.Log("store");
        yield return new WaitForSeconds(14.9f);  //14.9秒待つ
        //Debug.Log("14.9秒経ちました");
        bool flag = false;
        for (; ; ) // 無限ループ。一定の条件で抜けたい場合は代わりにwhileを使う
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
               // Array.Resize(ref Store[i], Store[i].Length + 1);
               // Store[i][Store[i].Length - 1] = Bones[i].position;
            }
            //Array.Resize(ref Store, Store.Length + 1);
            //Store[Store.Length - 1] = Bones[4].position;
            //Debug.Log(Bones[4].position);

        }

    }
    
    IEnumerator cut()
    {

        Debug.Log("cut");
        yield return new WaitForSeconds(14.9f);  //14.9秒待つ
        //Debug.Log("14.9秒経ちました");

        //for (; ; ) // 無限ループ。一定の条件で抜けたい場合は代わりにwhileを使う
        while(Time.time<64.0f)
        {
            
            yield return new WaitForSeconds(1.111f);
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            float starttime = Time.time;
            last = Store[0].Length - 1;
            //Debug.Log(last);
            Vector3[][] Routine = new Vector3[Bones.Length][];
            float ave = 0;
            
            //Vector3[][] Routine = new Vector3[Bones.Length][last - head];
            for (int i = 0; i < Bones.Length; i++)
            {
                //Debug.Log(i);
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
                
                //default(上中級者)18/13/8
                //未経験者8//6/4
                if (maxstop > 8)
                {
                    Texts[j].text = string.Format("{0}:◎", BoneNames[j]);
                    Texts[j].color = new Color(0.88f, 0.66f, 0.0f, 1.0f);
                }
                else if (maxstop > 6)
                {
                    Texts[j].text = string.Format("{0}:●", BoneNames[j]);
                    Texts[j].color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
                }
                else if (maxstop > 4)
                {
                    Texts[j].text = string.Format("{0}:▲", BoneNames[j]);
                    Texts[j].color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                }
                else
                {
                    Texts[j].text = string.Format("{0}:✖", BoneNames[j]);
                    Texts[j].color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                }
               
                
                //Texts[j].text = string.Format("{0}:{1}frame/{2}", Bones[j].name,maxstop, last - head);
            }
            ave = ave / (float)Bones.Length;
            
            if (ave > 8)
            {
                Judge.text = string.Format("Perfect!!{0}",count);
                Judge.color = new Color(0.88f, 0.66f, 0.0f, 1.0f);
            }
            else if(ave > 6)
            {
                Judge.text = string.Format("Greatt!!{0}", count);
                Judge.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
            }
            else if (ave > 4)
            {
                Judge.text = string.Format("Nice!!{0}", count);
                Judge.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            }
            else
            {
                Judge.text = string.Format("もっと長く!!", count);
                Judge.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            }
                      //Judge.text = string.Format("{0}frame stop", ave);
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
        double x =(Stoptime * 0.011) / (double)count;
        Debug.Log(x.GetType());
        Judge.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        Judge.text = string.Format("平均ストップ時間\r\n{0:f4}s", x);

        /*
        Debug.Log("fileWrite");
        int j = 0;
        using (StreamWriter sw = new StreamWriter(@"C:\Users\Manabe_Lab\Documents\data\HipsDisp.txt", false, Encoding.UTF8))
        {
            for (int i = 1; i < Store[j].Length; i++)
            {
                sw.WriteLine((float)Math.Sqrt(Math.Pow((Store[j][i].x - Store[j][i - 1].x), 2)
                                            + Math.Pow((Store[j][i].y - Store[j][i - 1].y), 2)
                                            + Math.Pow((Store[j][i].z - Store[j][i - 1].z), 2)));
            }
            j += 1;
        }
        using (StreamWriter sw = new StreamWriter(@"C:\Users\Manabe_Lab\Documents\data\Spine2Disp.txt", false, Encoding.UTF8))
        {
            for (int i = 1; i < Store[j].Length; i++)
            {
                sw.WriteLine((float)Math.Sqrt(Math.Pow((Store[j][i].x - Store[j][i - 1].x), 2)
                                            + Math.Pow((Store[j][i].y - Store[j][i - 1].y), 2)
                                            + Math.Pow((Store[j][i].z - Store[j][i - 1].z), 2)));
            }
            j += 1;
        }
        using (StreamWriter sw= new StreamWriter(@"C:\Users\Manabe_Lab\Documents\data\RightArmDisp.txt", false, Encoding.UTF8))
        {

            for (int i = 1; i < Store[j].Length; i++)
            {
                sw.WriteLine((float)Math.Sqrt(Math.Pow((Store[j][i].x - Store[j][i - 1].x), 2)
                                            + Math.Pow((Store[j][i].y - Store[j][i - 1].y), 2)
                                            + Math.Pow((Store[j][i].z - Store[j][i - 1].z), 2)));
            }
            j += 1;
        }
        using (StreamWriter sw = new StreamWriter(@"C:\Users\Manabe_Lab\Documents\data\RightForeArmDisp.txt", false, Encoding.UTF8))
        {
            for (int i = 1; i < Store[j].Length; i++)
            {
                sw.WriteLine((float)Math.Sqrt(Math.Pow((Store[j][i].x - Store[j][i - 1].x), 2)
                                            + Math.Pow((Store[j][i].y - Store[j][i - 1].y), 2)
                                            + Math.Pow((Store[j][i].z - Store[j][i - 1].z), 2)));
            }
            j += 1;
        }
        using (StreamWriter sw = new StreamWriter(@"C:\Users\Manabe_Lab\Documents\data\LefhtArmDisp.txt", false, Encoding.UTF8))
        {

            for (int i = 1; i < Store[j].Length; i++)
            {
                sw.WriteLine((float)Math.Sqrt(Math.Pow((Store[j][i].x - Store[j][i - 1].x), 2)
                                            + Math.Pow((Store[j][i].y - Store[j][i - 1].y), 2)
                                            + Math.Pow((Store[j][i].z - Store[j][i - 1].z), 2)));
            }
            j += 1;
        }
        using (StreamWriter sw = new StreamWriter(@"C:\Users\Manabe_Lab\Documents\data\LeftForeArmDisp.txt", false, Encoding.UTF8))
        {
            for (int i = 1; i < Store[j].Length; i++)
            {
                sw.WriteLine((float)Math.Sqrt(Math.Pow((Store[j][i].x - Store[j][i - 1].x), 2)
                                            + Math.Pow((Store[j][i].y - Store[j][i - 1].y), 2)
                                            + Math.Pow((Store[j][i].z - Store[j][i - 1].z), 2)));
            }
            j += 1;
        }
        */
    }
}
/*
    IEnumerator Hips()
    {

        Debug.Log("Hipscut");
        yield return new WaitForSeconds(13.8f);  //14.9秒待つ

        for (; ; ) // 無限ループ。一定の条件で抜けたい場合は代わりにwhileを使う
        {
            yield return new WaitForSeconds(1.1111f);
            int judge = 0;
            int maxstop = 0;
            last = Store[0].Length - 1;
            Debug.Log(last);
            Vector3[][] Routine = new Vector3[Bones.Length][];
            //Vector3[][] Routine = new Vector3[Bones.Length][last - head];

            int j = 0;
            Debug.Log(j);
            Routine[i] = new Vector3[last - head];
            Array.Copy(Store[i], head, Routine[i], 0, last - head);
            

            for (int i = 1; i < Routine[2].Length; i++)
            {
                float disp = (float)Math.Sqrt(Math.Pow((Routine[2][i].x - Routine[2][i - 1].x), 2)
                                             + Math.Pow((Routine[2][i].y - Routine[2][i - 1].y), 2)
                                             + Math.Pow((Routine[2][i].z - Routine[2][i - 1].z), 2));
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

            RightArmjudge.text = string.Format("{0}frame/{1}", maxstop, last - head);
            head = last + 1;
        }

    }*/
