namespace SSI.Ultils
{
    public class PasswordGenerator
    {
        public static string GeneratePassword()
        {
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digits = "0123456789";
            const string specialChars = "!@#$%^&*";

            Random random = new Random();
            int passwordLength = random.Next(4, 13);

            char[] password = new char[passwordLength];

            password[0] = lowerChars[random.Next(lowerChars.Length)];
            password[1] = upperChars[random.Next(upperChars.Length)];
            password[2] = digits[random.Next(digits.Length)];
            password[3] = specialChars[random.Next(specialChars.Length)];

            string allChars = lowerChars + upperChars + digits + specialChars;
            for (int i = 4; i < passwordLength; i++)
            {
                password[i] = allChars[random.Next(allChars.Length)];
            }

            password = password.OrderBy(x => random.Next()).ToArray();

            return new string(password);
        }
    }
}
