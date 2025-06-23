using Microsoft.VisualBasic;

namespace DevLife.APIproj.DTO
{
    public record RegisterDto(
        string Username,
        string Firstname,
        string Lastname,
        DateTime BirthDate,
        string Level,
        string Stack);

}
