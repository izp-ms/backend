using Application.Dto;
using Domain.Entities;

namespace Application.Validators;

public class PostcardTransferValidator
{
    public static bool IsPostcardValid(UserPostcard userPostcard, int newUserId, int? contextUserId)
    {
        if (contextUserId == null)
        {
            throw new Exception("User not found");
        }

        if (userPostcard == null)
        {
            throw new Exception("Postcard not found");
        }

        if (userPostcard.UserId == newUserId)
        {
            throw new Exception("Postcard already belongs to this user");
        }

        if (newUserId == contextUserId)
        {
            throw new Exception("You can't transfer postcard to yourself");
        }

        return true;
    }

    public static bool IsSenderAndReceiverValid(UserStatDto sender, UserStatDto receiver)
    {
        if (sender == null || receiver == null)
        {
            return false;
        }
        return true;
    }
}
