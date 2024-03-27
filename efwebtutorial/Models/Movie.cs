using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace efwebtutorial.Models;

public class Movie
{
	public int Id { get; set; }
	public string? Title { get; set; }
	[DisplayName("Release Date")] // this is the name that will be displayed in the view
	[DataType(DataType.Date)]
	public DateTime ReleaseDate { get; set; }
	public string? Genre { get; set; }
	[Column(TypeName = "decimal(18, 2)")]
	public decimal Price { get; set; }
}