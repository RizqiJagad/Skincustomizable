using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class MixAndMatchSpriteSwitcher : MonoBehaviour
{
    // Menggunakan Dictionary untuk menyimpan SpriteResolver berdasarkan kategori
    // Ini akan memudahkan kita menemukan SpriteResolver untuk setiap bagian tubuh
    public Dictionary<string, SpriteResolver> categoryResolvers = new Dictionary<string, SpriteResolver>();

    // Struktur untuk mendefinisikan pilihan yang tersedia untuk setiap kategori
    [System.Serializable]
    public class CategoryOptions
    {
        public string categoryName; // Contoh: "Body", "Leg", "Hair"
        public List<string> labels; // Daftar label yang tersedia untuk kategori ini (misal: "None", "Default", "Firefighter", "Suit")
    }

    [Header("Available Categories and Options")]
    public List<CategoryOptions> availableCategories;

    // Optional: Untuk menyimpan pilihan saat ini untuk setiap kategori
    // Akan diisi saat Runtime
    private Dictionary<string, string> currentSelections = new Dictionary<string, string>();


    void Awake()
    {
        // Inisialisasi categoryResolvers saat game dimulai
        // Cari semua SpriteResolver di bawah GameObject ini
        SpriteResolver[] resolvers = GetComponentsInChildren<SpriteResolver>(true); // true untuk mencari di inactive children juga

        foreach (SpriteResolver resolver in resolvers)
        {
            // Tambahkan resolver ke dictionary berdasarkan kategori awal mereka
            // Ini asumsi setiap resolver memiliki kategori unik di hierarki
            // Anda mungkin perlu menyesuaikan logika ini jika ada beberapa resolver dengan kategori yang sama
            if (!categoryResolvers.ContainsKey(resolver.GetCategory())) // KOREKSI DI SINI
            {
                categoryResolvers.Add(resolver.GetCategory(), resolver);
                currentSelections.Add(resolver.GetCategory(), resolver.GetLabel()); // Simpan label awal
            }
            else
            {
                Debug.LogWarning($"Multiple SpriteResolvers found for category: {resolver.GetCategory()}. Only the first one will be used in MixAndMatchSpriteSwitcher. Consider renaming categories or adjusting hierarchy.");
            }
        }
    }

    // --- Public Methods untuk Mix and Match ---

    /// <summary>
    /// Mengatur label untuk SpriteResolver di kategori tertentu.
    /// </summary>
    /// <param name="category">Nama kategori (misal: "Body", "Leg")</param>
    /// <param name="newLabel">Label baru yang ingin diterapkan (misal: "Firefighter", "None")</param>
    public void SetPart(string category, string newLabel)
    {
        if (categoryResolvers.TryGetValue(category, out SpriteResolver resolver))
        {
            if (newLabel == "None")
            {
                resolver.gameObject.SetActive(false);
            }
            else
            {
                resolver.gameObject.SetActive(true);
                resolver.SetCategoryAndLabel(category, newLabel);
                resolver.ResolveSpriteToSpriteRenderer();
            }
            currentSelections[category] = newLabel; // Update pilihan saat ini
            Debug.Log($"Category '{category}' changed to label: '{newLabel}'");
        }
        else
        {
            Debug.LogWarning($"SpriteResolver for category '{category}' not found!");
        }
    }

    /// <summary>
    /// Mendapatkan label saat ini untuk kategori tertentu.
    /// </summary>
    /// <param name="category">Nama kategori.</param>
    /// <returns>Label saat ini, atau string kosong jika tidak ditemukan.</returns>
    public string GetCurrentLabel(string category)
    {
        if (currentSelections.TryGetValue(category, out string label))
        {
            return label;
        }
        return string.Empty;
    }

    // --- Contoh Penggunaan (Debugging / UI Interaction) ---

    // Metode untuk mengganti ke opsi berikutnya dalam kategori tertentu
    [ContextMenu("Next Body Part")] // Contoh ContextMenu untuk debug di Inspector
    public void NextPartDebugBody()
    {
        CyclePart("Body");
    }

    // Metode untuk mengganti ke opsi berikutnya dalam kategori tertentu
    public void CyclePart(string category)
    {
        if (categoryResolvers.TryGetValue(category, out SpriteResolver resolver))
        {
            // Dapatkan daftar label yang tersedia untuk kategori ini
            List<string> labelsForCategory = GetLabelsForCategory(category);

            if (labelsForCategory != null && labelsForCategory.Count > 0)
            {
                string currentLabel = GetCurrentLabel(category);
                int currentIndex = labelsForCategory.IndexOf(currentLabel);
                int nextIndex = (currentIndex + 1) % labelsForCategory.Count;
                SetPart(category, labelsForCategory[nextIndex]);
            }
            else
            {
                Debug.LogWarning($"No labels defined for category: {category}");
            }
        }
    }

    private List<string> GetLabelsForCategory(string category)
    {
        foreach (var catOption in availableCategories)
        {
            if (catOption.categoryName == category)
            {
                return catOption.labels;
            }
        }
        return null; // Kategori tidak ditemukan
    }

    // Metode untuk mengatur ulang semua bagian ke "None" atau default
    [ContextMenu("Reset All Parts")]
    public void ResetAllParts()
    {
        foreach (var category in categoryResolvers.Keys)
        {
            SetPart(category, "None"); // Atau label default lainnya jika Anda punya
        }
    }
}