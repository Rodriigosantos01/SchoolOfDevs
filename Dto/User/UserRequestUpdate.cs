using SchollOfDevs.Enuns;

namespace SchollOfDevs.Dto.User
{
    public class UserRequestUpdate: UserRequest
    {
        public string CurrentPassword { get; set; }
    }
}
