namespace Survey_Basket.Helpers
{
	public static class EmailBodyBuilder
	{
		public static string GenerateEmailBody(string Template, Dictionary<string, string> TemplateModel)
		{
			var TempPath = $"{Directory.GetCurrentDirectory()}/Templates/{Template}.html";
			var streamreader = new StreamReader(TempPath);
			var body = streamreader.ReadToEnd();
			streamreader.Close();
			foreach (var item in TemplateModel)
				body = body.Replace(item.Key, item.Value);

			return body;
		}
	}
}
