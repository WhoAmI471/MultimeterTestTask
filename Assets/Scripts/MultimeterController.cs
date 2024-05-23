using System;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class MultimeterController : MonoBehaviour
{
    [SerializeField]
    private MultimeterModel MultimeterModel;
    [SerializeField]
    private MultimeterView MultimeterView;
    [SerializeField]
    private float scrollAngleStep = 72f;
    [SerializeField]
    private float power = 400f;
    [SerializeField]
    private float resistance = 1000f;

    private Outline outline;
    private bool isMouseOver = false;

    public static event Action ActionTriggerActive;
    
    private void Start()
    {
        outline = GetComponent<Outline>();

        outline.enabled = false;

        MultimeterModel.SetValues(power, resistance);
    }

    private void OnMouseEnter()
    {
        outline.enabled = true;
        isMouseOver = true;

        ActionTriggerActive += UpdateDisplayValues;
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
        isMouseOver = false;
        ActionTriggerActive -= UpdateDisplayValues;
    }

    private void UpdateDisplayValues()
    {
        switch (MultimeterModel.CurrentMode)
        {
            case MultimeterMode.Voltage:
                MultimeterView.UpdateValues(MultimeterModel.CalculateVoltage());
                break;
            case MultimeterMode.ACVoltage:
                MultimeterView.UpdateValues(0.01f);
                break;
            case MultimeterMode.Amperage:
                MultimeterView.UpdateValues(MultimeterModel.CalculateAmperage());
                break;
            case MultimeterMode.Resistance:
                MultimeterView.UpdateValues(MultimeterModel.Resistance);
                break;
            default:
                MultimeterView.TurnOffDisplay();
                break;
        }
    }

    private void LateUpdate()
    {
        if (isMouseOver)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (MultimeterModel.CurrentMode == MultimeterMode.Resistance)
                    MultimeterModel.SetMode();
                else
                    MultimeterModel.SwitchModeNext();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (MultimeterModel.CurrentMode > MultimeterMode.Off)
                    MultimeterModel.SwitchModeBack();
                else
                    MultimeterModel.SetMode(4);
            }
            
            ActionTriggerActive?.Invoke();
            MultimeterView.UpdateMode(MultimeterModel.CurrentMode);
        }



        transform.localRotation = Quaternion.Lerp(
            transform.localRotation, 
            Quaternion.Euler(((float)MultimeterModel.CurrentMode * scrollAngleStep) - 90, -90, 90), 
            0.05f);
    }
}