using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteOutline1Stage : MonoBehaviour
{
    [SerializeField] GameObject obj;

    [Header("Outline Settings")]
    [SerializeField] private float outlineThickness = 10f;       // �ƿ����� �β�

    private SpriteRenderer sprite;
    private Material outlineMaterial;
    private Material[] materials;

    [SerializeField] private PlayerEvent_1 player;

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

        player = GameObject.Find("Player").GetComponent<PlayerEvent_1>();
    }
    private void Update()
    {
        outlineMaterial.SetFloat("_OutlineThickness", outlineThickness);
    }

    void OnMouseEnter()
    {
        if (!player.onLight) return;

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