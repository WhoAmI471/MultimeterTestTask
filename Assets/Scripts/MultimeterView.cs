using UnityEngine;
using TMPro;

public class MultimeterView : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro valueText;

    [SerializeField]
    private TextMeshProUGUI voltageModeText;
    
    [SerializeField]
    private TextMeshProUGUI aCVoltageModeText;
    
    [SerializeField]
    private TextMeshProUGUI amperageModeText;

    [SerializeField]
    private TextMeshProUGUI resistanceModeText;

    public void UpdateMode(MultimeterMode mode)
    {
        voltageModeText.text = "V 0";
        amperageModeText.text = "A 0";
        aCVoltageModeText.text = "~ 0";
        resistanceModeText.text = "Ω 0";

        switch (mode)
        {
            case MultimeterMode.Voltage:
                voltageModeText.text = $"V {valueText.text}";
                break;
            case MultimeterMode.Amperage:
                amperageModeText.text = $"A {valueText.text}";
                break;
            case MultimeterMode.ACVoltage:
                aCVoltageModeText.text = $"~ {valueText.text}";
                break;
            case MultimeterMode.Resistance:
                resistanceModeText.text = $"Ω {valueText.text}";
                break;
            default:
                break;
        }
    }

    public void UpdateValues(float value)
    {
        valueText.text = value.ToString();
    }

    public void TurnOffDisplay()
    {
        valueText.text = "";
    }
}
