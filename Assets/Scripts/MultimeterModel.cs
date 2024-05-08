using System;
using UnityEngine;
public enum MultimeterMode
{
    Off,
    Voltage,
    ACVoltage,
    Amperage,
    Resistance
}

public class MultimeterModel : MonoBehaviour
{
    
    public MultimeterMode CurrentMode { get; private set; } = MultimeterMode.Off;
    
    public float Power { get; private set; } = 0f;
    public float Resistance { get; private set; } = 0f;

    public void SwitchModeNext()
    {
        CurrentMode = (MultimeterMode)(((int)CurrentMode + 1) % 5);
    }

    public void SwitchModeBack()
    {
        CurrentMode = (MultimeterMode)(((int)CurrentMode - 1) % 5);
    }

    internal void SetMode(int modeIndex = 0)
    {
        CurrentMode = (MultimeterMode)modeIndex;
    }

    public float CalculateVoltage()
    {
        return (float)Math.Round(Power/CalculateAmperage(), 2);
    }

    public float CalculateAmperage()
    {
        return (float)Math.Round(Math.Sqrt(Power/Resistance), 2);
    }

    public void SetValues(float power, float resistance)
    {
        Power = power;
        Resistance = resistance;
    }

}
