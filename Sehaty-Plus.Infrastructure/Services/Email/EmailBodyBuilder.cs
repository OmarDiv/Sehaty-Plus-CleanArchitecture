namespace Sehaty_Plus.Infrastructure.Services.Email
{
    public static class EmailBodyBuilder
    {
        public static string GenerateEmailBody(string template, Dictionary<string, string> templateBody)
        {
            var basePath = Path.Combine(AppContext.BaseDirectory, "Services", "Email", "Templates");
            var templatePath = Path.Combine(basePath, $"{template}.html");

            if (!File.Exists(templatePath))
                throw new FileNotFoundException($"Email template not found at path: {templatePath}");

            var body = File.ReadAllText(templatePath);

            foreach (var item in templateBody)
                body = body.Replace(item.Key, item.Value);

            return body;
        }
    }
}
