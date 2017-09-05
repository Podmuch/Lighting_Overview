using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampsController : MonoBehaviour
{
    [SerializeField]
    private AutoIntensity IntensityController;

    [SerializeField]
    private float minRelativeIntensity = 0.2f;

    public List<Light> Lights;
    private bool lightsEnabled;

	private void Update ()
    {
		if(lightsEnabled && IntensityController.CurrentIntensity > minRelativeIntensity * IntensityController.maxIntensity)
        {
            SetLightsState(false);
        }
        else if(!lightsEnabled && IntensityController.CurrentIntensity < minRelativeIntensity * IntensityController.maxIntensity)
        {
            SetLightsState(true);
        }
	}

    public void FindAllLights()
    {
        Lights = new List<Light>(gameObject.GetComponentsInChildren<Light>());
    }

    private void SetLightsState(bool state)
    {
        lightsEnabled = state;
        for(int i = 0; i < Lights.Count; i++)
        {
            Lights[i].gameObject.SetActive(state);
        }
    }
}
