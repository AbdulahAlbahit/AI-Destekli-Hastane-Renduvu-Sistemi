# MedRandevu — AI Destekli Hastane Randevu Sistemi

Hastaların online ortamda kolayca randevu alabileceği, yapay zeka ile doğru bölüme yönlendirileceği bir hastane randevu sistemidir.

**Canlı Demo:** [midrandevu.vercel.app](https://midrandevu.vercel.app)

---

## Projenin Amacı

Hastane randevu süreçlerini dijitalleştirmek ve hastaları yapay zeka yardımıyla doğru uzmanlara yönlendirmek. Kullanıcılar sisteme giriş yaptıktan sonra bölüm seçebilir, doktor seçebilir, uygun saat belirleyerek randevusunu oluşturabilir. Ayrıca AI asistana şikayetlerini yazarak hangi bölüme gitmesi gerektiği konusunda öneri alabilir.

---

## Özellikler

- **Kullanıcı Kayıt & Giriş** — TC kimlik numarası ve şifre ile JWT tabanlı güvenli oturum
- **Bölüm & Doktor Listeleme** — Tüm bölümler, klinikler ve doktorlar listelenir
- **Randevu Alma** — Adım adım (wizard) bölüm → doktor → tarih → saat seçimi
- **Randevu Yönetimi** — Aktif ve geçmiş randevuları görüntüleme, iptal etme
- **AI Sağlık Asistanı** — Gemini API ile entegre yapay zeka; şikayetleri analiz edip bölüm önerir
- **Dashboard** — Kullanıcıya özel istatistikler ve hızlı erişim paneli
- **Responsive Tasarım** — Mobil, tablet ve masaüstüne uyumlu arayüz

---

## Teknolojiler

### Backend
| Teknoloji | Açıklama |
|-----------|----------|
| ASP.NET Core (.NET 10) | Web API |
| Entity Framework Core | ORM (veritabanı işlemleri) |
| SQL Server | Veritabanı |
| JWT | Kimlik doğrulama |
| Gemini API | Yapay zeka entegrasyonu |

### Frontend
| Teknoloji | Açıklama |
|-----------|----------|
| HTML5 | Sayfa yapısı |
| CSS3 (Vanilla) | Tasarım ve stil |
| JavaScript (Vanilla) | Mantık ve API iletişimi |

### Deployment
| Platform | Kullanım |
|----------|----------|
| Somee.com | Backend API + SQL Server barındırma |
| Vercel | Frontend barındırma (GitHub CI/CD) |

---

## Proje Mimarisi

```
├── Api Layer/              # ASP.NET Core Web API
│   ├── Controllers/        # API endpoint'leri (User, Patient, Appointment, Ai, Department)
│   ├── settings/           # JWT ve request modelleri
│   └── Program.cs          # Uygulama ayarları ve middleware
│
├── Business Layer/         # İş mantığı katmanı
│   ├── IServices/          # Servis arayüzleri
│   ├── Services/           # Servis implementasyonları
│   ├── Dto/                # Veri transfer objeleri
│   └── Validation/         # Doğrulama kuralları
│
├── Data Accese Layer/      # Veritabanı katmanı
│   ├── Entities/           # Veritabanı tabloları (C# sınıfları)
│   ├── IRepository/        # Repository arayüzleri
│   ├── Repository/         # Repository implementasyonları
│   └── Context/            # DbContext (EF Core)
│
└── Frontend/               # Kullanıcı arayüzü
    ├── index.html          # Ana sayfa (landing page)
    ├── login.html          # Giriş ekranı
    ├── register.html       # Kayıt ekranı
    ├── dashboard.html      # Kullanıcı paneli
    ├── book.html           # Randevu alma (wizard)
    ├── appointments.html   # Randevularım
    ├── departments.html    # Bölümler
    ├── css/                # Stil dosyaları
    └── js/                 # JavaScript dosyaları
```

---

## Kurulum (Yerelde Çalıştırma)

### Gereksinimler
- .NET 10 SDK
- SQL Server (LocalDB veya Express)
- Bir web tarayıcı

### Adımlar

1. Repoyu klonlayın:
```bash
git clone https://github.com/AbdulahAlbahit/AI-Destekli-Hastane-Renduvu-Sistemi.git
```

2. `appsettings.json` dosyasındaki bağlantı bilgilerini kendi SQL Server'ınıza göre güncelleyin.

3. Migration uygulayın:
```bash
dotnet ef database update -p "Data Accese Layer/Data Access Layer.csproj" -s "Api Layer/Api Layer.csproj"
```

4. API'yi çalıştırın:
```bash
dotnet run --project "Api Layer/Api Layer.csproj"
```

5. `Frontend/index.html` dosyasını tarayıcıda açın.

---

## Lisans

Bu proje eğitim amaçlı geliştirilmiştir.
