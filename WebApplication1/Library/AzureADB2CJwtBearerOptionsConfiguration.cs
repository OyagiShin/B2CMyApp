// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Authentication.JwtBearer;

using B2CMyApp.Library.AzureAdB2C;
using Microsoft.Extensions.Options;

namespace B2CMyApp.Library {
    internal class AzureADB2CJwtBearerOptionsConfiguration : IConfigureNamedOptions<JwtBearerOptions> {
        private readonly IOptions<AzureADB2CSchemeOptions> _schemeOptions;
        private readonly IOptionsMonitor<AzureADB2COptions> _azureADB2COptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schemeOptions"></param>
        /// <param name="azureADB2COptions"></param>
        public AzureADB2CJwtBearerOptionsConfiguration(
            IOptions<AzureADB2CSchemeOptions> schemeOptions,
            IOptionsMonitor<AzureADB2COptions> azureADB2COptions ) {
            _schemeOptions = schemeOptions;
            _azureADB2COptions = azureADB2COptions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options"></param>
        public void Configure( string name, JwtBearerOptions options ) {

            //
            var azureADB2CScheme = GetAzureADB2CScheme(name);
            var azureADB2COptions = _azureADB2COptions.Get(azureADB2CScheme);
            if ( name != azureADB2COptions.JwtBearerSchemeName ) {
                return;
            }

            options.Audience = azureADB2COptions.ClientId;
            options.Authority = AzureADB2COpenIdConnectOptionsConfiguration.BuildAuthority(azureADB2COptions);
        }

        public void Configure( JwtBearerOptions options ) {
        }

        private string GetAzureADB2CScheme( string name ) {
            foreach ( var mapping in _schemeOptions.Value.JwtBearerMappings ) {
                if ( mapping.Value.JwtBearerScheme == name ) {
                    return mapping.Key;
                }
            }

            return null;
        }
    }
}