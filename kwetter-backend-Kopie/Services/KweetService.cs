using System;
using System.Threading.Tasks;
using kwetter_backend.Data;
using kwetter_backend.Models;

namespace kwetter_backend.Services
{
    public class KweetService
    {
        private KweetContext kweetContext;

        public KweetService(KweetContext context)
        {
            kweetContext = context;
        }

        public async Task<Response<Kweet>>CreateKweet(Guid profileId, string message)
        {
            var response = new Response<Kweet>();

            profileId = new Guid(); //TODO: retrieve profile Id from profile service;

            Kweet kweet = new Kweet
            {
                Id = Guid.NewGuid(),
                ProfileId = profileId,
                Message = message,
                DateOfCreation = DateTime.Now
            };

            await kweetContext.AddAsync(kweet);
            var succes = await kweetContext.SaveChangesAsync() > 0;
            if (succes)
            {
                response.Success = true;
                response.Data = kweet;
            }

            return response;
        }
    }
}
