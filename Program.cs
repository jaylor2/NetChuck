using System;
using System.Text.Json;
using System.Text.Json.Nodes;
HttpClient sharedClient = new();


using HttpResponseMessage response = await sharedClient.GetAsync("https://api.chucknorris.io/jokes/random");

response.EnsureSuccessStatusCode();


string jsonResponse = await response.Content.ReadAsStringAsync();
JsonDocument document = JsonDocument.Parse(jsonResponse);

JsonNode? jsonNode = JsonNode.Parse(jsonResponse);
string? valueNode = jsonNode?["value"]?.GetValue<string>();
Console.WriteLine($"Value (JsonNode): {valueNode}");
//Console.WriteLine("{}\n",jsonResponse["value"]);
//Console.WriteLine(valueNode?.GetType());
//Console.WriteLine(jsonNode?.GetType());

	
	

