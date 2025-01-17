using api.Dtos.User;
using api.Models;

namespace api.Mappers
{
    public static class UserMappers
    {
        public static UserDto ToUserDto(this User userModel)
        {
            return new UserDto
            {
                Id = userModel.Id,
                Login = userModel.Login,
            };
        }

        public static User ToUserFromCreateDTO(this RegisterUserDto userDto)
        {
            return new User
            {
                Login = userDto.Login,
            };
        }
    }
}
