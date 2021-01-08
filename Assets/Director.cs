using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Director : MonoBehaviour
{
    public GameObject Avator;
    public Text judgeText;

    public int head = 0;
    public int last;

    public Vector3 Hips;
    public Vector3[] HipsStore;

    public Vector3 Spine;
    public Vector3[] SpineStore;

    public Vector3 Spine1;
    public Vector3[] Spine1Store;

    public Vector3 Spine2;
    public Vector3[] Spine2Store;

    public Vector3 RightArm;
    public Vector3[] RightArmStore;

    public Vector3 RightForeArm;
    public Vector3[] RightForeArmStore;

    public Vector3 LeftArm;
    public Vector3[] LeftArmStore; 
    
    public Vector3 LeftForeArm;
    public Vector3[] LeftForeArmStore;

    public Transform child;

    //static Vector3[] Bones = new Vector3[8];

    //public List[] StoreRotation = new Vector3[8];


    // Start is called before the first frame update
    void Start()
    {
        
        Avator = GameObject.Find("Dancer");
        StartCoroutine("store");
        StartCoroutine("cut");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator store()
    {

        Debug.Log("store");
        yield return new WaitForSeconds(13.8f);  //14.9秒待つ
        //Debug.Log("14.9秒経ちました");

        for (; ; ) // 無限ループ。一定の条件で抜けたい場合は代わりにwhileを使う
        {
            yield return new WaitForSeconds(0.0111f);
            Array.Resize(ref HipsStore, HipsStore.Length + 1);
            HipsStore[HipsStore.Length - 1] = Hips;
            Array.Resize(ref SpineStore, SpineStore.Length + 1);
            SpineStore[SpineStore.Length - 1] = Spine;
            Array.Resize(ref Spine1Store, Spine1Store.Length + 1);
            Spine1Store[Spine1Store.Length - 1] = Spine1;
            Array.Resize(ref Spine2Store, Spine2Store.Length + 1);
            Spine2Store[Spine2Store.Length - 1] = Spine2;
            Array.Resize(ref RightArmStore, RightArmStore.Length + 1);
            RightArmStore[RightArmStore.Length - 1] = RightArm;
            Array.Resize(ref RightForeArmStore, RightForeArmStore.Length + 1);
            RightArmStore[RightArmStore.Length - 1] = RightForeArm;
            Array.Resize(ref LeftArmStore, LeftArmStore.Length + 1);
            LeftArmStore[LeftArmStore.Length - 1] = LeftArm;
            Array.Resize(ref LeftForeArmStore, LeftForeArmStore.Length + 1);
            LeftForeArmStore[LeftForeArmStore.Length - 1] = LeftForeArm;

            Debug.Log("ChildWorld:" + child.position + "|" + child.eulerAngles + "|" + child.lossyScale);

            /*
            for (int i = 0; i < Bones.Length; i++)
            {
                Array.Resize(ref StoreRotation[i], StoreRotation[i].Length + 1);
                StoreRotation[i][StoreRotation[i].Length - 1] = Bones[i];
            }
            */

        }

    }

    IEnumerator cut()
    {

        Debug.Log("cut");
        yield return new WaitForSeconds(13.8f);  //14.9秒待つ
        //Debug.Log("14.9秒経ちました");

        for (; ; ) // 無限ループ。一定の条件で抜けたい場合は代わりにwhileを使う
        {
            yield return new WaitForSeconds(1.1111f);
            last = HipsStore.Length-1;
            Vector3[] HipsList = new Vector3[last - head];
            Vector3[] SpineList = new Vector3[last - head];
            Vector3[] Spine1List = new Vector3[last - head];
            Vector3[] Spine2List = new Vector3[last - head];
            Vector3[] RightArmList = new Vector3[last - head];
            Vector3[] RightForeArmList = new Vector3[last - head];
            Vector3[] LeftArmList = new Vector3[last - head];
            Vector3[] LeftForeArmList = new Vector3[last - head];

            Array.Copy(HipsStore, head, HipsList, 0, last - head);
            Array.Copy(SpineStore, head, SpineList, 0, last - head);
            Array.Copy(Spine1Store, head, Spine1List, 0, last - head);
            Array.Copy(Spine2Store, head, Spine2List, 0, last - head);
            Array.Copy(RightArmStore, head, RightArmList, 0, last - head);
            Array.Copy(RightForeArmStore, head, RightForeArmList, 0, last - head);
            Array.Copy(LeftArmStore, head, LeftArmList, 0, last - head);
            Array.Copy(LeftForeArmStore, head, LeftForeArmList, 0, last - head);
            int judge = Treatment(RightArmList);
            judgeText.text = string.Format("{0}frame", judge);
            for (int i = 0; i < RightArmList.Length; i++)
            {
                //Debug.Log(RightArmList[i]);
            }
            //Debug.Log("ChildWorld:" + child.position + "|" + child.eulerAngles + "|" + child.lossyScale);

            head = last + 1;
            /*
            Vector3[][] MotionList = new Vector3[8][last-head];

            //last = HipsRoutine.Length - 1;
            //Vector3[] HipsList = new Vector3[last-head];
            //指定された範囲をコピーする 
            Array.Copy(HipsStore, head, MotionList[0], 0, last-head);
            head = last + 1;
            Debug.Log(MotionList[0].Length);
            */
        }

    }

    //NeuronTransformInstance からリアルタイムの各BoneのRotationを取得
    public void JustRotaion(Vector3 rot,int i)
    {

        if( i == 0)
        {
            Hips = rot;
            //Bones[0] = rot;
        }
        else if( i == 7)
        {
            Spine = rot;
            //Bones[1] = rot;
        }
        else if (i == 8)
        {
            Spine1 = rot;
            //Bones[2] = rot;
        }
        else if (i == 9)
        {
            Spine2 = rot;
            //Bones[3] = rot;
        }
        else if (i == 14)
        {
            RightArm = rot;
            //Bones[4] = rot;
        }
        else if (i == 15)
        {
            RightForeArm = rot;
            //Bones[5] = rot;
        }
        else if (i == 37)
        {
            LeftArm = rot;
            //Bones[6] = rot;
        }
        else if (i == 38)
        {
            LeftForeArm = rot;
            //Bones[7] = rot;
        }
    }

    public int Treatment(Vector3[] rotStore)
    {
        int frame_count = 0;
        for (int frame=0; frame < rotStore.Length; frame++)
        {
            if (frame != 0)
            {
                if(rotStore[frame].x- rotStore[frame-1].x > 70)
                {
                    rotStore[frame].x = 180 - rotStore[frame].x;
                }
                else if(rotStore[frame].x - rotStore[frame - 1].x < -70)
                {
                    rotStore[frame].x = 180 + rotStore[frame].x;
                }

                if (rotStore[frame].y - rotStore[frame - 1].y > 70)
                {
                    rotStore[frame].y = 180 - rotStore[frame].y;
                }
                else if (rotStore[frame].y - rotStore[frame - 1].y < -70)
                {
                    rotStore[frame].y = 180 + rotStore[frame].y;
                }

                if (rotStore[frame].z - rotStore[frame - 1].z > 70)
                {
                    rotStore[frame].z = 180 - rotStore[frame].z;
                }
                else if (rotStore[frame].z - rotStore[frame - 1].z < -70)
                {
                    rotStore[frame].z = 180 + rotStore[frame].z;
                }
                if (Math.Abs(rotStore[frame].z - rotStore[frame - 1].z) < 0.5)
                {
                    frame_count++;
                }
            }
        }

        return frame_count;

    }

    public void JustPosition(Vector3 srcP, int i)
    {
        if (i ==0)
        {
           // Debug.Log(srcP);
        }
    }


    double[,] MatrixTimesMatrix(double[,] A, double[,] B)
    {

        double[,] product = new double[A.GetLength(0), B.GetLength(1)];

        for (int i = 0; i < A.GetLength(0); i++)
        {
            for (int j = 0; j < B.GetLength(1); j++)
            {
                for (int k = 0; k < A.GetLength(1); k++)
                {
                    product[i, j] += A[i, k] * B[k, j];
                }
            }
        }

        return product;

    }
}
