using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using api_mojo.Extensions;
using core_mojo.models;      // Assure-toi que le 'M' est majuscule si c'est le cas dans ton dossier
using core_mojo.interfaces;        // Pour les DTOs // Pour IRepository
using core_mojo.Dtos;
using core_mojo;
using infrastructure_mojo.repositories;



var builder = WebApplication.CreateBuilder(args);

// --- IDENTITY ---
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// --- AUTHENTICATION (JWT) ---
builder.Services.AddCustomJwtAut(builder.Configuration);

// --- CONTROLLERS & JSON ---
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// --- DATABASE ---
builder.Services.AddDbContext<AppDbContext>(op =>
    op.UseSqlServer(builder.Configuration.GetConnectionString("connect")));

// --- REPOSITORIES INJECTION ---
builder.Services.AddScoped<IRepository<Amortissement, AmortissmentAddDto, AmortissmentUpdateDto>, AmortissementRepository>();
builder.Services.AddScoped<IRepository<Contrat, ContratAddDto, ContratUpdateDto>, ContratRepository>();
builder.Services.AddScoped<IRepository<Discussion, DiscussionAddDto, DiscussionUpdateDto>, DiscussionRepository>();
builder.Services.AddScoped<IRepository<Intervention, InterventionAddDto, InterventionUpdateDto>, InterventionRepository>();
builder.Services.AddScoped<IRepository<Message, MessageAddDto, MessageUpdateDto>, MessageRepository>();
builder.Services.AddScoped<IRepository<Organisation, OrganisationAddDto, OrganisationUpdateDto>, OrganisationRepository>();
builder.Services.AddScoped<IRepository<User, UserAddDto, UserUpdateDto>, UserRepository>();
builder.Services.AddScoped<IRepository<Velo, VeloAddDto, VeloUpdateDto>, VeloRepository>();

// --- SWAGGER ---
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