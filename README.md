# 1. Introduction

## 1.1. Scope and Purpose

**OrhAuth** is a comprehensive authentication and authorization library designed for modern applications. This library allows you to easily add user management, authentication, and authorization to your application.

For every application today, you need to answer the questions:  
**"Who is this user?"** and **"What can this user do?"**  
**OrhAuth** exists to solve exactly these problems. Just like a security guard, it controls who can log into your system and what actions they can take.

The main purposes of OrhAuth are to:

- **Simplify user management:** Provides ready-made processes such as registration, login, and password reset.
- **Provide secure authentication:** Offers secure session management with JWT (JSON Web Token) technology.
- **Provide flexible authorization:** With role and authorization-based access control, you can easily determine who can do what.
- **Offer an extensible structure:** Allows you to extend the standard user model with your own custom fields.

---

## 1.2. Target Audience

OrhAuth is intended for the following people and groups:

- **.NET developers:** Developers looking for a quick solution to build an authentication and authorization system.
- **System architects:** Architects who want to design a secure and consistent identity infrastructure.
- **Software teams:** Teams that want to establish standard security practices.
- **Project managers:** Managers who want to meet security requirements and reduce development time.

Regardless of your technical level, this documentation is written to help you understand OrhAuth.

---

## 1.3. System Requirements

To use the OrhAuth library, you need the following requirements:

- **.NET Framework** 4.6.1+ or **.NET Core** 2.1+ or **.NET** 5.0+
- **SQL Server** 2012+ (for database)
- **Entity Framework** 6+ (for data access)

You can use one of the following development environments:

- **Visual Studio 2019+** (recommended)
- **Visual Studio Code** + .NET SDK
- **JetBrains Rider**

### Installation

Install via **NuGet Package Manager**:

```
Install-Package OrhAuth
```

Or using the **.NET CLI**:

```
dotnet add package OrhAuth
```

---

## 1.4. Terminology and Abbreviations

Here are the key terms and abbreviations used in the OrhAuth documentation:

| Term                  | Description                                                                 |
|-----------------------|-----------------------------------------------------------------------------|
| **Authentication**    | The process of verifying who a user is.                                     |
| **Authorization**     | Determines whether an authenticated user can access a particular resource. |
| **JWT (JSON Web Token)** | A digitally signed token format used for securely transporting credentials. |
| **Refresh Token**     | A token used to obtain a new access token, usually with longer lifespan.   |
| **RBAC**              | Role-Based Access Control. Users are authorized according to their roles.   |
| **Claim**             | A key-value pair with user info (e.g., email, roles).                       |
| **ExtendedUser**      | A custom user class extending OrhAuth’s standard user model.                |
| **OperationClaim**    | Represents an action that can be performed in the system.                   |
| **UserOperationClaim**| Represents an operation claim assigned to a specific user.                  |

> These terms will be used throughout the documentation. You can always return to this section for reference.


---



# 2. Overview

## 2.1. What is OrhAuth?

**OrhAuth** is a JWT (JSON Web Token) based authentication and authorization library developed for .NET Framework projects.  
Using **Entity Framework 6**, it centrally manages user management, role-based authorization, and token management.

### Main Components

```csharp
// OrhAuth main components
IAuthService         // Authentication service interface
AuthManager          // IAuthService implementation 
ITokenHelper         // Token creation and validation interface
JwtHelper            // ITokenHelper implementation
AuthDbContext        // Entity Framework DbContext class
```

### Key Capabilities

1. Database management with Entity Framework Code-First approach  
2. JWT-based token generation and validation  
3. Role-based authorization (RBAC)  
4. Long sessions with Refresh Token support  
5. Extensible user model  

These features are bundled into a single library to accelerate development.

---

## 2.2. Architectural Structure

OrhAuth has a **layered architecture** and adheres to **SOLID principles**. Each layer has a clear responsibility and communicates through interfaces.

### Data Layer

```csharp
// Data Layer main components
AuthDbContext                      // Entity Framework DbContext
IEntityRepository<T>              // Generic repository interface
EfEntityRepositoryBase<T>         // Repository implementation
EntityConfigurations              // Fluent mapping classes
```

Handles data operations via EF6 and manages schema using Code-First.

### Business Logic Layer

```csharp
// Business Logic Layer main components
IAuthService                      // Authentication service interface
AuthManager                       // Service implementation
ITokenHelper                      // Token operations interface
JwtHelper                         // Token operations implementation
HashingHelper                     // Password hashing helper
```

Implements registration, login, token creation, etc.

### Model Layer

```csharp
// Model Layer main components
User                              // User entity class
OperationClaim                    // Authorization entity
UserOperationClaim                // User-Authorization mapping
RefreshToken                      // Refresh token class
AccessToken                       // JWT DTO
UserForRegisterDto, UserForLoginDto // DTOs
```

Contains entities and data transfer objects.

### Configuration Layer

```csharp
// Configuration Layer main components
OrhAuthOptions                    // Configuration class
AuthFrameworkInitializer         // Startup and setup logic
SchemaMetadataCache              // Extended model cache
```

Manages initialization and options setup.

---

## 2.3. Key Features

### User Management

```csharp
// Registration
var userForRegister = new UserForRegisterDto {
    Email = "user@example.com",
    Password = "SecureP@ssw0rd",
    FirstName = "John",
    LastName = "Doe"
};
User newUser = _authService.Register(userForRegister);

// Login
var userForLogin = new UserForLoginDto {
    Email = "user@example.com",
    Password = "SecureP@ssw0rd"
};
var accessToken = _authService.Login(userForLogin);
```

Use `accessToken.Token` and `accessToken.RefreshToken`.

### Extensible User Model

```csharp
public class ExtendedUser : User
{
    [ExtendUser]
    public DateTime BirthDate { get; set; }

    [ExtendUser]
    public string PhoneNumber { get; set; }

    [ExtendUser]
    public string Address { get; set; }

    [ExtendUser]
    public bool ReceiveNewsletter { get; set; }
}
```

Register with OrhAuth:

```csharp
var options = new OrhAuthOptions {
    ConnectionString = "your_connection_string",
    ExtendedUserType = typeof(ExtendedUser)
};
IAuthService authService = OrhAuthExtensions.ConfigureOrhAuth(options);
```

### Authorization Procedures

```csharp
// Create a role
var adminRole = new OperationClaim { Name = "Admin" };
_operationClaimRepository.Add(adminRole);

// Assign role to user
var userRole = new {
    UserId = user.Id,
    OperationClaimId = adminRole.Id
};
_userOperationClaimRepository.Add(userRole);

// Check roles
var userClaims = _authService.GetClaims(user);
bool isAdmin = userClaims.Any(c => c.Name == "Admin");
```

### Token Management

```csharp
var claims = _authService.GetClaims(user);
var accessToken = _tokenHelper.CreateToken(user, claims);

// Refresh token
var refreshToken = _authService.RefreshToken(oldRefreshToken);

// Validate
bool isValid = _tokenHelper.ValidateToken(token);
```

---

## 2.4. Integration with Other Systems

OrhAuth can be integrated easily into various types of .NET Framework applications.

### ASP.NET MVC Application Integration

```csharp
// In Global.asax.cs
protected void Application_Start()
{
    AreaRegistration.RegisterAllAreas();
    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
    RouteConfig.RegisterRoutes(RouteTable.Routes);
    BundleConfig.RegisterBundles(BundleTable.Bundles);

    // OrhAuth configuration
    var authOptions = new OrhAuthOptions
    {
        ConnectionString = ConfigurationManager.ConnectionStrings["AuthDbConnection"].ConnectionString,
        TokenSecurityKey = ConfigurationManager.AppSettings["Jwt:SecurityKey"],
        TokenIssuer = ConfigurationManager.AppSettings["Jwt:Issuer"],
        TokenAudience = ConfigurationManager.AppSettings["Jwt:Audience"],
        TokenExpirationMinutes = int.Parse(ConfigurationManager.AppSettings["Jwt:AccessTokenExpiration"]),
        RefreshTokenTTLDays = int.Parse(ConfigurationManager.AppSettings["Jwt:RefreshTokenTTL"])
    };

    GlobalVariables.AuthService = OrhAuthExtensions.ConfigureOrhAuth(authOptions);
}
```

### Web API Application Integration with OWIN

```csharp
// OWIN Startup.cs
public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        var config = new HttpConfiguration();
        config.MapHttpAttributeRoutes();
        config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
        );

        var authOptions = new OrhAuthOptions
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["AuthDbConnection"].ConnectionString
        };

        var authService = OrhAuthExtensions.ConfigureOrhAuth(authOptions);

        var container = new UnityContainer();
        container.RegisterInstance<IAuthService>(authService);
        config.DependencyResolver = new UnityResolver(container);

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

### Windows Forms / WPF Application Integration

```csharp
// Program.cs or App.xaml.cs
private static IAuthService _authService;
public static void Main()
{
    var authOptions = new OrhAuthOptions
    {
        ConnectionString = ConfigurationManager.ConnectionStrings["AuthDbConnection"].ConnectionString
    };

    _authService = OrhAuthExtensions.ConfigureOrhAuth(authOptions);

    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    Application.Run(new LoginForm(_authService));
}
```

OrhAuth supports storing JWT tokens on the client side and using them in API requests via `Authorization: Bearer {token}` header. It also supports refresh token mechanisms for token renewal.

---

## OrhAuthOptions - All Configuration Settings

### Settings Overview

- `ConnectionString`
- `CreateDatabaseIfNotExists` (default: `true`)
- `TokenSecurityKey`
- `TokenIssuer`
- `TokenAudience`
- `TokenExpirationMinutes` (default: `30`)
- `RefreshTokenTTLDays` (default: `7`)
- `AddDefaultAdmin` (default: `true`)
- `DefaultAdminEmail` (default: `"admin@example.com"`)
- `DefaultAdminPassword` (default: `"Admin123!"`)
- `ExtendedUserType`

### Sample Configuration

```csharp
var authOptions = new OrhAuthOptions
{
    ConnectionString = ConfigurationManager.ConnectionStrings["AuthDbConnection"].ConnectionString,
    CreateDatabaseIfNotExists = true,
    TokenSecurityKey = ConfigurationManager.AppSettings["Jwt:SecurityKey"],
    TokenIssuer = ConfigurationManager.AppSettings["Jwt:Issuer"],
    TokenAudience = ConfigurationManager.AppSettings["Jwt:Audience"],
    TokenExpirationMinutes = int.Parse(ConfigurationManager.AppSettings["Jwt:AccessTokenExpiration"]),
    RefreshTokenTTLDays = int.Parse(ConfigurationManager.AppSettings["Jwt:RefreshTokenTTL"]),
    AddDefaultAdmin = true,
    DefaultAdminEmail = "yonetici@sirketim.com",
    DefaultAdminPassword = "Guclu$PasswordDetermine2024!",
    ExtendedUserType = typeof(MyCompany.Models.ExtendedUser)
};
```

### Example Web.config

```xml
<configuration>
  <connectionStrings>
    <add name="AuthDbConnection"
         connectionString="Data Source=SQLSERVER;Initial Catalog=OrhAuthDB;User ID=dbuser;Password=dbpassword;"
         providerName="System.Data.SqlClient" />
  </connectionStrings>

  <appSettings>
    <add key="Jwt:SecurityKey" value="long_ve_guvenli_guvenli_bir_anahtar_ornegi_1234567890_ABCDEFG" />
    <add key="Jwt:Issuer" value="https://api.sirketim.com" />
    <add key="Jwt:Audience" value="https://uygulamam.sirketim.com" />
    <add key="Jwt:AccessTokenExpiration" value="30" />
    <add key="Jwt:RefreshTokenTTL" value="7" />
    <add key="Application:Name" value="Company Management System" />
    <add key="Application:Version" value="1.0.0" />
  </appSettings>
</configuration>
```

---

## Typical Use Cases

### 1. Basic Configuration (Minimum Requirements)

```csharp
var authOptions = new OrhAuthOptions
{
    ConnectionString = ConfigurationManager.ConnectionStrings["AuthDbConnection"].ConnectionString,
    TokenSecurityKey = ConfigurationManager.AppSettings["Jwt:SecurityKey"]
};
IAuthService authService = OrhAuthExtensions.ConfigureOrhAuth(authOptions);
```

---

### 2. Configuration with Extended User Model

```csharp
public class CustomUser : User
{
    [ExtendUser(maxLength: 100)]
    public string Department { get; set; }

    [ExtendUser]
    public DateTime BirthDate { get; set; }

    [ExtendUser(isUnique: true)]
    public string EmployeeId { get; set; }
}
```

```csharp
var authOptions = new OrhAuthOptions
{
    ConnectionString = ConfigurationManager.ConnectionStrings["AuthDbConnection"].ConnectionString,
    TokenSecurityKey = ConfigurationManager.AppSettings["Jwt:SecurityKey"],
    TokenExpirationMinutes = 60,
    ExtendedUserType = typeof(CustomUser)
};
IAuthService authService = OrhAuthExtensions.ConfigureOrhAuth(authOptions);
```

---

### 3. Security-Oriented Configuration (Production)

```csharp
var authOptions = new OrhAuthOptions
{
    ConnectionString = ConfigurationManager.ConnectionStrings["AuthDbConnection"].ConnectionString,
    CreateDatabaseIfNotExists = false,
    TokenSecurityKey = ConfigurationManager.AppSettings["Jwt:SecurityKey"],
    TokenIssuer = ConfigurationManager.AppSettings["Jwt:Issuer"],
    TokenAudience = ConfigurationManager.AppSettings["Jwt:Audience"],
    TokenExpirationMinutes = 15,
    RefreshTokenTTLDays = 1,
    AddDefaultAdmin = false
};
IAuthService authService = OrhAuthExtensions.ConfigureOrhAuth(authOptions);
```

---

These configuration options allow you to flexibly adapt OrhAuth to different environments and requirements, including development, staging, and production.

> ✅ You can now use OrhAuth to build secure, token-based authentication systems with minimal effort while maintaining scalability and extensibility.

---




# 3. Start

## 3.1. Installation Steps

Follow these steps to integrate the OrhAuth library into your .NET Framework project.

### NuGet Package Installation

You can install OrhAuth via the **NuGet Package Manager Console**:

```powershell
Install-Package OrhAuth
```

Alternatively:

**Right-click on Project > Manage NuGet Packages > Search "OrhAuth"**

### Automatic Loading of Dependencies

The following NuGet packages are automatically installed:

- `EntityFramework` (6.x)
- `System.IdentityModel.Tokens.Jwt` (5.3.0+)
- `Microsoft.IdentityModel.Tokens`
- `Newtonsoft.Json`

If any issue occurs, you can manually install them.

---

## 3.2. Package References

OrhAuth adds the following references to your project:


```xml
<Reference Include="EntityFramework" />
<Reference Include="System.IdentityModel.Tokens.Jwt" />
<Reference Include="Microsoft.IdentityModel.Tokens" />
<Reference Include="System.Web" />
<Reference Include="System.Data" />
<Reference Include="System.Security" />
<Reference Include="System.Runtime.Serialization" />
```

These are required for EF, JWT handling, and other features.

---

## 3.3. Basic Configuration

### Option 1: Configuration via Web.config / App.config

**Web.config:**

```xml
<configuration>
  <connectionStrings>
    <add name="AuthDbConnection"
         connectionString="Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=OrhAuthDb;Integrated Security=True"
         providerName="System.Data.SqlClient" />
  </connectionStrings>

  <appSettings>
    <add key="Jwt:SecurityKey" value="write_here_a_strong_security_key_yazin_en_az_32_characters_must_be" />
    <add key="Jwt:Issuer" value="https://sizin-siteniz.com" />
    <add key="Jwt:Audience" value="https://sizin-api-adresiniz.com" />
    <add key="Jwt:AccessTokenExpiration" value="60" />
    <add key="Jwt:RefreshTokenTTL" value="7" />
  </appSettings>
</configuration>
```

**C# Code:**

```csharp
var authOptions = new OrhAuthOptions
{
    ConnectionString = ConfigurationManager.ConnectionStrings["AuthDbConnection"].ConnectionString,
    CreateDatabaseIfNotExists = true
};
IAuthService authService = OrhAuthExtensions.ConfigureOrhAuth(authOptions);
```

### Option 2: Direct Configuration in Code

```csharp
var authOptions = new OrhAuthOptions
{
    ConnectionString = "Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=OrhAuthDb;Integrated Security=True",
    CreateDatabaseIfNotExists = true,
    TokenSecurityKey = "write_a_strong_security_key_here_en_az_az_32_character_must_have",
    TokenIssuer = "https://sizin-siteniz.com",
    TokenAudience = "https://sizin-api-adresiniz.com",
    TokenExpirationMinutes = 60,
    RefreshTokenTTLDays = 7,
    AddDefaultAdmin = true,
    DefaultAdminEmail = "admin@example.com",
    DefaultAdminPassword = "Admin123!"
};
IAuthService authService = OrhAuthExtensions.ConfigureOrhAuth(authOptions);
```

### AuthService Retention for Global Access

```csharp
public static class GlobalServices
{
    public static IAuthService AuthService { get; set; }
}
GlobalServices.AuthService = OrhAuthExtensions.ConfigureOrhAuth(authOptions);
var token = GlobalServices.AuthService.Login(userForLogin);
```

---

## 3.4. Database Configuration

OrhAuth uses Entity Framework Code-First approach.

### Automatic Database Creation

```csharp
var authOptions = new OrhAuthOptions
{
    ConnectionString = ConfigurationManager.ConnectionStrings["AuthDbConnection"].ConnectionString,
    CreateDatabaseIfNotExists = true
};
```

Creates schema on first run:

- Creates tables: `Users`, `OperationClaims`, `UserOperationClaims`, `RefreshTokens`
- Adds default roles: Admin, User
- Creates admin user if `AddDefaultAdmin = true`

### Creating Database with Extended User

```csharp
public class ExtendedUser : User
{
    [ExtendUser(maxLength: 100)]
    public string Department { get; set; }

    [ExtendUser]
    public DateTime BirthDate { get; set; }

    [ExtendUser(isUnique: true)]
    public string EmployeeId { get; set; }
}

var authOptions = new OrhAuthOptions
{
    ConnectionString = ConfigurationManager.ConnectionStrings["AuthDbConnection"].ConnectionString,
    CreateDatabaseIfNotExists = true,
    ExtendedUserType = typeof(ExtendedUser)
};
IAuthService authService = OrhAuthExtensions.ConfigureOrhAuth(authOptions);
```

This automatically adds custom fields to the `Users` table.

### Manual Database Configuration

```csharp
var authOptions = new OrhAuthOptions
{
    ConnectionString = ConfigurationManager.ConnectionStrings["AuthDbConnection"].ConnectionString,
    CreateDatabaseIfNotExists = false
};
```

In this case, you must create the schema manually or use migration.

---

### Sample Database Schema

| Table              | Description                                                  |
|-------------------|--------------------------------------------------------------|
| `Users`           | Id, Email, FirstName, LastName, PasswordHash, IsActive       |
| `OperationClaims` | Roles/permissions in system                                  |
| `UserOperationClaims` | Role assignments to users                              |
| `RefreshTokens`   | Stores token renewal data                                     |

> ExtendedUser adds additional fields to the `Users` table.


---


# 4. User Management

## 4.1. User Model

OrhAuth provides a flexible user management system. The base `User` class includes:

```csharp
public class User : EntityBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    public bool IsActive { get; set; }

    // Navigation properties
    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
}
```

Passwords are hashed and salted, never stored as plain text.

---

## 4.2. User Registration

```csharp
var userForRegister = new UserForRegisterDto
{
    Email = "kullanici@ornek.com",
    Password = "Guvenli$ifre123",
    FirstName = "Ahmet",
    LastName = "Yilmaz"
};

var user = _authService.Register(userForRegister);

// Or with token
var accessToken = _authService.RegisterWithToken(userForRegister);
```

### Registration Flow

- Email check
- Password complexity check
- Hash and salt generation
- Save to DB
- Assign default "User" role

---

## 4.3. User Login

```csharp
var userForLogin = new UserForLoginDto
{
    Email = "kullanici@ornek.com",
    Password = "Guvenli$ifre123"
};

var accessToken = _authService.Login(userForLogin);
```

### Login Flow

- Find user by email
- Check if active
- Verify password
- Create JWT and Refresh token
- Add claims to token

---

## 4.4. Refresh Token Usage

```csharp
string refreshToken = "previously_received_refresh_token";
var newAccessToken = _authService.RefreshToken(refreshToken);
```

A new access and refresh token is returned if valid.

Invalid if:

- Expired
- User logs out
- Manually revoked (e.g., password change)

---

## 4.5. Retrieving User Information

```csharp
User userById = _authService.GetUserById(userId);
User userByEmail = _authService.GetUserByEmail(email);
```

---

## 4.6. Password Procedures

### Change Password

```csharp
bool success = _authService.ChangePassword(
    userId,
    oldPassword: "OldPassword123",
    newPassword: "NewPassword456"
);
```

### Admin Reset

```csharp
bool success = _authService.ResetPassword(
    userId,
    newPassword: "TemporaryPassword789"
);
```

### Password Hashing

```csharp
HashingHelper.CreatePasswordHash("Secure$password123", out var hash, out var salt);
bool isValid = HashingHelper.VerifyPasswordHash("Secure$password123", hash, salt);
```

---

## 4.7. User Status Management

```csharp
_authService.ActivateUser(userId);
_authService.DeactivateUser(userId);
```

---

## 4.8. Extended User Information

```csharp
public class ExtendedUser : User
{
    [ExtendUser(maxLength: 20, isRequired: true)]
    public string PhoneNumber { get; set; }

    [ExtendUser]
    public DateTime BirthDate { get; set; }

    [ExtendUser]
    public string Address { get; set; }

    [ExtendUser(isUnique: true)]
    public string IdentityNumber { get; set; }
}
```

### Access Extended Fields

```csharp
ExtendedUser extendedUser = _authService.GetUserById(userId) as ExtendedUser;
if (extendedUser != null)
{
    var phone = extendedUser.PhoneNumber;
    var birth = extendedUser.BirthDate;
}
```

### ExtendUser Attribute Options

```csharp
[ExtendUser(
    maxLength: 100,
    isRequired: true,
    isUnique: false,
    dbType: "nvarchar",
    description: "Description"
)]
```

---

## 4.9. Claims and Token Content

```csharp
var claims = _authService.GetClaims(user);
var accessToken = _tokenHelper.CreateToken(user, claims);
```

### Default Claims

- `email`
- `name`
- `sub`
- `role`

### Custom Claims

```csharp
public class ExtendedUser : User
{
    [ExtendUser]
    [AddToClaim(claimType: "phone_number")]
    public string PhoneNumber { get; set; }

    [ExtendUser]
    [AddToClaim(claimType: "birth_date", valueFormat: "yyyy-MM-dd")]
    public DateTime BirthDate { get; set; }
}
```

These will be included in JWT and accessible on the client side.



---


# 5. Authorization System

## 5.1. Overview

OrhAuth uses **Role-Based Access Control (RBAC)**. Users are granted permissions through roles.

**Core components:**

- `OperationClaim`: Defines roles/permissions (e.g. Admin, User)
- `UserOperationClaim`: User-role assignment
- JWT Claims: Roles are stored in tokens for each request

---

## 5.2. Basic Concepts

### `OperationClaim`

```csharp
public class OperationClaim : EntityBase
{
    public string Name { get; set; }
    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
}
```

Default roles:

- **Admin**: Full system access
- **User**: Standard access

### `UserOperationClaim`

```csharp
public class UserOperationClaim : EntityBase
{
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }

    public virtual User User { get; set; }
    public virtual OperationClaim OperationClaim { get; set; }
}
```

Users can have multiple roles.

---

## 5.3. Role Management

### Creating Roles

```csharp
var managerRole = new OperationClaim { Name = "Manager" };
var editorRole = new OperationClaim { Name = "Editor" };
_operationClaimRepository.Add(managerRole);
_operationClaimRepository.Add(editorRole);
```

### Listing Roles

```csharp
var allRoles = _operationClaimRepository.GetList();
var adminRole = _operationClaimRepository.Get(r => r.Name == "Admin");
```

### Updating & Deleting Roles

```csharp
var role = _operationClaimRepository.Get(r => r.Id == roleId);
role.Name = "NewRoleName";
_operationClaimRepository.Update(role);
_operationClaimRepository.Delete(role);
```

---

## 5.4. Assigning Roles to Users

```csharp
var userRole = new UserOperationClaim
{
    UserId = userId,
    OperationClaimId = roleId
};
_userOperationClaimRepository.Add(userRole);
```

### Assigning Multiple Roles

```csharp
var adminRole = _operationClaimRepository.Get(r => r.Name == "Admin");
var editorRole = _operationClaimRepository.Get(r => r.Name == "Editor");

_userOperationClaimRepository.Add(new UserOperationClaim
{
    UserId = userId,
    OperationClaimId = adminRole.Id
});

_userOperationClaimRepository.Add(new UserOperationClaim
{
    UserId = userId,
    OperationClaimId = editorRole.Id
});
```

---

## 5.5. Managing User Roles

### Get User Roles

```csharp
var userClaims = _authService.GetClaims(user);

var userRoles = _userOperationClaimRepository
    .GetList(uc => uc.UserId == userId)
    .Select(uc => uc.OperationClaim)
    .ToList();
```

### Remove Specific Role

```csharp
var userRole = _userOperationClaimRepository.Get(
    ur => ur.UserId == userId && ur.OperationClaimId == roleId);
if (userRole != null)
{
    _userOperationClaimRepository.Delete(userRole);
}
```

### Remove All Roles

```csharp
var userRoles = _userOperationClaimRepository.GetList(ur => ur.UserId == userId);
foreach (var role in userRoles)
{
    _userOperationClaimRepository.Delete(role);
}
```

---

## 5.6. Migration of Roles in JWT Token

```csharp
public List<OperationClaim> GetClaims(User user)
{
    using (var context = new AuthDbContext(_connectionString))
    {
        var result = from operationClaim in context.OperationClaims
                     join userOperationClaim in context.UserOperationClaims
                     on operationClaim.Id equals userOperationClaim.OperationClaimId
                     where userOperationClaim.UserId == user.Id
                     select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
        return result.ToList();
    }
}
```

### JWT Claim Injection

```csharp
private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
{
    var claims = new List<Claim>();
    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
    claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
    claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));

    operationClaims.ForEach(role =>
        claims.Add(new Claim(ClaimTypes.Role, role.Name)));

    return claims;
}
```

---

## 5.7. Authority Control

### ASP.NET MVC

```csharp
[Authorize(Roles = "Admin")]
public ActionResult AdminPanel() => View();

[Authorize(Roles = "Admin,Manager")]
public ActionResult Reports() => View();
```

### Web API

```csharp
[Authorize(Roles = "Editor")]
[HttpPost]
public IHttpActionResult CreateContent(ContentModel model) => Ok();
```

### In-Code Check

```csharp
bool isAdmin = User.IsInRole("Admin");
if (!isAdmin)
    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
```

---

## 5.8. Creating a Custom Role System

### Hierarchical Role System

```csharp
public class RoleHierarchy
{
    private readonly Dictionary<string, List<string>> _roleHierarchy = new()
    {
        ["Admin"] = new() { "Manager", "Editor", "User" },
        ["Manager"] = new() { "Editor", "User" },
        ["Editor"] = new() { "User" },
        ["User"] = new()
    };

    public bool HasRole(List<string> userRoles, string requiredRole)
    {
        if (userRoles.Contains(requiredRole)) return true;

        foreach (var role in userRoles)
        {
            if (_roleHierarchy.ContainsKey(role) && CheckRoleHierarchy(role, requiredRole))
                return true;
        }

        return false;
    }

    private bool CheckRoleHierarchy(string userRole, string requiredRole)
    {
        if (_roleHierarchy[userRole].Contains(requiredRole)) return true;

        foreach (var role in _roleHierarchy[userRole])
        {
            if (CheckRoleHierarchy(role, requiredRole)) return true;
        }

        return false;
    }
}
```

### Using Hierarchical Control

```csharp
var roleHierarchy = new RoleHierarchy();
var userRoles = _authService.GetClaims(user).Select(c => c.Name).ToList();

if (roleHierarchy.HasRole(userRoles, "Editor"))
{
    // Proceed
}
```

### Permission Based Access Control

```csharp
var permissions = new List<OperationClaim>
{
    new() { Name = "content.create" },
    new() { Name = "content.read" },
    new() { Name = "content.update" },
    new() { Name = "content.delete" },
    new() { Name = "user.manage" }
};

foreach (var permission in permissions)
    _operationClaimRepository.Add(permission);

// Assign permission
_userOperationClaimRepository.Add(new UserOperationClaim
{
    UserId = userId,
    OperationClaimId = permissions.First(p => p.Name == "content.read").Id
});
```

Permission Check:

```csharp
var userPermissions = _authService.GetClaims(user);
bool canCreateContent = userPermissions.Any(p => p.Name == "content.create");
```

---

## 5.9. Sample Scenarios

### Example 1: E-Commerce Roles

```csharp
var roles = new List<OperationClaim>
{
    new() { Name = "Admin" },
    new() { Name = "StoreManager" },
    new() { Name = "CustomerSupport" },
    new() { Name = "ContentEditor" },
    new() { Name = "Customer" }
};

foreach (var role in roles)
    _operationClaimRepository.Add(role);

var employee = _authService.GetUserByEmail("calisan@ornek.com");
var managerRole = _operationClaimRepository.Get(r => r.Name == "StoreManager");
var supportRole = _operationClaimRepository.Get(r => r.Name == "CustomerSupport");

_userOperationClaimRepository.Add(new UserOperationClaim
{
    UserId = employee.Id,
    OperationClaimId = managerRole.Id
});

_userOperationClaimRepository.Add(new UserOperationClaim
{
    UserId = employee.Id,
    OperationClaimId = supportRole.Id
});
```

### Example 2: Controller + Method Level

```csharp
[Authorize(Roles = "Admin,StoreManager")]
public class ProductsController : ApiController
{
    [HttpGet]
    public IHttpActionResult GetProducts() => Ok();

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IHttpActionResult CreateProduct(ProductModel product) => Ok();

    [HttpGet]
    [AllowAnonymous]
    public IHttpActionResult GetPublicProducts() => Ok();
}
```

With this structure, access can be customized to match real-world authorization requirements.


```



# 6. Token System

## 6.3. Token Creation

### 6.3.1. Creating AccessToken

```csharp
var userForLogin = new UserForLoginDto
{
    Email = "kullanici@ornek.com",
    Password = "Sifre123!"
};

var accessToken = _authService.Login(userForLogin);

string token = accessToken.Token;
DateTime expiration = accessToken.Expiration;
string refreshToken = accessToken.RefreshToken;
```

### 6.3.2. Adding Custom Claims

```csharp
var customClaims = new Dictionary<string, string>
{
    { "organization", "Company A" },
    { "department", "Finance" },
    { "location", "Istanbul" }
};

var accessToken = _authService.CreateAccessToken(user, customClaims);
```

---

## 6.4. Client Side Token Storage and Utilization

### 6.4.1. Token Storage (JavaScript)

```javascript
function handleLoginSuccess(response) {
    localStorage.setItem('accessToken', response.token);
    localStorage.setItem('refreshToken', response.refreshToken);
    localStorage.setItem('tokenExpiration', response.expiration);
}
```

### 6.4.2. Token Usage in API Requests

```javascript
function callSecureApi(url, method, data) {
    const token = localStorage.getItem('accessToken');
    return fetch(url, {
        method: method,
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(data)
    });
}
```

---

## 6.5. Refresh Token

```csharp
string refreshToken = "previously_received_refresh_token";

try {
    var newAccessToken = _authService.RefreshToken(refreshToken);
    string newToken = newAccessToken.Token;
    DateTime newExpiration = newAccessToken.Expiration;
    string newRefreshToken = newAccessToken.RefreshToken;
} catch (Exception ex) {
    // Redirect to login
}
```

### 6.5.1. Client Side Token Refresh

```javascript
async function refreshAccessToken() {
    const refreshToken = localStorage.getItem('refreshToken');
    try {
        const response = await fetch('/api/auth/refreshtoken', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ refreshToken })
        });

        if (response.ok) {
            const data = await response.json();
            localStorage.setItem('accessToken', data.token);
            localStorage.setItem('refreshToken', data.refreshToken);
            localStorage.setItem('tokenExpiration', data.expiration);
            return true;
        } else {
            logout();
            return false;
        }
    } catch (error) {
        console.error('Token refresh error:', error);
        logout();
        return false;
    }
}
```

---

## 6.6. Token Verification

```csharp
string token = "incoming_jwt_token";
bool isValid = _authService.ValidateToken(token);
```

---

## 6.7. Token Revocation

```csharp
string refreshToken = "cancel_to_refresh_token";
bool revoked = _authService.RevokeToken(refreshToken);
```

---

## 6.8. JWT Configuration in .NET Framework

### 6.8.1. Required NuGet Packages

```powershell
Install-Package Microsoft.Owin.Security.Jwt
Install-Package Microsoft.Owin.Security.OAuth
Install-Package System.IdentityModel.Tokens.Jwt -Version 5.3.0
```

### 6.8.2. OWIN Startup Configuration

```csharp
[assembly: OwinStartup(typeof(YourNamespace.Startup))]

public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        var authOptions = new OrhAuthOptions
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["AuthDbConnection"].ConnectionString,
            CreateDatabaseIfNotExists = true,
            TokenSecurityKey = ConfigurationManager.AppSettings["Jwt:SecurityKey"],
            TokenIssuer = ConfigurationManager.AppSettings["Jwt:Issuer"],
            TokenAudience = ConfigurationManager.AppSettings["Jwt:Audience"],
            TokenExpirationMinutes = int.Parse(ConfigurationManager.AppSettings["Jwt:AccessTokenExpiration"])
        };

        var authService = OrhAuthExtensions.ConfigureOrhAuth(authOptions);

        app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
        {
            AuthenticationMode = AuthenticationMode.Active,
            AllowedAudiences = new[] { authOptions.TokenAudience },
            IssuerSecurityKeyProviders = new[] {
                new SymmetricKeyIssuerSecurityKeyProvider(
                    authOptions.TokenIssuer,
                    SecurityKeyHelper.GetSymmetricSecurityKey(authOptions.TokenSecurityKey))
            }
        });
    }
}
```

### 6.8.3. Web.config Configuration

```xml
<configuration>
  <appSettings>
    <add key="Jwt:SecurityKey" value="write_here_a_strong_security_key_yazin_en_az_32_characters_must_be" />
    <add key="Jwt:Issuer" value="https://api.sirketim.com" />
    <add key="Jwt:Audience" value="https://www.sirketim.com" />
    <add key="Jwt:AccessTokenExpiration" value="30" />
  </appSettings>
</configuration>
```

---

## 6.9. Customizing Token Settings

```csharp
var authOptions = new OrhAuthOptions
{
    ConnectionString = "your_connection_string",
    TokenSecurityKey = "your_security_key_min_32_chars",
    TokenIssuer = "your_api_domain",
    TokenAudience = "your_client_domain",
    TokenExpirationMinutes = 30,
    RefreshTokenTTLDays = 7
};
```

---

## 6.10. Example Use Cases

### 6.10.1. Using JWT with API Controller

```csharp
[Authorize]
public class SecureApiController : ApiController
{
    private readonly IAuthService _authService;

    public SecureApiController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IHttpActionResult GetSecureData()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Unauthorized();

        int userId = int.Parse(userIdClaim.Value);
        var user = _authService.GetUserById(userId);

        return Ok(new {
            Message = $"Hello {user.FirstName} {user.LastName}",
            Data = "Your secure data is here"
        });
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IHttpActionResult GetAdminData()
    {
        return Ok(new {
            Message = "Welcome to Admin Panel",
            Data = "Confidential administrator information"
        });
    }
}
```

### 6.10.2. Using JWT with MVC Controller

```csharp
[Authorize]
public class SecureController : Controller
{
    private readonly IAuthService _authService;

    public SecureController(IAuthService authService)
    {
        _authService = authService;
    }

    public ActionResult Dashboard()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return new HttpUnauthorizedResult();

        int userId = int.Parse(userIdClaim.Value);
        var user = _authService.GetUserById(userId);

        ViewBag.UserName = $"{user.FirstName} {user.LastName}";
        return View();
    }

    [Authorize(Roles = "Admin")]
    public ActionResult AdminPanel()
    {
        return View();
    }
}
```

> OrhAuth's JWT system provides scalable and secure authentication support for your .NET applications.


```



# 7. Data Access Layer

## 7.1. Overview

OrhAuth uses **Entity Framework 6.x Code-First** to define and manage the database schema. Key components:

- Entity Classes (POCOs)
- DbContext for database operations
- Repository Pattern abstraction
- Fluent API configurations

---

## 7.2. Code-First Approach

Advantages:

- Schema is defined in code
- Development-first approach
- Rapid development without DB admin required

### 7.2.1. Entity Classes

```csharp
public abstract class EntityBase
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

public class User : EntityBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
}
```

Other entities include: `OperationClaim`, `UserOperationClaim`, `RefreshToken`.

---

### 7.2.2. DbContext Class

```csharp
public class AuthDbContext : DbContext
{
    public AuthDbContext(string connectionString) : base(connectionString)
    {
        Configuration.LazyLoadingEnabled = true;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Configurations.Add(new UserConfiguration());
        modelBuilder.Configurations.Add(new OperationClaimConfiguration());
        modelBuilder.Configurations.Add(new UserOperationClaimConfiguration());
        modelBuilder.Configurations.Add(new RefreshTokenConfiguration());
    }
}
```

---

## 7.3. Repository Pattern

### 7.3.1. Interface

```csharp
public interface IEntityRepository<T> where T : class, new()
{
    T Get(Expression<Func<T, bool>> filter);
    IList<T> GetList(Expression<Func<T, bool>> filter = null);
    T Add(T entity);
    T Update(T entity);
    void Delete(T entity);
    int Count(Expression<Func<T, bool>> filter = null);
}
```

### 7.3.2. EF Implementation

```csharp
public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity> where TEntity : class, new()
{
    private readonly string _connectionString;

    public EfEntityRepositoryBase(string connectionString)
    {
        _connectionString = connectionString;
    }

    public TEntity Add(TEntity entity)
    {
        using (var context = new AuthDbContext(_connectionString))
        {
            var entry = context.Entry(entity);
            entry.State = EntityState.Added;

            if (entity is EntityBase baseEntity)
                baseEntity.CreatedDate = DateTime.Now;

            context.SaveChanges();
            return entity;
        }
    }

    // Other methods (Get, Update, Delete) are implemented similarly
}
```

---

## 7.4. Fluent API Configurations

### UserConfiguration

```csharp
public class UserConfiguration : EntityTypeConfiguration<User>
{
    public UserConfiguration()
    {
        HasKey(u => u.Id);
        ToTable("Users");

        Property(u => u.Email).IsRequired().HasMaxLength(100).IsUnicode(true);
        Property(u => u.FirstName).IsRequired().HasMaxLength(50);
        Property(u => u.LastName).IsRequired().HasMaxLength(50);

        HasIndex(u => u.Email).IsUnique().HasName("IX_User_Email");
    }
}
```

---

## 7.5. Extended User Model Support

OrhAuth dynamically handles additional user fields through reflection and schema caching during initialization.

---

## 7.6. Database Operations

### CRUD Example

```csharp
public User Register(UserForRegisterDto dto)
{
    if (UserExists(dto.Email))
        throw new Exception("Email already registered");

    HashingHelper.CreatePasswordHash(dto.Password, out var hash, out var salt);

    var user = new User
    {
        Email = dto.Email,
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        PasswordHash = hash,
        PasswordSalt = salt,
        IsActive = true
    };

    _userRepository.Add(user);

    var role = _operationClaimRepository.Get(r => r.Name == "User");
    _userOperationClaimRepository.Add(new UserOperationClaim
    {
        UserId = user.Id,
        OperationClaimId = role.Id
    });

    return user;
}
```

### Filtering & Querying

```csharp
var activeUsers = _userRepository.GetList(u => u.IsActive);
```

### Paging

```csharp
public List<User> GetUsers(int page, int size)
{
    using var context = new AuthDbContext(_connectionString);
    return context.Users.OrderBy(u => u.Id).Skip((page - 1) * size).Take(size).ToList();
}
```

---

## 7.7. Initialization

On first run, OrhAuth can:

- Create database
- Add base roles (`Admin`, `User`)
- Add default admin if enabled

---

## 7.8. .NET Framework Considerations

### Connection String

Define in `Web.config` or `App.config`:

```xml
<connectionStrings>
  <add name="AuthDbConnection" 
       connectionString="Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=MyAuthDb;Integrated Security=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### Using TransactionScope

```csharp
using (var scope = new TransactionScope())
{
    var user = _userRepository.Add(newUser);
    _userOperationClaimRepository.Add(new UserOperationClaim { UserId = user.Id, OperationClaimId = roleId });
    scope.Complete();
}
```

---

## 7.9. Example Use Cases

### Extended User Registration

```csharp
public dynamic RegisterExtendedUser(UserForRegisterDto baseUser, object extendedProps)
{
    var user = Register(baseUser);
    using var context = new AuthDbContext(_connectionString);

    var extUser = context.Users.Find(user.Id);
    foreach (var prop in extendedProps.GetType().GetProperties())
    {
        var target = SchemaMetadataCache.ExtendedUserType.GetProperty(prop.Name);
        target?.SetValue(extUser, prop.GetValue(extendedProps));
    }

    context.SaveChanges();
    return GetUserDynamicById(user.Id);
}
```

---

OrhAuth provides a clean and maintainable data access infrastructure based on EF and repository pattern — ideal for scalable authentication systems.


---


# 8. API References

This section provides reference documentation for the main APIs in the **OrhAuth** library. These APIs enable integration of authentication, authorization, token, and user management in .NET Framework projects.

---

## 8.1. AuthManager API

Implements the `IAuthService` interface for all authentication and authorization operations.

### Constructor

```csharp
public AuthManager(
    IEntityRepository<User> userRepository,
    IEntityRepository<OperationClaim> operationClaimRepository,
    IEntityRepository<UserOperationClaim> userOperationClaimRepository,
    IEntityRepository<RefreshToken> refreshTokenRepository,
    ITokenHelper tokenHelper,
    string connectionString)
```

- `userRepository`: User entity repository
- `operationClaimRepository`: Role/authorization repository
- `userOperationClaimRepository`: User-role relationship repository
- `refreshTokenRepository`: Refresh token repository
- `tokenHelper`: JWT token manager
- `connectionString`: Connection string to database

---

### Register

```csharp
public User Register(UserForRegisterDto userForRegisterDto)
```

Registers a new user and assigns the default "User" role.

#### Example

```csharp
var userDto = new UserForRegisterDto
{
    Email = "user@example.com",
    Password = "SecurePass123!",
    FirstName = "John",
    LastName = "Doe"
};

var user = _authService.Register(userDto);
```

---

### RegisterWithToken

```csharp
public AccessToken RegisterWithToken(UserForRegisterDto userForRegisterDto)
```

Registers the user and returns a JWT access token.

#### Example

```csharp
var userDto = new UserForRegisterDto
{
    Email = "user@example.com",
    Password = "SecurePass123!",
    FirstName = "John",
    LastName = "Doe"
};

var token = _authService.RegisterWithToken(userDto);
```

---

### Login

```csharp
public AccessToken Login(UserForLoginDto userForLoginDto)
```

Verifies credentials and returns an access token.

#### Example

```csharp
var loginDto = new UserForLoginDto
{
    Email = "user@example.com",
    Password = "SecurePass123!"
};

var token = _authService.Login(loginDto);
```

---

### CreateAccessToken

```csharp
public AccessToken CreateAccessToken(User user)
```

Generates a new JWT for the specified user.

#### Example

```csharp
var user = _authService.GetUserByEmail("user@example.com");
var token = _authService.CreateAccessToken(user);
```

---

### RefreshToken

```csharp
public AccessToken RefreshToken(string refreshToken, Dictionary<string, string> customClaims = null)
```

Renews an access token using a valid refresh token.

#### Example (Basic)

```csharp
var newToken = _authService.RefreshToken("existing-refresh-token");
```

#### Example (With custom claims)

```csharp
var customClaims = new Dictionary<string, string>
{
    { "department", "HR" },
    { "location", "Istanbul" }
};

var newToken = _authService.RefreshToken("existing-refresh-token", customClaims);
```

---

### RevokeToken

```csharp
public bool RevokeToken(string refreshToken)
```

Invalidates a refresh token.

#### Example

```csharp
bool revoked = _authService.RevokeToken("refresh-token-to-revoke");
```

---

(Diğer fonksiyonlar örnekleriyle birlikte devam edecek...)



---

## 8.2. TokenHelper API

Manages creation and validation of JWT access tokens and refresh tokens.

### Constructor

```csharp
public JwtHelper()
public JwtHelper(string securityKey, string issuer, string audience, int expirationMinutes)
```

---

### CreateToken

```csharp
public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
```

#### Example

```csharp
var user = _authService.GetUserById(123);
var claims = _authService.GetClaims(user);
var token = _tokenHelper.CreateToken(user, claims);
```

---

### CreateToken with Custom Claims

```csharp
public AccessToken CreateToken(User user, List<OperationClaim> operationClaims, Dictionary<string, string> additionalClaims)
```

#### Example

```csharp
var user = _authService.GetUserById(123);
var claims = _authService.GetClaims(user);
var extras = new Dictionary<string, string>
{
    { "department", "Marketing" },
    { "office", "HQ" }
};
var token = _tokenHelper.CreateToken(user, claims, extras);
```

---

### CreateRefreshToken

```csharp
public string CreateRefreshToken()
```

#### Example

```csharp
string refreshToken = _tokenHelper.CreateRefreshToken();
```

---

### ValidateToken

```csharp
public bool ValidateToken(string token)
```

#### Example

```csharp
string token = Request.Headers["Authorization"]?.Replace("Bearer ", "");
bool isValid = _tokenHelper.ValidateToken(token);
```

---

## 8.3. Repository API

Generic repository interface for data access.

### IEntityRepository<T>

```csharp
public interface IEntityRepository<T> where T : class, new()
```

- `Get(...)`
- `GetList(...)`
- `Add(...)`
- `Update(...)`
- `Delete(...)`
- `Count(...)`

#### Example

```csharp
var user = _userRepository.Get(u => u.Email == "user@example.com");
var activeUsers = _userRepository.GetList(u => u.IsActive);
int userCount = _userRepository.Count();
```

---

### EfEntityRepositoryBase<T>

Entity Framework implementation of the repository interface.

```csharp
public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
```

#### Example

```csharp
var userRepo = new EfEntityRepositoryBase<User>(connectionString);
var roleRepo = new EfEntityRepositoryBase<OperationClaim>(connectionString);
```

---

## 8.4. SchemaMetadataCache API

Supports extended user models by reflecting additional fields.

---

### RegisterExtendedType

```csharp
SchemaMetadataCache.RegisterExtendedType(typeof(ExtendedUser));
```

### GetExtendedProperties

```csharp
var props = SchemaMetadataCache.GetExtendedProperties();
foreach (var prop in props)
{
    Console.WriteLine($"{prop.Name}: {prop.PropertyType.Name}");
}
```

### IsExtendedProperty

```csharp
bool exists = SchemaMetadataCache.IsExtendedProperty("Department");
```

### GetExtendUserAttributeForProperty

```csharp
var attr = SchemaMetadataCache.GetExtendUserAttributeForProperty("Department");
```

### ExtendedUserType

```csharp
var type = SchemaMetadataCache.ExtendedUserType;
```

---

### Use Case Example

```csharp
public class ExtendedUser : User
{
    [ExtendUser(PropertyName = "UserDepartment", IsRequired = true)]
    public string Department { get; set; }

    [ExtendUser]
    public DateTime? HireDate { get; set; }

    [AddToClaim]
    public string EmployeeId { get; set; }
}

SchemaMetadataCache.RegisterExtendedType(typeof(ExtendedUser));

var userDto = new UserForRegisterDto { ... };
var extendedProps = new { Department = "IT", HireDate = DateTime.Now, EmployeeId = "EMP123" };

dynamic user = _authService.RegisterExtendedUser(userDto, extendedProps);
```

---

These reference APIs form the backbone of authentication, authorization, and user management logic within OrhAuth-powered .NET applications.


---


  
