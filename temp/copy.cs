using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copy : MonoBehaviour
{

    //coyp object;
    public int _x;
    public int _z;
    public int Vector_Y;

    public GameObject target;

    // Use this for initialization
    void Start()
    {
        CreateGameBoard(_x, _z);
    }

    private void CreateGameBoard(int cols, int rows)
    {
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject newBlock = (GameObject)Instantiate(target, new Vector3(i, 2, j), Quaternion.identity);
                newBlock.name = "Block: " + i + "," + j;
            }
        }
    }

}
