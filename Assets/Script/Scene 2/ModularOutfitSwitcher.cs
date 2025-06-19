using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class ModularOutfitSwitcher : MonoBehaviour
{
    [System.Serializable]
    public class SpritePart
    {
        public string category;                    // Contoh: "Body", "Leg", "Hair"
        public string label;                       // Contoh: "Firefighter", "Suit", atau "None"
        public SpriteResolver targetResolver;      // Sprite Resolver yang akan diganti (boleh kosong untuk menonaktifkan objek manual)
    }

    [System.Serializable]
    public class Outfit
    {
        public string outfitName;
        public List<SpritePart> parts;
    }

    public List<Outfit> outfits;

    [Header("Debug Outfit Index")]
    public int currentIndex = 0;

    public void ApplyOutfit(int index)
    {
        if (index < 0 || index >= outfits.Count)
        {
            Debug.LogWarning("Outfit index out of range!");
            return;
        }

        Outfit selected = outfits[index];

        foreach (var part in selected.parts)
        {
            if (part.targetResolver != null)
            {
                // Jika label "None", anggap ingin menyembunyikan bagian
                if (part.label == "None")
                {
                    part.targetResolver.gameObject.SetActive(false);
                    continue;
                }

                // Pastikan aktif dan ganti sprite-nya
                part.targetResolver.gameObject.SetActive(true);
                part.targetResolver.SetCategoryAndLabel(part.category, part.label);
                part.targetResolver.ResolveSpriteToSpriteRenderer();
            }
            else
            {
                // Optional: Coba nonaktifkan berdasarkan label (misalnya objek bernama persis dengan label)
                Transform partObj = transform.Find(part.label);
                if (partObj != null)
                {
                    partObj.gameObject.SetActive(false);
                }
            }
        }

        currentIndex = index;
        Debug.Log($"Outfit applied: {selected.outfitName}");
    }

    [ContextMenu("Next Outfit")]
    public void NextOutfit()
    {
        int next = (currentIndex + 1) % outfits.Count;
        ApplyOutfit(next);
    }

    [ContextMenu("Previous Outfit")]
    public void PreviousOutfit()
    {
        int prev = (currentIndex - 1 + outfits.Count) % outfits.Count;
        ApplyOutfit(prev);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextOutfit();
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            PreviousOutfit();
        }
    }
}
