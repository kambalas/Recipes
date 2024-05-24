using PoS.Core.Exceptions;
using System.Net;

namespace RecipesAPI.Exceptions
{
    public class InvalidLoginCredentialsException : ApiException
    {
        public InvalidLoginCredentialsException(string message) : base(message, HttpStatusCode.BadRequest) { }
    }
}
