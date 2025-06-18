using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOutfitSwitcher : MonoBehaviour
{
    [System.Serializable]
    public class OutfitData
    {
        public string outfitName;
        public List<SpriteEntry> sprites;
    }

    [System.Serializable]
    public class SpriteEntry
    {
        public string partName;                 // Contoh: "Body", "Hair", dll
        public SpriteRenderer targetRenderer;   // Renderer spesifik untuk bagian ini
        public Sprite sprite;                   // Sprite yang akan ditampilkan
        // public Vector3 positionOffset;       // Tidak perlu kalau pakai bone
    }

    [Header("Daftar Outfit")]
    public List<OutfitData> outfitList;

    [Header("Semua Sprite Renderer yang Mungkin Ada")]
    public List<SpriteRenderer> allPartRenderers;

    private int currentIndex = 0;

    void Awake()
    {
        // Opsional: otomatis ambil semua SpriteRenderer anak dari objek ini
        if (allPartRenderers == null || allPartRenderers.Count == 0)
        {
            allPartRenderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());
        }
    }

    void Start()
    {
        ApplyOutfit(currentIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextOutfit();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousOutfit();
        }
    }

    private void ClearAllParts()
    {
        foreach (var renderer in allPartRenderers)
        {
            if (renderer != null)
            {
                renderer.sprite = null;
            }
        }
    }

    private void ApplyOutfit(int index)
    {
        if (index < 0 || index >= outfitList.Count) return;

        ClearAllParts();

        var outfit = outfitList[index];

        foreach (var entry in outfit.sprites)
        {
            if (entry.targetRenderer != null)
            {
                entry.targetRenderer.sprite = entry.sprite;
                // Hapus offset agar tidak ganggu rigging
                // entry.targetRenderer.transform.localPosition = entry.positionOffset;
            }
            else
            {
                Debug.LogWarning($"Renderer untuk part '{entry.partName}' belum diset di Outfit '{outfit.outfitName}'.");
            }
        }

        Debug.Log($"Outfit aktif: {outfit.outfitName}");
    }

    [ContextMenu("Next Outfit")]
    public void NextOutfit()
    {
        currentIndex = (currentIndex + 1) % outfitList.Count;
        ApplyOutfit(currentIndex);
    }

    [ContextMenu("Previous Outfit")]
    public void PreviousOutfit()
    {
        currentIndex = (currentIndex - 1 + outfitList.Count) % outfitList.Count;
        ApplyOutfit(currentIndex);
    }
}
