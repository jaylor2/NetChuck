//using System;
//using System.Text.Json;
//using System.Text.Json.Nodes;
//HttpClient sharedClient = new();


//using HttpResponseMessage response = await sharedClient.GetAsync("https://api.chucknorris.io/jokes/random");

//response.EnsureSuccessStatusCode();


//string jsonResponse = await response.Content.ReadAsStringAsync();
//JsonDocument document = JsonDocument.Parse(jsonResponse);

//JsonNode? jsonNode = JsonNode.Parse(jsonResponse);
//string? valueNode = jsonNode?["value"]?.GetValue<string>();
//Console.WriteLine($"Value (JsonNode): {valueNode}");
//Console.WriteLine("{}\n",jsonResponse["value"]);
//Console.WriteLine(valueNode?.GetType());
//Console.WriteLine(jsonNode?.GetType());

using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace NetChuck;


public class Chuckit
{
	public static async Task Main(string[] args)
	{
		foreach (string item in args)
				{
					Console.WriteLine(item);
				}
		var chuckIt = new Chuckit();
		if (args.Length != 1) chuckIt.PrintHelp();
		string category = args[0];
		var catelist = await chuckIt.CatChuck();
		Console.WriteLine(catelist);

	}
	public void PrintHelp()
	{
		Console.WriteLine("No Argument!");
		Environment.Exit(0);
	}
	//GETs category list and converts to C# String
	public async Task<string> CatChuck()
	{
		using (HttpClient client = new HttpClient())
		{
			try
		 	{
	

				// Define the API endpoint
				string apiUrl = "https://api.chucknorris.io/jokes/categories";
				
				// Send a GET request
				HttpResponseMessage response = await client.GetAsync(apiUrl);
				Console.WriteLine(response);
				// Ensure the request was successful
				response.EnsureSuccessStatusCode(); 	
				
				// Read the response content as a string
				string responseJSON = await response.Content.ReadAsStringAsync();
				ReadOnlySpan<byte> jsonBytes = Encoding.UTF8.GetBytes(responseJSON);
				
				var options = new JsonReaderOptions
				{
				    AllowTrailingCommas = true,
				    CommentHandling = JsonCommentHandling.Skip
				};
				//Stolen UTF8 Reader code from .NET tutorials. Not working. Yet.	
				var reader = new Utf8JsonReader(jsonBytes);

				while (reader.Read())
				{
				    Console.Write(reader.TokenType);

				    switch (reader.TokenType)
				    {
					case JsonTokenType.PropertyName:
					case JsonTokenType.String:
					    {
						string? text = reader.GetString();
						Console.Write(" ");
						Console.Write(text);
						break;
					    }

				    }
				    Console.WriteLine();
				}



				Console.WriteLine("GET Request Successful:");
				Console.WriteLine(JsonBytes.GetType());
				return(jsonBytes);


		   	}
		   	catch (HttpRequestException message)
		   	{
		  		Console.WriteLine($"Request Error: {message}");
				return null;
		  	}
		}
	}
}
