# Backend Code Standards — MusicXD (.NET 8 / C#)

## Naming Conventions

| Element | Convention | Example |
|---------|-----------|---------|
| Namespaces | PascalCase, hierarchical | `MusicXD.Application.Features.Auth` |
| Classes | PascalCase | `AlbumReviewService`, `CreateReviewCommand` |
| Interfaces | `I` prefix + PascalCase | `IAlbumReviewRepository`, `IJwtTokenService` |
| Methods | PascalCase | `GetAlbumByIdAsync()`, `CreateReviewAsync()` |
| Async methods | Always suffix `Async` | `GetByIdAsync()`, `SaveChangesAsync()` |
| Properties | PascalCase | `UserId`, `CreatedAt`, `ReviewText` |
| Local variables | camelCase | `albumId`, `isAuthenticated`, `reviewCount` |
| Parameters | camelCase | `userId`, `command`, `cancellationToken` |
| Constants | PascalCase | `MaxReviewLength`, `DefaultPageSize` |
| Private fields | `_camelCase` | `_dbContext`, `_logger`, `_spotifyService` |
| Enums | PascalCase (type and values) | `ActivityType.AlbumReviewed` |

---

## Project Structure (Clean Architecture)

```
MusicXD.Domain/
  Entities/          ← Pure domain entities, no EF or framework dependencies
  Exceptions/        ← Domain-specific exceptions (future)

MusicXD.Application/
  Features/          ← CQRS commands and queries, one folder per feature
  DTOs/              ← Data transfer objects (response shapes)
  Interfaces/        ← Contracts implemented by Infrastructure
  ServiceCollectionExtensions.cs

MusicXD.Infrastructure/
  Persistence/
    ApplicationDbContext.cs
    Configurations/  ← IEntityTypeConfiguration<T> for each entity
    Migrations/      ← EF Core migrations (auto-generated)
  Services/          ← JwtTokenService, SpotifyService
  Caching/           ← RedisCacheService
  Jobs/              ← Background services (SpotifySyncJob)
  DependencyInjection.cs

MusicXD.API/
  Controllers/       ← Thin controllers, delegate to MediatR
  Middleware/        ← ExceptionHandlingMiddleware
  Program.cs         ← DI composition root
```

---

## CQRS Pattern (MediatR)

Every use case is a Command (write) or Query (read). Each gets its own folder:

```
Features/AlbumReviews/
  Commands/
    CreateAlbumReview/
      CreateAlbumReviewCommand.cs        ← IRequest<AlbumReviewDto>
      CreateAlbumReviewCommandHandler.cs ← IRequestHandler<...>
      CreateAlbumReviewCommandValidator.cs ← AbstractValidator<...>
  Queries/
    GetAlbumReviews/
      GetAlbumReviewsQuery.cs            ← IRequest<List<AlbumReviewDto>>
      GetAlbumReviewsQueryHandler.cs     ← IRequestHandler<...>
```

### Command example

```csharp
// Command (record preferred for immutability)
public record CreateAlbumReviewCommand(
    Guid UserId,
    Guid AlbumId,
    int Rating,
    string ReviewText) : IRequest<AlbumReviewDto>;

// Handler
public class CreateAlbumReviewCommandHandler
    : IRequestHandler<CreateAlbumReviewCommand, AlbumReviewDto>
{
    private readonly IApplicationDbContext _context;

    public CreateAlbumReviewCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<AlbumReviewDto> Handle(
        CreateAlbumReviewCommand command,
        CancellationToken cancellationToken)
    {
        // business logic here
    }
}

// Validator (FluentValidation)
public class CreateAlbumReviewCommandValidator
    : AbstractValidator<CreateAlbumReviewCommand>
{
    public CreateAlbumReviewCommandValidator()
    {
        RuleFor(x => x.Rating).InclusiveBetween(1, 5);
        RuleFor(x => x.ReviewText).MaximumLength(2000);
    }
}
```

---

## Controllers

Controllers must be thin — they only:
1. Receive HTTP request and extract parameters
2. Send command/query via MediatR
3. Return the result

No business logic in controllers.

```csharp
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AlbumReviewsController : ControllerBase
{
    private readonly ISender _sender;

    public AlbumReviewsController(ISender sender) => _sender = sender;

    [HttpPost]
    public async Task<ActionResult<AlbumReviewDto>> Create(
        [FromBody] CreateAlbumReviewCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetByAlbum), new { albumId = result.AlbumId }, result);
    }
}
```

---

## Entity Framework Core

- Entity configurations go in separate `IEntityTypeConfiguration<T>` classes in `Persistence/Configurations/`
- Never call `.Result` or `.Wait()` — always `await`
- Always pass `CancellationToken` through the call chain
- Use `AsNoTracking()` for read-only queries

```csharp
// Configuration class (not in DbContext)
public class AlbumReviewConfiguration : IEntityTypeConfiguration<AlbumReview>
{
    public void Configure(EntityTypeBuilder<AlbumReview> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Rating).IsRequired();
        builder.Property(r => r.ReviewText).HasMaxLength(2000);
        builder.HasOne(r => r.User).WithMany().HasForeignKey(r => r.UserId);
        builder.HasOne(r => r.Album).WithMany().HasForeignKey(r => r.AlbumId);
    }
}
```

---

## Error Handling

All unhandled exceptions are caught by `ExceptionHandlingMiddleware` in `MusicXD.API/Middleware/`. Do not wrap every handler in try/catch — let the middleware handle it.

For domain validation errors, throw specific exceptions (future: `DomainException`, `NotFoundException`).

---

## Async Rules

```csharp
// ✅ Correct
var user = await _context.Users.FindAsync(userId, cancellationToken);

// ❌ Never do this
var user = _context.Users.FindAsync(userId).Result;
var user = _context.Users.FindAsync(userId).GetAwaiter().GetResult();
```

---

## Dependency Injection

Register services in layer-specific extension methods, not in `Program.cs`:

```csharp
// MusicXD.Infrastructure/DependencyInjection.cs
public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration)
{
    services.AddDbContext<ApplicationDbContext>(...);
    services.AddScoped<IJwtTokenService, JwtTokenService>();
    return services;
}

// Program.cs — clean composition root
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
```

---

## Testing

- Framework: **xUnit**
- Mocking: **Moq**
- Pattern: **AAA (Arrange / Act / Assert)**
- Unit tests: test handlers in isolation with mocked interfaces
- Location: `MusicXD.Tests/` project (to be created — Issue #12, #13)

```csharp
[Fact]
public async Task Handle_ValidCommand_ReturnsReviewDto()
{
    // Arrange
    var mockContext = new Mock<IApplicationDbContext>();
    var handler = new CreateAlbumReviewCommandHandler(mockContext.Object);
    var command = new CreateAlbumReviewCommand(
        UserId: Guid.NewGuid(),
        AlbumId: Guid.NewGuid(),
        Rating: 5,
        ReviewText: "Masterpiece");

    // Act
    var result = await handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(5, result.Rating);
}
```

---

## Code Checklist

Before committing:
- [ ] All new methods are `async` and return `Task<T>` or `Task`
- [ ] `CancellationToken` passed through all async calls
- [ ] No `.Result` or `.Wait()`
- [ ] New entities have `IEntityTypeConfiguration<T>` class
- [ ] EF Core migration created if schema changed (`dotnet ef migrations add <Name>`)
- [ ] No business logic in controllers
- [ ] `dotnet build` passes without warnings
