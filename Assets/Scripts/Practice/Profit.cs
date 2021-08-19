using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profit : MonoBehaviour
{
    public List<int> profits = new List<int>();
    public int n = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        ConsecProfit();
    }

    private void ConsecProfit()
    {
        int numMonths = 0;
        int consecProfit = 1;
        
        // check current mont vs next month
        for (int i = 0; i < profits.Count - 1; i++)
        {
            // if next month is greater than this month
                // consecProfit += 1
                // if consecProfit == n
                    // numMonths += 1;
                    // consecProfit = 0;
            // else
                // consecProfit = 0;
            if (profits[i + 1] > profits[i])
            {
                consecProfit += 1;
                if (consecProfit == n)
                {
                    numMonths += 1;
                    consecProfit = 1;
                }
            }
            else
            {
                consecProfit = 1;
            }
            
        }
                

        // Debug.Log(numMonths);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
