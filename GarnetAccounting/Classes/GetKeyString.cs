namespace GarnetAccounting.Classes
{
    public static class GetKeyString
    {
        public static async Task<(string Content, string ErrorMessage, bool IsSuccess)> ProcessFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return (null, "محتوای فایل خالی است", false);
            }

            try
            {
                using var reader = new StreamReader(file.OpenReadStream());
                var allLines = await reader.ReadToEndAsync();
                //var lines = allLines.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                var keyContent = allLines;  //string.Join("\n", lines.Skip(1).Take(lines.Length - 2));


                return (keyContent, null, true);
            }
            catch (Exception ex)
            {
                return (null, ex.Message, false);
            }
        }
    }

}
