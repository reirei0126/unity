using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neuron;

namespace Neuron
{
    public class JudgeMove : MonoBehaviour
    {
        NeuronInstance Instance;
        NeuronActor actor;

        public JudgeMove()
        { 
            Instance = new NeuronInstance();
            actor = new NeuronActor();
        }

        // UI Text指定用
        //public Text judgeText;
        // Start is called before the first frame update

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 1; i < (int)NeuronBones.NumOfBones; ++i)
            {
                Vector3 rot =this.actor.GetReceivedRotation((NeuronBones)i);

                Debug.Log(rot.x+","+rot.y+"," + rot.z);

            }
        }
    }
}
