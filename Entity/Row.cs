using Microsoft.AspNetCore.Identity;

namespace hospital.Entity;

public class Row : IdentityUser
{
    public string FullName { get; set; }
    public string Phone { get; set; }
}