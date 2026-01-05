using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Scoping.Application.DbOps;
using Services.Scoping.Application.DbOps.Engagement; // Add this using
using Services.Scoping.Domain.AggregateRoots.EngagementAggregate;
using Services.Scoping.Domain.AggregateRoots.FundAggregate;
using Services.Scoping.Infrastructure.Persistence;
using Services.Scoping.Infrastructure.Repositories;
using Services.Scoping.Infrastructure.Repositories.Engagement;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllers();

// Register repository dependencies here
builder.Services.AddScoped<IEngagementRepository, EngagementRepository>();
builder.Services.AddScoped<IEngagementCreationService, EngagementCreationService>();
builder.Services.AddScoped<IFundRepository, FundRepository>();

// Register MediatR dependencies
builder.Services.AddMediatR(cfg =>
{
    // Register handlers from both API and Application assemblies
    cfg.RegisterServicesFromAssemblyContaining<Program>();
    cfg.RegisterServicesFromAssemblyContaining<GetEngagementDetailsDBHandler>();
    cfg.RegisterServicesFromAssemblyContaining<CreateEngagementDBHandler>();
    cfg.RegisterServicesFromAssemblyContaining<UpdateEngagementDBhandler>();
    cfg.RegisterServicesFromAssemblyContaining<GetFundDbHandler>();
});
builder.Services.AddDbContext<ScopingDbContext>(options =>
    options.UseSqlServer("Data Source=NIT2LPT-24-439;Initial Catalog=Fund-Scoping;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();