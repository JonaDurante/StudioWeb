﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StudioData.Models.JWT;

namespace StudioWeb
{
    // Esta clase nos permite agregar los settings de mi jwt al programa
    public static class AddJwtTokenServicesExtensions
    {
        public static void AddJwtTokenServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Añadimos JWTsettings 
            var bindJwtSettings = new JwtSettings();
            // transpaso
            configuration.Bind("JsonWebTokenKeys", bindJwtSettings);
            // Añadimos Singleton a los JWTsettings 
            services.AddSingleton(bindJwtSettings);

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Autenticar usuarios 
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;    // Comprobar Usuarios
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        // Transpasamos el archivo de configuración.
                        ValidateIssuerSigningKey = bindJwtSettings.ValidateIsUserSigninKey,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IsUserSigninKey)),
                        ValidateIssuer = bindJwtSettings.ValidateIsUser,
                        ValidIssuer = bindJwtSettings.ValidIsUser,
                        ValidateAudience = bindJwtSettings.ValidateAudience,
                        ValidAudience = bindJwtSettings.ValidAudience,
                        RequireExpirationTime = bindJwtSettings.RequiredExpirationTime,
                        ValidateLifetime = bindJwtSettings.ValidtaeLifeTime,
                        ClockSkew = TimeSpan.FromDays(1)
                    };
                });
        }
    }
}
