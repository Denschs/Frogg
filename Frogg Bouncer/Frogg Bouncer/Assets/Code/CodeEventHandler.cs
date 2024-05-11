using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeEventHandler : MonoBehaviour
{
    public static event Action LosingLife;
    public static void Trigger_LosingLife() { LosingLife.Invoke(); }

    public static event Action GettingPointsRaw;
    public static void Trigger_GettingPointsRaw() { GettingPointsRaw.Invoke(); }

    public static event Action<int> GettingPoints;
    public static void Trigger_GettingPoints(int newpoints) { GettingPoints.Invoke(newpoints); }

    public static event Action<int> FairyCounterChanged;
    public static void Trigger_FairyCounterChanged(int newCounter) { FairyCounterChanged?.Invoke(newCounter); }

    public static event Action ElectricDebufStarter;
    public static void Trigger_ElectricDebufStarter() { ElectricDebufStarter.Invoke(); }

    public static event Action IceDebuffStarter;
    public static void Trigger_IceDebuffStarter() { IceDebuffStarter.Invoke(); }

    public static event Action FireDebuffStarter;
    public static void Trigger_FireDebuffStarter() { FireDebuffStarter.Invoke(); }
}
