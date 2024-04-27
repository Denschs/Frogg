using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeEventHandler : MonoBehaviour
{
    public static event Action LosingLife;
    public static void Trigger_LosingLife() { LosingLife.Invoke(); }

    public static event Action<int> GettingPoints;
    public static void Trigger_GettingPoints(int newpoints) { GettingPoints.Invoke(newpoints); }
}
