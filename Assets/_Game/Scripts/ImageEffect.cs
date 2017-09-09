using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ImageEffect : MonoBehaviour
{
  [SerializeField] private Shader shader;
  [SerializeField] Color tint;
  private Material materialActual;

  Material material
  {
    get
    {
      if (materialActual == null)
      {
        Debug.Log("Creando material");
        materialActual = new Material(shader);
        materialActual.hideFlags = HideFlags.HideAndDontSave;
      }
      return materialActual;
    }
  }


  void Start()
  {
    if (!SystemInfo.supportsImageEffects)
    {
      enabled = false;
      return;
    }

    if (!shader || !shader.isSupported)
    {
      enabled = false;
    }

  }

  void OnRenderImage(RenderTexture entrada, RenderTexture salida)
  {
    material.SetColor("_Tint", tint);
    Graphics.Blit(entrada, salida, material);
  }


  void OnDisable()
  {
    if (materialActual)
    {
      DestroyImmediate(materialActual);
    }
  }
}
