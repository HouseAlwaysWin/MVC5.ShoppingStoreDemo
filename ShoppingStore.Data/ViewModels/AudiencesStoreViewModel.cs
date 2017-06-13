using Microsoft.Owin.Security.DataHandler.Encoder;
using ShoppingStore.Domain.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Data.ViewModels
{
    public class AudiencesStoreViewModel
    {
        public static ConcurrentDictionary<string, Audience> AudiencesList =
            new ConcurrentDictionary<string, Audience>();

        public AudiencesStoreViewModel()
        {
            AudiencesList.TryAdd("e672f43a307d48978f5d60c51fc2944c", new Audience
            {
                ClientId = "e672f43a307d48978f5d60c51fc2944c",
                Base64Secret = "wzLHzvMqSK9t7R1Ctcf1Y54IpJhOOW3kKMYAtOHnW7o=",
                Name = "Audience"
            });
        }



        public static Audience AddAudience(string name)
        {
            var clientId = Guid.NewGuid().ToString("N");

            var key = new byte[32];
            RNGCryptoServiceProvider.Create().GetBytes(key);
            var base64Secret = TextEncodings.Base64.Encode(key);

            Audience newAudience =
                new Audience
                {
                    ClientId = clientId,
                    Base64Secret = base64Secret,
                    Name = name
                };

            AudiencesList.TryAdd(clientId, newAudience);
            return newAudience;
        }

        public static Audience FindAudience(string clientId)
        {
            Audience audience = null;
            if (AudiencesList.TryGetValue(clientId, out audience))
            {
                return audience;
            }
            return null;
        }
    }
}
