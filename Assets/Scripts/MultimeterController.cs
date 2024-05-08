using UnityEngine;

public class MultimeterController : MonoBehaviour
{
    public MultimeterModel model;
    public MultimeterView view;

    private Transform mid;
    private Outline outline;

    [SerializeField]
    private float scrollAngleStep = 72f;

    private bool isMouseOver = false;
    
    private void Start()
    {
        mid = GetComponent<Transform>();
        outline = GetComponent<Outline>();

        outline.enabled = false;

        model.SetValues(400f, 1000f);
    }

    private void OnMouseEnter()
    {
        outline.enabled = true;
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
        isMouseOver = false;
    }

    private void LateUpdate()
    {
        if (isMouseOver)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (model.CurrentMode == MultimeterMode.Resistance)
                    model.SetMode();
                else
                    model.SwitchModeNext();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (model.CurrentMode > MultimeterMode.Off)
                    model.SwitchModeBack();
                else
                    model.SetMode(4);
            }
            
            view.UpdateMode(model.CurrentMode);
        }

        switch (model.CurrentMode)
        {
            case MultimeterMode.Voltage:
                view.UpdateValues(model.CalculateVoltage());
                break;
            case MultimeterMode.ACVoltage:
                view.UpdateValues(0.01f);
                break;
            case MultimeterMode.Amperage:
                view.UpdateValues(model.CalculateAmperage());
                break;
            case MultimeterMode.Resistance:
                view.UpdateValues(model.Resistance);
                break;
            default:
                view.TurnOffDisplay();
                break;
        }

        mid.localRotation = Quaternion.Lerp(
            mid.localRotation, 
            Quaternion.Euler(((float)model.CurrentMode * scrollAngleStep) - 90, -90, 90), 
            0.05f);
    }
}