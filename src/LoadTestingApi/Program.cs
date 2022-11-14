using AutoBogus;
using Bogus;
using LazyCache;
using LoadTestingApi;
using LoadTestingApi.Entities;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLazyCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Grab the cache and populate
// ===========================

var size = 1000;
var cache = app.Services.GetService<IAppCache>();

//   Populate strings
Randomizer.Seed = new Random(123);
cache.Add(CacheKeys.CachedStrings, AutoFaker.Generate<string>(size));

//   Populate Address book
Randomizer.Seed = new Random(123);

var phoneFaker = new AutoFaker<PhoneEntity>()
    .RuleFor(x => x.Number, f => f.Person.Phone);

var personFaker = new AutoFaker<PersonEntity>()
    .RuleFor(x => x.ID, f => f.Random.Int(1, 9999999))
    .RuleFor(x => x.Name, f => f.Person.FullName)
    .RuleFor(x => x.Email, f => f.Person.Email)
    .RuleFor(x => x.Age, f => f.Random.Int(10, 90))
    .RuleFor(x => x.Phones, f => phoneFaker.Generate(3));

cache.Add(CacheKeys.CachedAddressBook, new AddressBookEntity { Persons = personFaker.Generate(size) });

// =========================== End Populate Cache


app.UseAuthorization();
app.MapControllers();

app.Run();
