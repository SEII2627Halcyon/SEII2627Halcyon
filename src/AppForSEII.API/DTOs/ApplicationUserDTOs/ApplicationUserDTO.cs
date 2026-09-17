using Microsoft.AspNetCore.Identity;

namespace AppForSEII.API.DTOs.ApplicationUserDTO;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUserDTO 
{
    public ApplicationUserDTO()
    {
    }
    public ApplicationUserDTO(string id, string? name, string? surname, string? userName, string? phoneNumber )
    {
        Id = id;
        Name = name;
        Surname = surname;
        UserName = userName;
        Email = userName;
        PhoneNumber=phoneNumber;
    }

    [StringLength(50)]
    public string? Id {get;set;}

    [StringLength(50)]
    public string? Name{ get;set;}

    [StringLength(50)]
    public string? Surname {get;set;}

    [StringLength(50)]
    public string? UserName{get;set;}

    [StringLength(50)]
    public string? Email{get;set;}

    [Phone]
    public string? PhoneNumber{ get; set;}

    public override bool Equals(object? obj)
    {
        return obj is ApplicationUserDTO dTO &&
               Id == dTO.Id &&
               Name == dTO.Name &&
               Surname == dTO.Surname &&
               UserName == dTO.UserName &&
               Email == dTO.Email &&
               PhoneNumber == dTO.PhoneNumber;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Surname, UserName, Email, PhoneNumber);
    }
}