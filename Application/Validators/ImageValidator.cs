using System.Drawing;

namespace Application.Validators;

public class ImageValidator
{
    public static bool IsImageValid(string base64Image)
    {
        byte[] imageBytes = Convert.FromBase64String(base64Image);

        using MemoryStream ms = new MemoryStream(imageBytes);
        using Image image = Image.FromStream(ms);
        double targetAspectRatio = 4.0 / 6.0;
        double imageAspectRatio = image.Width / image.Height;

        if (Math.Abs(imageAspectRatio - targetAspectRatio) < 0.01)
        {
            return true;
        }

        return false;
    }
}
