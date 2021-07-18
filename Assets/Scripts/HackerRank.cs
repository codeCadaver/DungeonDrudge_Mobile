using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerRank : MonoBehaviour
{
    public int pairs = 0;

    public List<int> socks;

    public List<int> paired;
    // Start is called before the first frame update
    void Start()
    {
        socks = new List<int>()
        {
            10, 20, 20, 10, 10, 30, 50, 10, 20  
            
        };
        
        CheckSocks(socks);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckSocks(List<int> socks)
    {
        if (socks.Count < 2)
        {
            pairs = 0;
            return;
        }
        
        

        foreach (int item in socks)
        {
            Debug.Log(item);
        }
    }
}
