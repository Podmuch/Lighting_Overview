using UnityEngine;
using System.Collections;

public class AutoIntensity : MonoBehaviour
{
    public ParticleSystem Stars;
    public Color starsColor = Color.white;

    public float maxIntensity = 1.2f;
    public float maxAmbient = 1f;

    public Vector3 eastDirection;
    public Vector3 westDirection;
    public float heightOfSunInMidday;
    public float animationTime = 60;

    public float CurrentIntensity { get { return mainLight.intensity; } }

    float timer = 0;
    Light mainLight;
    Skybox sky;
    Material skyMat;

    void Awake()
    {
        timer = animationTime * 0.5f;
        mainLight = GetComponent<Light>();
        skyMat = RenderSettings.skybox;
        Update();
    }

    void Update()
    {
        //east = 0, west = 1 || -1, south = 0.5, north = -0.5
        float animationFactor = (timer - animationTime * 0.5f) / (animationTime*0.5f);
        float sunPositionFactor = animationFactor*2.0f;
        sunPositionFactor = sunPositionFactor < -1.0f ? Mathf.Abs(sunPositionFactor) - 2.0f :
                            sunPositionFactor > 1.0f ? 2.0f - sunPositionFactor : sunPositionFactor;
        sunPositionFactor = Mathf.Sign(sunPositionFactor) * Mathf.Sin(Mathf.Abs(sunPositionFactor) * Mathf.PI * 0.5f);
        float clampedFactor = Mathf.Clamp01(sunPositionFactor);

        mainLight.intensity = maxIntensity * clampedFactor;

        RenderSettings.ambientIntensity = Mathf.LerpUnclamped(maxAmbient*0.25f, maxAmbient, sunPositionFactor);

        transform.localRotation = Quaternion.Euler(Vector3.SlerpUnclamped(eastDirection, westDirection, animationFactor));
        transform.localRotation = Quaternion.Euler(new Vector3(Mathf.LerpUnclamped(0, heightOfSunInMidday, sunPositionFactor), transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z));
        timer += Time.deltaTime;
        timer = timer > animationTime ? 0 : timer;
        if (Stars != null)
        {
            var starsMainModule = Stars.main;
            starsMainModule.startColor = new Color(starsColor.r, starsColor.g, starsColor.b, 1.0f - clampedFactor);
        }
    }
}