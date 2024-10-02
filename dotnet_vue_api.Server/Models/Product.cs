namespace dotnet_vue_api.Server.Models
{
	public class Product
	{
		public int ProductId { get; set; }
		public required string Name { get; set; }
		public required decimal Price { get; set; }
	}

}
