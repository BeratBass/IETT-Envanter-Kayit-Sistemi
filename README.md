# 📦 İETT Envanter Kayıt Sistemi

Bu proje, **İETT Bilgi İşlem** departmanındaki stajım sırasında geliştirilmiş bir envanter kayıt sistemi web uygulamasıdır.

[cite_start]Uygulamanın temel amacı, kurum envanterindeki ürünlerin (marka, model, kategori) ve bu ürünlere bağlı işlemlerin (personel sicili, adet, tarih) dijital olarak yönetilmesini sağlamaktır[cite: 55, 101]. [cite_start]Sistem, rol bazlı erişim kontrolü sunarak "Admin" ve "Kullanıcı" rolleri için farklı yetkilendirmeler sağlar[cite: 25, 56, 101].

## 🚀 Temel Özellikler

* **🔐 Güvenli Kullanıcı Yönetimi:** MD5 ile şifrelenmiş parola korumalı Kayıt Ol (`/Register`), Giriş Yap (`/Login`) ve Çıkış Yap (`/Logout`) özellikleri.
* [cite_start]**👮 Rol Bazlı Erişim Kontrolü:** "Admin" rolüne sahip kullanıcılar (`User.IsInRole("admin")`) envanter oluşturma, düzenleme ve silme gibi kritik işlemleri yapabilirken[cite: 25, 40, 56, 87, 101, 117], normal kullanıcılar bu alanlara erişemez.
* [cite_start]**🚚 İşlem (Envanter) Yönetimi:** Personele (sicil numarası ile) ürün (marka, model) atama, adet ve tarih belirleme[cite: 64, 65, 66, 68, 70, 71].
* [cite_start]**🏷️ Ürün Yönetimi:** Sisteme yeni Ürünler (Marka, Model) ekleme [cite: 106, 107] [cite_start]ve bu ürünleri Ürün Kategorileri ile ilişkilendirme[cite: 108].
* [cite_start]**📁 Kategori Yönetimi:** Ürünleri sınıflandırmak için dinamik kategoriler oluşturma ve listeleme[cite: 30, 32, 34].
* [cite_start]**🔄 Dinamik Formlar:** JavaScript kullanılarak, "İşlemler" sayfasında bir marka seçildiğinde, ilgili modellerin otomatik olarak "Model" dropdown menüsüne yüklenmesi[cite: 62, 73, 74, 75, 78].
* [cite_start]**↕️ Aktif/Pasif Durum Yönetimi:** Tüm kayıtların (İşlemler, Kategoriler, Ürünler) durumunu "Aktifleştir" veya "Pasifleştir" butonları ile yönetebilme[cite: 37, 40, 41, 43, 85, 88, 91, 115, 118, 121].
* [cite_start]**📄 Sayfalama (Pagination):** Tüm tablolarda (İşlemler, Kategoriler, Ürünler), kayıtları bölerek daha hızlı ve verimli bir kullanıcı deneyimi sunma[cite: 46, 47, 94, 95, 124, 125].
* [cite_start]**🔔 Anlık Bildirim Sistemi:** `TempData` kullanılarak [cite: 48, 126, 141][cite_start], başarılı (yeşil) veya hatalı (kırmızı) işlemlerde kullanıcıya 3 saniyeliğine görünen pop-up uyarı mesajları gösterme[cite: 48, 49, 50, 51, 127, 142].

## 📸 Ekran Görüntüleri

*(Tavsiye: Projenizin `README.md` dosyasının daha profesyonel görünmesi için bu başlığın altına uygulamanızın ekran görüntülerini (Login, Anasayfa, İşlemler vb.) ekleyebilirsiniz.)*

**Giriş Ekranı**
![Login Ekranı](buraya-login-ekrani-resmini-ekleyin.png)

**İşlemler Sayfası (Admin)**
![İşlemler Ekranı](buraya-islemler-ekrani-resmini-ekleyin.png)

**Ürünler Sayfası**
![Ürünler Ekranı](buraya-urunler-ekrani-resmini-ekleyin.png)

## 🛠️ Kullanılan Teknolojiler

Bu projenin geliştirilmesinde aşağıdaki teknolojiler ve mimariler kullanılmıştır:

* **Backend:**
    * `C#`
    * `ASP.NET Core MVC` (Web uygulaması altyapısı)
* **Veritabanı:**
    * `Entity Framework Core` (ORM)
    * `Microsoft SQL Server` (veya belirlediğiniz başka bir veritabanı)
    * `EF Core Migrations` (Veritabanı şema yönetimi)
* **Frontend:**
    * `HTML5`
    * [cite_start]`CSS3` (Özel `sidebar` [cite: 25, 57, 97, 102] ve form stilleri ile)
    * [cite_start]`JavaScript (ES6+)` (Dinamik formlar ve bildirimler için [cite: 16, 50, 73, 127, 142])
    * `Bootstrap` (Hızlı prototipleme ve grid sistemi için)
* **Kimlik Doğrulama:**
    * `ASP.NET Core Cookie Authentication`
    * `MD5` (Parola Hashing)

## 🏁 Projeyi Çalıştırma

Projeyi yerel makinenizde çalıştırmak için aşağıdaki adımları izleyin:

1.  Bu depoyu klonlayın:
    ```bash
    git clone [https://github.com/kullanici-adiniz/proje-adiniz.git](https://github.com/kullanici-adiniz/proje-adiniz.git)
    ```
2.  `iettproje` klasöründeki `appsettings.Development.json` dosyasını oluşturun. (Bu dosya `.gitignore` ile gizlenmiştir).
3.  Oluşturduğunuz `appsettings.Development.json` dosyasına veritabanı bağlantı dizenizi (`ConnectionStrings`) ekleyin:
    ```json
    {
      "ConnectionStrings": {
        "baglanti": "Server=SUNUCU_ADINIZ;Database=IETT_EnvanterDB;Trusted_Connection=True;TrustServerCertificate=True;"
      }
    }
    ```
4.  Visual Studio'da `Package Manager Console`'u açın ve `Migrations`'ı uygulayarak veritabanını oluşturun:
    ```powershell
    Update-Database
    ```
5.  Projeyi Visual Studio'da `F5` tuşuna basarak veya "Başlat" (Start) düğmesi ile çalıştırın.
