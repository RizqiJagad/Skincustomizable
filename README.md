# 🎮 SkinCustomizable

Sistem kustomisasi karakter 2D di Unity yang memungkinkan penggantian **outfit** (seperti baju, celana, dan aksesoris) serta elemen **sprite dinamis** seperti rambut dan warna. Cocok untuk proyek game dengan karakter yang dapat disesuaikan oleh pemain.

---

## 🧩 Komponen Utama

### 1. `SpriteOutfitSwitcher`

Komponen ini digunakan untuk mengganti satu set pakaian karakter secara otomatis berdasarkan nama outfit.

#### Properti:
- `Outfit List`: Daftar outfit yang tersedia.
  - `Outfit Name`: Nama dari outfit (misalnya: `FireFighter`)
  - `Sprites`: Elemen-elemen sprite yang terganti.
    - `Part Name`: Nama bagian (Body, Shirt, Collar, dll)
    - `Target`: Objek `SpriteRenderer` yang akan diganti.
    - `Sprite`: Sprite baru yang ditampilkan pada bagian tersebut.

🖼️ Contoh tampilan di Unity:
![SpriteOutfitSwitcher Inspector](/Assets/Doc/Image/SpriteOutfitSwitcher.png)

---

### 2. `CustomizableElement`

Untuk bagian seperti **rambut, aksesoris, atau skin** yang bisa dikustomisasi dari segi bentuk dan warna.

#### Properti:
- `Sprite Renderer`: Target `SpriteRenderer`.
- `Sprite Options`: Daftar pilihan sprite.
- `Sprite Index`: Indeks sprite yang sedang digunakan.
- `Color Options`: Daftar warna yang tersedia.
- `Color Index`: Indeks warna yang digunakan saat ini.

🖼️ Contoh tampilan di Unity:
![CustomizableElement Inspector](/Assets/Doc/Image/SpriteOutfitSwitcher.png)

---

## 🛠️ Cara Menggunakan

### 👕 Outfit:
1. Tambahkan script `SpriteOutfitSwitcher` ke GameObject karakter utama.
2. Tambahkan data outfit sesuai jumlah set yang ingin ditampilkan.
3. Masukkan SpriteRenderer target dan Sprite baru per bagian tubuh.

### 🎨 Custom Element:
1. Tambahkan script `CustomizableElement` ke elemen seperti rambut.
2. Tambahkan beberapa `Sprite Options` dan `Color Options`.
3. Gunakan `Sprite Index` dan `Color Index` untuk mengatur tampilan awal.

---
