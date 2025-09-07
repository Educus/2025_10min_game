using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteOutline : MonoBehaviour
{
    [SerializeField] GameObject obj;

    [Header("Outline Settings")]
    [SerializeField] private float outlineThickness = 10f;       // 아웃라인 두께

    private SpriteRenderer sprite;
    private Material outlineMaterial;
    private Material[] materials;

    private void Awake()
    {
        if (obj == null)
        {
            sprite = GetComponent<SpriteRenderer>();
        }
        else
        {
            sprite = obj.GetComponent<SpriteRenderer>();
        }

        outlineMaterial = Instantiate(Resources.Load<Material>(@"Materials/SpriteOutlineMat"));

        outlineMaterial.name = "OutLine (Instance)";

        materials = sprite.materials;
    }
    private void Update()
    {
        outlineMaterial.SetFloat("_OutlineThickness", outlineThickness);
    }

    void OnMouseEnter()
    {
        if (sprite.color != new Color(1, 1, 1, 0))
        {
            sprite.material = outlineMaterial;
        }
    }

    void OnMouseExit()
    {
        sprite.materials = this.materials.ToArray();
    }
}