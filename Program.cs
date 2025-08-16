using HtmlAgilityPack;   // Library for parsing and navigating HTML documents
using System;            // Basic system functions (Console, Exception, etc.)
using System.Net.Http;   // Provides HttpClient for sending HTTP requests
using System.Threading.Tasks; // For asynchronous programming (async/await)
using System.Linq;       // For LINQ operations like Select, Where, ToList

public class WebScraper
{
    public static async Task Main(string[] args)
    {
        // (1) Ask the user to enter the target URL
        Console.WriteLine("-- Enter The URL --");
        string url = Console.ReadLine(); // Read input from the terminal

        try
        {
            // (2) Fetch HTML content from the given URL (async method)
            string html = await FetchHtmlAsync(url);

            // (3) Create an HtmlDocument instance to parse the HTML string
            var htmlDoc = new HtmlDocument();

            // (4) Load the fetched HTML into the HtmlDocument object
            htmlDoc.LoadHtml(html);

            // (5) Use XPath to select all <div> elements in the document
            var divs = htmlDoc.DocumentNode
                .SelectNodes("//div")               // Get all <div> tags
                ?.Select(d => d.InnerText.Trim())   // Extract and clean inner text
                .Where(t => !string.IsNullOrEmpty(t)) // Remove empty or null strings
                .ToList();                          // Convert results to a list

            // (6) Print the extracted data in the terminal
            if (divs != null)
            {
                Console.WriteLine("Scraped Data:");
                
                foreach (var div in divs)
                {
                    Console.WriteLine(div); // Print the text inside the <div>
                    Console.WriteLine("--------------------------------------------------"); // Separator for readability
                }
            }
            else
            {
                Console.WriteLine("No <div> elements found.");
            }
        }
        catch (Exception ex)
        {
            // (7) Catch errors (invalid URL, network issue, or parsing errors)
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    // Helper Method: Sends HTTP GET request and returns the HTML response
    private static async Task<string> FetchHtmlAsync(string url)
    {
        // Create an HttpClient instance to send HTTP requests
        using var httpClient = new HttpClient();

        // Add a powerful User-Agent (pretend to be Chrome on Windows 10)
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
            "AppleWebKit/537.36 (KHTML, like Gecko) " +
            "Chrome/120.0.0.0 Safari/537.36"
        );

        // extra headers to look like a real browser
        httpClient.DefaultRequestHeaders.Accept.ParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
        httpClient.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.9");
        httpClient.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate, br");
        httpClient.DefaultRequestHeaders.Connection.ParseAdd("keep-alive");

        // Send GET request asynchronously and return HTML content as a string
        return await httpClient.GetStringAsync(url);
    }
}
