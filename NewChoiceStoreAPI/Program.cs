using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NewChoiceStoreAPI;
using NewChoiceStoreAPI.Data;
using NewChoiceStoreAPI.Interfaces;
using NewChoiceStoreAPI.Models;
using NewChoiceStoreAPI.Services;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

//to make api available for being called 

var policyName = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName, builder =>
    {
        builder.AllowAnyOrigin().WithMethods("GET", "POST", "PUT","DELETE").AllowAnyHeader();
    });
});

builder.Services.AddDbContext<DatabaseContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("connection")));

//builder.Services.AddScoped<AddProd>(); //only needed to load products

builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<ISignupService,SignupService>();
builder.Services.AddScoped<ILoginService,LoginService>();
builder.Services.AddScoped<ICartsService,CartsService>();
builder.Services.AddScoped<IOrderService,OrderService>();
builder.Services.AddScoped<IWishListService,WishListService>();
builder.Services.AddCors();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options=>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration.GetValue<string>("Jwt:ValidIssuer"),
            ValidAudience= builder.Configuration.GetValue<string>("Jwt:ValidAudience"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:Key")))
           
        };
    }
   );

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Executed only once to load products into database;
/*var scope = app.Services.CreateScope();
scope.ServiceProvider.GetService<AddProd>().AddProducts();*/

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors(policyName);

app.Run();

