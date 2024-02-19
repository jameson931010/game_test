using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapCreate : MonoBehaviour
{
    [SerializeField] GameObject[] Blocks;

    public void render()
    {
        int r=Random.Range(0,Blocks.Length);
        GameObject block=Instantiate(Blocks[r],new Vector3(0,0,0),new Quaternion(0,0,0,0));
    }

    void Start()
    {
        render();
    }

}
