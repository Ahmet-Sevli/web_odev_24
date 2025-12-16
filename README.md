# 💇‍♀️ Kuaför / Berber İşletme Yönetim Sistemi

**2024–2025 Güz Dönemi – Web Programlama Dersi Proje Ödevi**

Bu proje, Web Programlama dersi kapsamında teorik ve pratik bilgilerin gerçek bir probleme uygulanması amacıyla geliştirilmiş bir **ASP.NET Core MVC tabanlı kuaför / berber işletme yönetim sistemidir**.

---

## 🇹🇷 Türkçe

### 📌 Proje Amacı

Bu projenin amacı; kuaför ve berber salonlarının sunduğu hizmetleri, çalışanlarını ve randevu süreçlerini dijital ortamda yönetebilecekleri bir web uygulaması geliştirmektir. Sistem sayesinde:

* Müşteriler uygun çalışanlardan randevu alabilir,
* Çalışanların müsaitlik durumları takip edilebilir,
* Günlük işlem yoğunluğu ve kazançlar izlenebilir.

Ayrıca proje kapsamında **REST API kullanımı** ve **yapay zekâ entegrasyonu** sağlanmıştır.

---

### 🧩 Proje Kapsamı ve Özellikler

#### 1️⃣  Berber Tanımlamaları

* Berberlerin çalışma saatleri belirlenebilir.
* Sunulan işlemler, işlem süreleri ve ücretleri tanımlanabilir.

#### 2️⃣ Çalışan Yönetimi

* Salon çalışanları sisteme eklenebilir.
* Her çalışanın uzmanlık alanları ve yapabildiği işlemler tanımlanabilir.
* Çalışanların uygunluk saatleri belirlenir.

#### 3️⃣ Randevu Sistemi

* Kullanıcılar uygun çalışan ve işlem seçerek randevu alabilir.
* Çakışan randevular sistem tarafından engellenir.
* Randevu detayları (işlem, ücret) kayıt altına alınır.
* Randevu onay mekanizması bulunmaktadır.

#### 4️⃣ REST API Kullanımı

* Projenin bir bölümünde veritabanı ile iletişim **REST API** üzerinden sağlanmaktadır.
* LINQ kullanılarak uygun veriler sorgulanmaktadır.

#### 5️⃣ Yapay Zekâ Entegrasyonu

* Kullanıcılar sisteme fotoğraf yükleyebilir.
* Yapay zekâ entegrasyonu sayesinde farklı saç modeli veya saç rengi  eklenir.

---

### 🛠️ Kullanılan Teknolojiler

* ASP.NET Core 6 MVC
* C#
* Entity Framework Core (ORM)
* SQL Server / PostgreSQL
* RESTful API
* HTML5, CSS3, JavaScript, jQuery
* Bootstrap
* Yapay Zekâ Servisi (harici entegrasyon)

---

### 👤 Kullanıcı Rolleri

* **Admin**

  * Salon, çalışan ve işlem yönetimi
  * Randevu onaylama

* **Kayıtlı Kullanıcı**

  * Randevu alma
  * Randevu görüntüleme

> Admin Giriş Bilgileri
> **Kullanıcı Adı:** [OgrenciNumarasi@sakarya.edu.tr](mailto:OgrenciNumarasi@sakarya.edu.tr)
> **Şifre:** sau

---

### 🚀 Kurulum

1. Projeyi klonlayın

```bash
git clone https://github.com/Ahmet-Sevli/web_odev_24i.git
```

2. Veritabanı ayarlarını `appsettings.json` dosyasında yapılandırın

3. Migration işlemlerini çalıştırın

```bash
dotnet ef database update
```

4. Projeyi çalıştırın

```bash
dotnet run
```

---



## 🇬🇧 English

### 📌 Project Description

This project is a **Hairdresser / Barber Shop Management System** developed using **ASP.NET Core MVC** as part of the 2024–2025 Fall Semester Web Programming course.

The system enables salons to manage services, employees, and appointment processes while allowing customers to book appointments based on employee availability. The project also includes **REST API usage** and **AI integration**.

---

### 🧩 Key Features

* Salon and service management
* Employee management with expertise and availability
* Appointment booking and validation
* REST API for database communication
* AI-based hairstyle or hair color recommendations via image upload

---

### 🛠️ Technologies Used

* ASP.NET Core 6 MVC
* C#
* Entity Framework Core
* SQL Server / PostgreSQL
* RESTful API
* HTML5, CSS3, JavaScript, jQuery
* Bootstrap
* AI Integration Service

---

### 📄 Academic Context

This project was developed as part of a **university course assignment** and is shared publicly on GitHub in accordance with course requirements.

---

## 📄 License

This project is for **educational purposes only**.

---

### 👤 Öğrenci Bilgileri

* **Ad Soyad:** …………………
* **Öğrenci No:** …………………
* **Ders:** Web Programlama
* **Üniversite:** Sakarya Üniversitesi
* **GitHub:** [https://github.com/kullanici-adi](https://github.com/kullanici-adi)
