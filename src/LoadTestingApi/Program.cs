using System.Reflection;
using AutoBogus;
using Bogus;
using LazyCache;
using LoadTestingApi;
using LoadTestingApi.DependencyInjection;
using LoadTestingApi.Entities;
using LoadTestingApi.MappingAbstractions;
using Mapster;
using MapsterMapper;
using ProtoBuf.Meta;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// 1. Configure Logging
// ===========================
builder.Host.UseSerilog((ctx, lc) =>
{
    lc.WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning);
});


// 2. Add services to the container.
// ===========================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLazyCache();

builder.Services.AddSingleton(new TypeAdapterConfig());
builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services.Scan(scan =>
{
    scan.FromAssemblyOf<ISingletonService>().AddClasses(classes => classes.AssignableTo<ITransientService>()).AsImplementedInterfaces().WithTransientLifetime();
    scan.FromAssemblyOf<ISingletonService>().AddClasses(classes => classes.AssignableTo<IScopedService>()).AsImplementedInterfaces().WithScopedLifetime();
    scan.FromAssemblyOf<ISingletonService>().AddClasses(classes => classes.AssignableTo<ISingletonService>()).AsImplementedInterfaces().WithSingletonLifetime();
});

//builder.Services.AddSingleton<IAddressBookMapper, AddressBookMapper>();

// 3. Build app
// ===========================
var app = builder.Build();


// 4. Populate cache
// ===========================

var size = 1000;
var cache = app.Services.GetService<IAppCache>();

//   Populate strings
Randomizer.Seed = new Random(123);
cache.Add(CacheKeys.CachedStrings, AutoFaker.Generate<string>(size));

//   Populate Address book Entity
Randomizer.Seed = new Random(123);

var phoneFaker = new AutoFaker<PhoneEntity>()
    .RuleFor(x => x.Number, f => f.Person.Phone);

var personFaker = new AutoFaker<PersonEntity>()
    .RuleFor(x => x.ID, f => f.Random.Int(1, 9999999))
    .RuleFor(x => x.Name, f => f.Person.FullName)
    .RuleFor(x => x.Email, f => f.Person.Email)
    .RuleFor(x => x.Age, f => f.Random.Int(10, 90))
    .RuleFor(x => x.Phones, f => phoneFaker.Generate(3));

var addressBookEntity = new AddressBookEntity
{
    Persons = personFaker.Generate(size)
};

cache.Add(CacheKeys.CachedAddressBookEntity, addressBookEntity);

//    Populate Address book DTO
var mapper = new AddressBookMapper();
var addressBookDto = mapper.Map(addressBookEntity);
cache.Add(CacheKeys.CachedAddressBookDto, addressBookDto);


// =========================== End Populate Cache




// 5. Configure the HTTP request pipeline.
// ===========================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
