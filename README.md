# Yemek Sipariş Uygulaması

Bu proje, kullanıcıların restoranlardan yemek siparişi verebilmelerini sağlayan bir uygulamanın simülasyonudur. C# Form ve SQL kullanarak geliştirilmiştir.

## Özellikler

- **Giriş ve Kayıt Olma:**
  - Kullanıcılar giriş yapabilir veya yeni bir hesap oluşturabilir.
  - Kayıt sırasında e-posta doğrulaması için kullanıcıya bir kod gönderilir.

- **Restoran Listesi:**
  - Giriş yaptıktan sonra kullanıcıya mevcut restoranların listesi veritabanından çekilerek gösterilir.

- **Menü Görüntüleme:**
  - Bir restorana tıklandığında, restoranın menüsü veritabanından çekilerek kullanıcıya sunulur.

- **Sepete Ekleme:**
  - Kullanıcı, menüden seçtiği yemekleri sepete ekleyebilir.

- **Sipariş Verme:**
  - Kullanıcı, sepetindeki yemekler için sipariş verebilir.
  - Sipariş bilgileri QR koduna dönüştürülerek ekranda gösterilir.

## Kullanılan Teknolojiler

- **Geliştirme Ortamı:** C# Windows Forms
- **Veritabanı:** SQL

## Kurulum

1. Bu projeyi yerel makinenize klonlayın:
   ```bash
   git clone https://github.com/anilcee/csharp-food-order/.git
