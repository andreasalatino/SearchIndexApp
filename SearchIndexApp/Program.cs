using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;
using SearchIndexApp;
using Azure.Search;
using Microsoft.Azure.Search;

namespace SearchIndexApp {
	class Program
	{

		static void Main(string[] args)
		{
			string serviceName = "languageanalios-asq6yt5nl2tq7ro";
			string apiKey = "emrqcuZl1bfC0fq0joUSzKdgdfdG7FhrM15yqupDspAzSeBQx9Qu";
			string indexName = "indexitem";

			// Create a SearchIndexClient to send create/delete index commands
			Uri serviceEndpoint = new Uri($"https://{serviceName}.search.windows.net/");
			AzureKeyCredential credential = new AzureKeyCredential(apiKey);
			SearchIndexClient adminClient = new SearchIndexClient(serviceEndpoint, credential);

			// Create a SearchClient to load and query documents
			SearchClient srchclient = new SearchClient(serviceEndpoint, indexName, credential);

			Console.WriteLine("Test");

			RunQueries(srchclient);
		}

		// Write search results to console
		private static void WriteDocuments(SearchResults<Item> searchResults)
		{
			foreach (SearchResult<Item> result in searchResults.GetResults())
			{
				Console.WriteLine(result.Document);
			}

			Console.WriteLine();
		}

		private static void WriteDocuments(AutocompleteResults autoResults)
		{
			foreach (AutocompleteItem result in autoResults.Results)
			{
				Console.WriteLine(result.Text);
			}

			Console.WriteLine();
		}

		// Run queries, use WriteDocuments to print output
		private static void RunQueries(SearchClient srchclient)
		{
			SearchOptions options;
			SearchResults<Item> response;

			// Query 1
			Console.WriteLine("Query #1: Search on empty term '*' to return all documents, showing a subset of fields...\n");

			options = new SearchOptions()
			{
				IncludeTotalCount = true,
				Filter = "",
				OrderBy = { "" }
			};

			options.Select.Add("id");
			options.Select.Add("link");
			options.Select.Add("condition");

			response = srchclient.Search<Item>("*", options);
			WriteDocuments(response);
		}
	}
}

