using data_mojo;
using data_mojo.models;
using data_mojo.repositories;
using Microsoft.EntityFrameworkCore;
using data_mojo.dtos;
using data_mojo.interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// DB
builder.Services.AddDbContext<AppDbContext>(op =>
op.UseSqlServer(builder.Configuration.GetConnectionString("connect")));

// Repositories
builder.Services.AddScoped<IRepository<Accessoire, AccessoireAddDto, AccessoireUpdateDto>, AccessoireRepository>();
builder.Services.AddScoped<IRepository<Amortissement, AmortissmentAddDto, AmortissmentUpdateDto>, AmortissementRepository>();
builder.Services.AddScoped<IRepository<Contrat, ContratAddDto, ContratUpdateDto>, ContratRepository>();
builder.Services.AddScoped<IRepository<Discussion, DiscussionAddDto, DiscussionUpdateDto>, DiscussionRepository>();
builder.Services.AddScoped<IRepository<Intervention, InterventionAddDto, InterventionUpdateDto>, InterventionRepository>();
builder.Services.AddScoped<IRepository<Message, MessageAddDto, MessageUpdateDto>, MessageRepository>();
builder.Services.AddScoped<IRepository<Organisation, OrganisationAddDto, OrganisationUpdateDto>, OrganisationRepository>();
builder.Services.AddScoped<IRepository<User, UserAddDto, UserUpdateDto>, UserRepository>();
builder.Services.AddScoped<IRepository<Velo, VeloAddDto, VeloUpdateDto>, VeloRepository>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// app.UseAuthorization();
app.MapControllers();

app.Run();

