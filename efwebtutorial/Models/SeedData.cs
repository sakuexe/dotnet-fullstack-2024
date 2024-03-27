using efwebtutorial.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using efwebtutorial.Models;
using System;
using System.Linq;

namespace MvcMovie.Models;

// this class is called in the Program.cs file.
public static class SeedData
{
	// seed the database with test data
	// by this we mean that we will add some data to the database when the application is started
	// we also check if there already is data in the database
	public static void Initialize(IServiceProvider serviceProvider)
	{
		using (var context = new EfWebTutorialContext(
			serviceProvider.GetRequiredService<
				DbContextOptions<EfWebTutorialContext>>()))
		{
			// Look for any movies.
			if (context.Movie.Any())
			{
				return;   // DB has been seeded
			}
			context.Movie.AddRange(
				new Movie
				{
					Title = "When Harry Met Sally",
					ReleaseDate = DateTime.Parse("1989-2-12"),
					Genre = "Romantic Comedy",
					Price = 7.99M
				},
				new Movie
				{
					Title = "Ghostbusters ",
					ReleaseDate = DateTime.Parse("1984-3-13"),
					Genre = "Comedy",
					Price = 8.99M
				},
				new Movie
				{
					Title = "Ghostbusters 2",
					ReleaseDate = DateTime.Parse("1986-2-23"),
					Genre = "Comedy",
					Price = 9.99M
				},
				new Movie
				{
					Title = "Rio Bravo",
					ReleaseDate = DateTime.Parse("1959-4-15"),
					Genre = "Western",
					Price = 3.99M
				}
			);
			context.SaveChanges();
		}
	}
}