using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyManager : MonoBehaviour
{
    public BigNumber monkeyDefaultPrice { get; set; }
    public float monkeyPriceMultiple { get; set; } = 1.15f;
    public float monkeyTypingSpeed = 3f;
    public int monkeyRestCycle = 5;
    public float monkeyRestTime = 7f;

    public BigNumber[] SucceseGetGoldArray = { 
        new BigNumber(100, 0), 
        new BigNumber(200, 0),
        new BigNumber(300, 0),
        new BigNumber(400, 0),
        new BigNumber(500, 0),
        new BigNumber(600, 0),
        new BigNumber(700, 0),
        new BigNumber(800, 0),
        new BigNumber(900, 0),
        new BigNumber(1000, 0),
    };

    public float goldGetMultiple = 1.5f;


    void Start()
    {
        monkeyDefaultPrice = new BigNumber(100, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
