namespace Domain.Enums;

public enum RegistrationResult
{
    Success,
    PasswordsDoNotMatch,
    EmailAlreadyExists,
    IncorrectEmail,
    IncorrectNickName,
    WeakPassword,
}
