// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.using Microsoft.AspNetCore.Authorization;

using System;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace B2CMyApp.Library.AzureAdB2C {

    /// <summary>
    /// 
    /// </summary>
    internal class AzureADB2COpenIdConnectOptionsConfiguration : IConfigureNamedOptions<OpenIdConnectOptions> {
        private readonly IOptions<AzureADB2CSchemeOptions> _schemeOptions;
        private readonly IOptionsMonitor<AzureADB2COptions> _azureADB2COptions;

        public AzureADB2COpenIdConnectOptionsConfiguration( IOptions<AzureADB2CSchemeOptions> schemeOptions, IOptionsMonitor<AzureADB2COptions> azureADB2COptions ) {
            _schemeOptions = schemeOptions;
            _azureADB2COptions = azureADB2COptions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options"></param>
        public void Configure( string name, OpenIdConnectOptions options ) {
            var azureADB2CScheme = GetAzureADB2CScheme(name);
            var azureADB2COptions = _azureADB2COptions.Get(azureADB2CScheme);
            if ( name != azureADB2COptions.OpenIdConnectSchemeName ) {
                return;
            }

            options.ClientId = azureADB2COptions.ClientId;
            options.ClientSecret = azureADB2COptions.ClientSecret;
            options.Authority = BuildAuthority(azureADB2COptions);
            options.CallbackPath = azureADB2COptions.CallbackPath ?? options.CallbackPath;
            options.SignedOutCallbackPath = azureADB2COptions.SignedOutCallbackPath ?? options.SignedOutCallbackPath;
            options.SignInScheme = azureADB2COptions.CookieSchemeName;
            options.UseTokenLifetime = true;
            options.TokenValidationParameters = new TokenValidationParameters { NameClaimType = "name" };

            var handlers = new AzureADB2COpenIDConnectEventHandlers(azureADB2CScheme, azureADB2COptions);
            options.Events = new OpenIdConnectEvents {
                OnRedirectToIdentityProvider = handlers.OnRedirectToIdentityProvider,
                OnRemoteFailure = handlers.OnRemoteFailure
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AzureADB2COptions"></param>
        /// <returns></returns>
        internal static string BuildAuthority( AzureADB2COptions AzureADB2COptions ) {
            var baseUri = new Uri(AzureADB2COptions.Instance);
            var pathBase = baseUri.PathAndQuery.TrimEnd('/');
            var domain = AzureADB2COptions.Domain;
            var policy = AzureADB2COptions.DefaultPolicy;

            // https://YagioB2CTenant.b2clogin.com/YagioB2CTenant.onmicrosoft.com/oauth2/v2.0/authorize?p=B2C_1_signinUp&client

            var url = new Uri(baseUri, new PathString($"{pathBase}/{domain}/{policy}/v2.0")).ToString();
            return url;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetAzureADB2CScheme( string name ) {
            foreach ( var mapping in _schemeOptions.Value.OpenIDMappings ) {
                if ( mapping.Value.OpenIdConnectScheme == name ) {
                    return mapping.Key;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public void Configure( OpenIdConnectOptions options ) {
        }
    }
}