namespace Entities.Concrete.DTOs.MembershipDTOs
{
    public record class RegisterDTO(
        string Email,
        string UserName,
        string FirstName,
        string LastName,
        string Password
        );
   
}
