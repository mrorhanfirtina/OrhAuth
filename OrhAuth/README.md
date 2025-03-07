# OrhAuth

## JWT Tabanlı Kimlik Doğrulama ve Yetkilendirme Kütüphanesi

**OrhAuth**, .NET Framework projeleri için geliştirilmiş, kolay entegre edilebilen, JWT tabanlı bir kimlik doğrulama ve yetkilendirme kütüphanesidir.  
**Entity Framework 6** altyapısını kullanarak, **kullanıcı yönetimi**, **rol tabanlı yetkilendirme** ve **token yönetimi** işlemlerini kolaylaştırır.

---

## İçindekiler

- Özellikler
- Kurulum
- Hızlı Başlangıç
- Yapılandırma
- User Sınıfını Genişletme
- API Kullanımı
  - Kullanıcı Kaydı
  - Kullanıcı Girişi
  - Token Yenileme
  - Yetkilendirme
- Gelişmiş Özellikler
- Örnekler
- SSS

---

## Özellikler

- **JWT Kimlik Doğrulama**: Endüstri standardı JSON Web Token tabanlı kimlik doğrulama
- **Rol-Tabanlı Yetkilendirme**: Esnek yetkilendirme altyapısı
- **Refresh Token Desteği**: Uzun süreli oturum yönetimi
- **Entity Framework 6 Entegrasyonu**: Kod-öncelikli (Code-First) yaklaşım ile veritabanı yönetimi
- **Dinamik User Sınıfı Genişletme**: Özel kullanıcı özellikleri ekleme imkanı
- **Otomatik Veritabanı Oluşturma**: İlk kullanımda veritabanı ve temel verileri otomatik oluşturma
- **OWIN Entegrasyonu**: OWIN middleware desteği

---

## Kurulum

### NuGet Paketi Yükleme

```bash
PM> Install-Package OrhAuth
```

### Veritabanı Bağlantı Bilgisini Ayarlama

**Web.config** veya **App.config** dosyanıza aşağıdaki bağlantı dizesini ekleyin:

```xml
<connectionStrings>
  <add name="AuthDbConnection" connectionString="Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=OrhAuthDb;Integrated Security=True" providerName="System.Data.SqlClient" />
</connectionStrings>
```

### JWT Yapılandırması

**AppSettings** bölümüne JWT ayarlarını ekleyin:

```xml
<appSettings>
  <add key="Jwt:SecurityKey" value="buraya_guclu_bir_guvenlik_anahtari_yazin_en_az_32_karakter_olmali" />
  <add key="Jwt:Issuer" value="https://sizin-siteniz.com" />
  <add key="Jwt:Audience" value="https://sizin-api-adresiniz.com" />
  <add key="Jwt:AccessTokenExpiration" value="60" /> <!-- Dakika cinsinden token süresi -->
  <add key="Jwt:RefreshTokenTTL" value="7" />       <!-- Gün cinsinden refresh token süresi -->
</appSettings>
```

---

## Hızlı Başlangıç

OrhAuth'u projenize eklemek ve yapılandırmak için aşağıdaki adımları izleyin:

```csharp
using OrhAuth.Configurations;
using OrhAuth.Extensions;
using OrhAuth.Services;

// Program.cs veya Global.asax.cs içinde
public static class Startup
{
    public static void ConfigureAuth()
    {
        // OrhAuth yapılandırması
        var authOptions = new OrhAuthOptions
        {
            ConnectionString = "Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=OrhAuthDb;Integrated Security=True",
            CreateDatabaseIfNotExists = true,
            TokenSecurityKey = "buraya_guclu_bir_guvenlik_anahtari_yazin_en_az_32_karakter_olmali",
            TokenIssuer = "https://sizin-siteniz.com",
            TokenAudience = "https://sizin-api-adresiniz.com",
            TokenExpirationMinutes = 60
        };

        // Auth servisini yapılandır
        IAuthService authService = OrhAuthExtensions.ConfigureOrhAuth(authOptions);
        
        // İsteğe bağlı: Service locator veya DI container'a kaydet
        // ServiceLocator.Register<IAuthService>(authService);
    }
}
```

---

## Yapılandırma

OrhAuth, farklı ortamlarda esnek kullanım için çeşitli yapılandırma seçenekleri sunar.

### OrhAuthOptions Sınıfı

```csharp
var options = new OrhAuthOptions
{
    // Veritabanı yapılandırması
    ConnectionString = "your_connection_string",
    CreateDatabaseIfNotExists = true, // İlk çalıştırmada veritabanını oluşturur
    
    // JWT yapılandırması
    TokenSecurityKey = "your_security_key",
    TokenIssuer = "your_issuer",
    TokenAudience = "your_audience",
    TokenExpirationMinutes = 60
};
```

### Web.config/App.config Üzerinden Yapılandırma

Bu durumda OrhAuth, app settings'ten değerleri otomatik olarak okur:

```csharp
// Parametre vermeden oluşturduğunuzda app.config/web.config'ten okur
var authService = OrhAuthExtensions.ConfigureOrhAuth(new OrhAuthOptions
{
    ConnectionString = "your_connection_string",
    CreateDatabaseIfNotExists = true
});
```

---

## User Sınıfını Genişletme

OrhAuth, `User` sınıfını projelerinize özel gereksinimlerinize göre genişletmenize olanak tanır. Bu, `ExtendUserAttribute` özniteliği kullanılarak yapılır:

```csharp
using OrhAuth.Attributes;
using OrhAuth.Models.Entities;

// User sınıfını genişleten yeni bir sınıf
public class ExtendedUser : User
{
    [ExtendUser(maxLength: 100, isRequired: true, description: "Departman bilgisi")]
    public string Department { get; set; }
    
    [ExtendUser(isUnique: true, description: "Çalışan numarası")]
    public string EmployeeNumber { get; set; }
    
    [ExtendUser(dbType: "datetime2")]
    public DateTime LastLoginDate { get; set; }
}
```

Bu genişletme özellikleri, OrhAuth ilk çalıştırıldığında veritabanına otomatik olarak eklenir.

#### `ExtendUserAttribute` Parametreleri

| Parametre    | Tip    | Varsayılan | Açıklama                                            |
|--------------|--------|------------|-----------------------------------------------------|
| maxLength    | int    | 255        | String alanların maksimum uzunluğu                  |
| isRequired   | bool   | false      | Zorunlu alan mı?                                    |
| isUnique     | bool   | false      | Benzersiz bir alan mı?                              |
| defaultValue | string | null       | Varsayılan değer                                    |
| description  | string | null       | Alan açıklaması                                     |
| order        | int    | 0          | Sıralama değeri                                     |
| dbType       | string | null       | Veritabanı alan tipi (örn: "nvarchar", "datetime2")  |

---

## API Kullanımı

### Kullanıcı Kaydı

```csharp
public ActionResult Register(RegisterViewModel model)
{
    try
    {
        var userDto = new UserForRegisterDto
        {
            Email = model.Email,
            Password = model.Password,
            FirstName = model.FirstName,
            LastName = model.LastName,
            LocalityId = "1" // İsteğe bağlı, şube/lokasyon bilgisi
        };
        
        // Kullanıcıyı kaydet
        var user = _authService.Register(userDto);
        
        // İsteğe bağlı: Kullanıcıya rol atama
        _authService.AddClaim(user.Id, "User");
        
        // Kullanıcı kaydedildikten sonra giriş için token al
        var loginDto = new UserForLoginDto { Email = model.Email, Password = model.Password };
        var token = _authService.Login(loginDto);
        
        return Json(new { success = true, token = token });
    }
    catch (Exception ex)
    {
        return Json(new { success = false, message = ex.Message });
    }
}
```

### Kullanıcı Girişi

```csharp
public ActionResult Login(LoginViewModel model)
{
    try
    {
        var loginDto = new UserForLoginDto
        {
            Email = model.Email,
            Password = model.Password
        };
        
        var token = _authService.Login(loginDto);
        
        if (token != null)
        {
            // Session'a token bilgisini kaydet (isteğe bağlı)
            Session["AccessToken"] = token.Token;
            Session["RefreshToken"] = token.RefreshToken;
            
            return Json(new {
                success = true,
                token = token.Token,
                refreshToken = token.RefreshToken,
                expiration = token.Expiration
            });
        }
        
        return Json(new { success = false, message = "Geçersiz kullanıcı adı veya şifre" });
    }
    catch (Exception ex)
    {
        return Json(new { success = false, message = ex.Message });
    }
}
```

### Token Yenileme

```csharp
public ActionResult RefreshToken(string refreshToken)
{
    try
    {
        var tokenDto = _authService.RefreshToken(refreshToken);
        
        if (tokenDto != null)
        {
            return Json(new {
                success = true,
                token = tokenDto.Token,
                refreshToken = tokenDto.RefreshToken,
                expiration = tokenDto.Expiration
            });
        }
        
        return Json(new { success = false, message = "Geçersiz yenileme jetonu" });
    }
    catch (Exception ex)
    {
        return Json(new { success = false, message = ex.Message });
    }
}
```

### Yetkilendirme

OWIN middleware kullanarak API rotalarınızı yetkilendirmek için:

```csharp
// Startup.cs veya OWIN yapılandırma dosyasında
public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        // JWT kimlik doğrulama ayarları
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = ConfigurationManager.AppSettings["Jwt:Issuer"],
            ValidAudience = ConfigurationManager.AppSettings["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["Jwt:SecurityKey"]))
        };
        
        // OWIN middleware yapılandırması
        app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
        {
            TokenValidationParameters = tokenValidationParameters
        });
        
        // Web API yapılandırması
        var config = new HttpConfiguration();
        WebApiConfig.Register(config);
        app.UseWebApi(config);
    }
}
```

Controller seviyesinde yetkilendirme:

```csharp
[Authorize]
public class SecureApiController : ApiController
{
    [HttpGet]
    public IHttpActionResult GetProtectedData()
    {
        return Ok(new { message = "Bu veri korunuyor!" });
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IHttpActionResult GetAdminOnlyData()
    {
        return Ok(new { message = "Bu veri sadece Admin rolüne sahip kullanıcılar için!" });
    }
}
```

---

## Gelişmiş Özellikler

### Özel Rol Yönetimi

```csharp
// Yeni rol oluşturma
public void CreateRole(string roleName)
{
    var operationClaim = new OperationClaim { Name = roleName };
    _operationClaimRepository.Add(operationClaim);
}

// Kullanıcıya rol atama
public void AssignRoleToUser(int userId, string roleName)
{
    var user = _userRepository.Get(u => u.Id == userId);
    var role = _operationClaimRepository.Get(r => r.Name == roleName);
    
    if (user != null && role != null)
    {
        var userOperationClaim = new UserOperationClaim
        {
            UserId = user.Id,
            OperationClaimId = role.Id
        };
        
        _userOperationClaimRepository.Add(userOperationClaim);
    }
}
```

### Kullanıcı Profili Genişletme

Önceden tanımladığınız genişletilmiş alanlarla kullanıcı profili yönetimi:

```csharp
// Genişletilmiş kullanıcı bilgilerini güncelleme
public void UpdateUserProfile(ExtendedUser user)
{
    var existingUser = _userRepository.Get(u => u.Id == user.Id);
    if (existingUser != null)
    {
        // Temel User özelliklerini güncelle
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Email = user.Email;
        // ... diğer temel özellikler
        
        // Genişletilmiş özellikleri reflection ile güncelle
        foreach (var property in typeof(ExtendedUser).GetProperties())
        {
            if (property.IsDefined(typeof(ExtendUserAttribute), false))
            {
                var value = property.GetValue(user);
                property.SetValue(existingUser, value);
            }
        }
        
        _userRepository.Update(existingUser);
    }
}
```

### Kullanıcı Listesi Sorgulama

```csharp
// Aktif kullanıcıları listeleme
public List<User> GetActiveUsers()
{
    return _userRepository.GetList(u => u.IsActive && !u.IsDeleted);
}

// Belirli bir role sahip kullanıcıları listeleme
public List<User> GetUsersInRole(string roleName)
{
    var role = _operationClaimRepository.Get(r => r.Name == roleName);
    if (role != null)
    {
        return _userRepository.GetList(
            u => u.UserOperationClaims.Any(uoc => uoc.OperationClaimId == role.Id && !u.IsDeleted)
        );
    }
    return new List<User>();
}
```

---

## Örnekler

### Web MVC Projesi Entegrasyonu

```csharp
// Global.asax.cs
protected void Application_Start()
{
    AreaRegistration.RegisterAllAreas();
    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
    RouteConfig.RegisterRoutes(RouteTable.Routes);
    BundleConfig.RegisterBundles(BundleTable.Bundles);
    
    // OrhAuth yapılandırması
    var authOptions = new OrhAuthOptions
    {
        ConnectionString = ConfigurationManager.ConnectionStrings["AuthDbConnection"].ConnectionString,
        CreateDatabaseIfNotExists = true
        // Diğer ayarlar varsayılan değerlerle kullanılacak
    };
    
    // Auth servisini yapılandır ve global olarak kaydet
    GlobalVariables.AuthService = OrhAuthExtensions.ConfigureOrhAuth(authOptions);
}

// GlobalVariables.cs
public static class GlobalVariables
{
    public static IAuthService AuthService { get; set; }
}
```

### Web API Projesi Entegrasyonu

```csharp
// OWIN Startup.cs
public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        // OrhAuth yapılandırma
        var authOptions = new OrhAuthOptions
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["AuthDbConnection"].ConnectionString,
            CreateDatabaseIfNotExists = true,
            TokenSecurityKey = ConfigurationManager.AppSettings["Jwt:SecurityKey"],
            TokenIssuer = ConfigurationManager.AppSettings["Jwt:Issuer"],
            TokenAudience = ConfigurationManager.AppSettings["Jwt:Audience"],
            TokenExpirationMinutes = int.Parse(ConfigurationManager.AppSettings["Jwt:AccessTokenExpiration"])
        };
        
        // Auth servisini yapılandır
        var authService = OrhAuthExtensions.ConfigureOrhAuth(authOptions);
        
        // DI Container'a kayıt
        var container = new UnityContainer();
        container.RegisterInstance<IAuthService>(authService);
        
        // Web API Yapılandırma
        HttpConfiguration config = new HttpConfiguration();
        config.DependencyResolver = new UnityResolver(container);
        
        // Routing yapılandırma
        config.MapHttpAttributeRoutes();
        config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
        );
        
        // OWIN auth middleware
        app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
        {
            AuthenticationMode = AuthenticationMode.Active,
            TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = ConfigurationManager.AppSettings["Jwt:Issuer"],
                ValidAudience = ConfigurationManager.AppSettings["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["Jwt:SecurityKey"]))
            }
        });
        
        app.UseWebApi(config);
    }
}
```

---

## SSS

### Veritabanı nasıl oluşturulur?
OrhAuth, ilk kullanımda veritabanını otomatik olarak oluşturur *(CreateDatabaseIfNotExists = true ayarı ile)*. Bu, Entity Framework'ün Code-First yaklaşımını kullanır.

### Özel kullanıcı alanları nasıl eklenir?
`ExtendUserAttribute` özniteliğini kullanarak **User** sınıfını genişleten bir sınıf oluşturun. OrhAuth, bu alanları otomatik olarak veritabanına ekleyecektir.

### Token süreleri nasıl ayarlanır?
Token süreleri web.config'de veya **OrhAuthOptions** sınıfında belirtilebilir:
- **TokenExpirationMinutes**: Access token süresi *(dakika cinsinden)*
- **RefreshTokenTTL**: Refresh token süresi *(gün cinsinden)*

### Birden fazla uygulama aynı OrhAuth veritabanını kullanabilir mi?
Evet, aynı bağlantı dizesini kullanarak birden fazla uygulama aynı kimlik doğrulama sistemine bağlanabilir.
