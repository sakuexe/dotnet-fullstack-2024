
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace blazeit.Models;

public class TodoItem : IHasId
{
	[BsonId]
	[BsonRepresentation(BsonType.String)]
	public ObjectId _id { get; set; }
	[MinLength(2)]
	public string Title { get; set; }
	public bool IsCompleted { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.Now;

	public TodoItem(string title)
	{
		_id = ObjectId.GenerateNewId();
		Title = title;
	}
}