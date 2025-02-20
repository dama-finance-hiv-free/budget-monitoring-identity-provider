using System;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityProvider
{
    internal class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "mvc.client",
                    ClientSecrets = { new Secret("B8B53AF6-AA47-4F12-9EF1-C6416BD67202".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    
                    // where to redirect to after login
                    RedirectUris = { "http://localhost:3000/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "damaapi",
                    }
                },
                new Client
                {
                    ClientId = "emr.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("1E70CEF3-C0AE-4F7B-BE4E-9F0613E58F25".Sha256())
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "damaapi",
                    },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AlwaysSendClientClaims = true,
                },
                // React Dev client
                new Client
                {
                    ClientId = "emr.js.client",
                    ClientName = "EMR React Client",
                    ClientUri = "http://localhost:3000",

                    AllowedGrantTypes = GrantTypes.Implicit,

                    RequireClientSecret = false,

                    RedirectUris =
                    {
                        "http://localhost:3000/signin-oidc",
                    },

                    PostLogoutRedirectUris = { "http://localhost:3000/signout-oidc" },
                    AllowedCorsOrigins = { "http://localhost:3000" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "damaapi"
                    },

                    AllowAccessTokensViaBrowser = true
                },
                // React client
                new Client
                {
                    ClientId = "damafin.js.client",
                    ClientName = "EMR React Client",
                    ClientUri = "https://www.damafin.net",

                    AllowedGrantTypes = GrantTypes.Implicit,

                    RequireClientSecret = false,

                    RedirectUris =
                    {
                        "https://www.damafin.net/signin-oidc",
                    },

                    PostLogoutRedirectUris = { "https://www.damafin.net/signout-oidc" },
                    AllowedCorsOrigins = { "https://www.damafin.net", "https://damafin.net" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "damaapi"
                    },

                    AllowAccessTokensViaBrowser = true
                }
            };
        }
    }

    internal class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource
                {
                    Name = "damaapi",
                    DisplayName = "Pmel API",
                    Description = "Allow the application to access the Pmel API on your behalf",
                    Scopes = new List<string> { "damaapi"},
                    ApiSecrets = new List<Secret> {new Secret("E795CE6B-78EA-4555-B33E-D2FBA3FDD1E7".Sha256())},
                    UserClaims = new List<string> {"role"}
                },
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("damaapi", "Read Access to EMR API"),
            };
        }
    }

    internal class Users
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    Username = "admin@cbchs.cm",
                    Password = "Password123!",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "admin@cbchs.cm"),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                },
                new TestUser
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    Username = "doctor@cbchs.cm",
                    Password = "Password123!",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "doctor@cbchs.cm"),
                        new Claim(JwtClaimTypes.Role, "doctor")
                    }
                },
                new TestUser
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    Username = "screener@cbchs.cm",
                    Password = "Password123!",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "screener@cbchs.cm"),
                        new Claim(JwtClaimTypes.Role, "screener")
                    }
                },
                new TestUser
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    Username = "pharmacist@cbchs.cm",
                    Password = "Password123!",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "pharmacist@cbchs.cm"),
                        new Claim(JwtClaimTypes.Role, "pharmacist")
                    }
                },
                new TestUser
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    Username = "laboratory@cbchs.cm",
                    Password = "Password123!",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "laboratory@cbchs.cm"),
                        new Claim(JwtClaimTypes.Role, "laboratory")
                    }
                } ,              
                new TestUser
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    Username = "cashpayment@cbchs.cm",
                    Password = "Password123!",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "cashpayment@cbchs.cm"),
                        new Claim(JwtClaimTypes.Role, "cashpayment")
                    }
                },              
                new TestUser
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    Username = "procurement@cbchs.cm",
                    Password = "Password123!",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "procurement@cbchs.cm"),
                        new Claim(JwtClaimTypes.Role, "procurement")
                    }
                },            
                new TestUser
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    Username = "french@cbchs.cm",
                    Password = "Password123!",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "french@cbchs.cm"),
                        new Claim(JwtClaimTypes.Role, "nurse")
                    }
                },           
                new TestUser
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    Username = "nurse@cbchs.cm",
                    Password = "Password123!",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "nurse@cbchs.cm"),
                        new Claim(JwtClaimTypes.Role, "nurse")
                    }
                },
            };
        }
    }
}
