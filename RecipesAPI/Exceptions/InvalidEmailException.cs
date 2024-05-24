using PoS.Core.Exceptions;
using System.Net;

namespace RecipesAPI.Exceptions
{
    public class InvalidEmailException : ApiException
    {
        public InvalidEmailException() : base("email is not valid", HttpStatusCode.BadRequest) { }
    }
}
