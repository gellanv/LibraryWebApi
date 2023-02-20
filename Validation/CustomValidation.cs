using Library.Exceptions;

namespace LibraryWebApi.Validation
{
    public class CustomValidation
    {
        public void CheckId(int id, string message)
        {
            if (id <= 0)
            {
                throw new BadRequestCustomException($"{message}{id} isn't valid");
            }
        }

        public void CheckScore(int score)
        {
            if (score < 1 || score > 5)
            {
                throw new ValidationCustomException($"Score {score} isn't valid");
            }
        }

        public void CheckObjectForNull(object instance, string message)
        {
            if (instance == null)
            {
                throw new NotFoundCustomException($"The {message} wasn't found");
            }
        }
    }
}
