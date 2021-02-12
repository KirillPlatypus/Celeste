using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "CoinCounter", menuName = "CoinCounterObject", order = 0)]

public class CoinCounter : ScriptableObject 
{

    public int count;

    public int GetCoinNumber
    {
        get
        {
            return count;
        }
    }
    public void AddCoin(object sender, EventArgs e)
    {
        ++count;
    }
}
