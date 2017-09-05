using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetChanger : MonoBehaviour
{
    public Material WallMaterial;

    private float offset;

    private void Update ()
    {
        offset += Time.deltaTime;
        offset = offset >= 1.0f ? offset - 1.0f : offset;
        WallMaterial.mainTextureOffset = new Vector2(offset, 0);
    }
}
