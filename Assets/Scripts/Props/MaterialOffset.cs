using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialOffset : MonoBehaviour
{
    public Material material;
    public float yOffset = 0.1f;
    public float ySpeed;
    private void Update()
    {
        yOffset += ySpeed * Time.deltaTime;
        material.mainTextureOffset = new Vector2(0, yOffset);
    }
}
