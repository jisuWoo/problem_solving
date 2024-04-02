using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genetor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int start = 1;
        int end = 5000;

        int selfNumberSum = CalculateSelfNumberSum(start, end);
         Debug.Log($"¼ýÀÚ ÇÕ : {selfNumberSum}");
    }

    // Update is called once per frame
    void Update()
    {

    }

    static int CalculateSelfNumberSum(int start, int end)
    {
        int sum = 0;
        List<int> selfNumbers = new List<int>();

        for (int i = start; i <= end; i++)
        {
            int generatedNumber = i + CalculateSumOfDigits(i);
            if (generatedNumber <= end)
            {
                selfNumbers.Add(generatedNumber);
            }
        }

        for (int i = start; i <= end; i++)
        {
            if (!selfNumbers.Contains(i))
            {
                sum += i;
            }
        }

        return sum;
    }
    static int CalculateSumOfDigits(int number)
    {
        int sum = 0;
        while (number > 0)
        {
            sum += number % 10;
            number /= 10;
        }
        return sum;
    }

}
