using System.Collections;
using System.Collections.Generic;
using Practice;
using UnityEngine;

public class PartTime : Employee
{
    public float hoursWorked;
    public float hourlyRate;
    
    public override float CalculateMonthlySalary()
    {
        return hoursWorked * hourlyRate;
    }
}
