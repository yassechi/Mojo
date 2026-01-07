using infrastructure_mojo.repositories;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using api_mojo.Extensions;
using core_mojo.models;
using core_mojo;

var builder = WebApplication.CreateBuilder(args);

// Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// Jwt
builder.Services.AddCustomJwtAut(builder.Configuration);

// controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// database
builder.Services.AddDbContext<AppDbContext>(op =>
    op.UseSqlServer(builder.Configuration.GetConnectionString("connect")));

// Enregistrement du repository
builder.Services.AddScoped<AmortissementRepository>();
builder.Services.AddScoped<ContratRepository>();
builder.Services.AddScoped<DiscussionRepository>();
builder.Services.AddScoped<InterventionRepository>();
builder.Services.AddScoped<MessageRepository>();
builder.Services.AddScoped<OrganisationRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<VeloRepository>();


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();  

app.MapControllers();

app.Run();