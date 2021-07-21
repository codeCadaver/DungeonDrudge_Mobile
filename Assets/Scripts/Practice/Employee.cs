using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Practice
{
    public abstract class Employee : MonoBehaviour
    {
        public static string companyName = "GameDevHQ";
        public string employeeName;

        public abstract float CalculateMonthlySalary();
    }
    
}
