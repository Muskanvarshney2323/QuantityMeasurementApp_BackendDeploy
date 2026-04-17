using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;

namespace QuantityMeasurementAppBusinessLayer.Helpers
{
    public class GoogleTokenHelper
    {
        private readonly IConfiguration _configuration;

        public GoogleTokenHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<GoogleJsonWebSignature.Payload?> VerifyGoogleTokenAsync(string idToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string>
                {
                    _configuration["GoogleAuth:ClientId"]!
                }
            };

            return await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
        }
    }
}