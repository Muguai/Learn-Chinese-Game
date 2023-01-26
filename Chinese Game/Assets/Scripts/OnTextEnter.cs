using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnTextEnter : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            TextMeshPro[] texts;
            texts = col.gameObject.GetComponentsInChildren<TextMeshPro>();
            foreach (TextMeshPro T in texts)
            {
                Debug.Log("HITTEXT");
                // T.renderer.material.SetColor(ShaderUtilities.ID_GlowColor, new Color32(0, 255, 0, 128));
                T.fontMaterial.SetFloat("_GlowPower", 1f);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            TextMeshPro[] texts;
            texts = col.gameObject.GetComponentsInChildren<TextMeshPro>();
            foreach (TextMeshPro T in texts)
            {
                Debug.Log("HITTEXT");
                // T.renderer.material.SetColor(ShaderUtilities.ID_GlowColor, new Color32(0, 255, 0, 128));
                T.fontMaterial.SetFloat("_GlowPower", 0f);
            }
        }
    }
}
