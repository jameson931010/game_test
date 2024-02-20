using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class mapCreate : MonoBehaviour
{
    [SerializeField] GameObject[] Blocks;
    [SerializeField] int Width;
    [SerializeField] int Height;
    

    public void render()
    {
        for(int i=0;i<Width;i++){
            for(int j=0;j<Height;j++){
                float x=i*0.4330177f,y=j*1.5f+(i&1)*0.75f;
                int r=Random.Range(0,Blocks.Length);
                GameObject block=Instantiate(Blocks[r],new Vector3(x,y,0),new Quaternion(0,0,0,0));
            }
        }
    }

    void Start()
    {
        render();
    }

}
