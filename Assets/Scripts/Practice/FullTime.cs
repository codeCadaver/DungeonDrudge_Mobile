using System.Collections;
using System.Collections.Generic;
using Practice;
using UnityEngine;

public class FullTime : Employee
{
    public float salary;
    public override float CalculateMonthlySalary()
    {
        return salary /= 12;
    }
}
