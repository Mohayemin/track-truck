using System.Web;
using Ssi.TrackTruck.Bussiness.Auth;

namespace Ssi.TrackTruck.Web.Utils
{
    public class SignedInUser : ISignedInUser
    {
        private readonly HttpContextBase _httpContext;

        public SignedInUser(HttpContextBase httpContext)
        {
            _httpContext = httpContext;
        }

        public string Id
        {
            get
            {
                return _httpContext.User.Identity.Name;
            }
        }

        public bool IsSignedIn {
            get
            {
                return _httpContext.User.Identity.IsAuthenticated;
            }
        }
    }
}