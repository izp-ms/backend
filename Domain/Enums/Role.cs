using System.ComponentModel;

namespace Domain.Enums;

public enum Role
{
    [Description("User")]
    User,
    [Description("Admin")]
    Admin,
}
