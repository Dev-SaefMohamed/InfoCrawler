**C# Console App: Fetching and Printing HTML

What the app does -> It creates a reusable HttpClient object.

It sets headers (like User-Agent) to make requests look like a real browser.

It defines a method FetchHtmlAsync that takes a URL, sends an HTTP GET request, and returns the page’s HTML.

Instead of saving HTML to a file, it prints the HTML directly in the terminal.

Explanation of FetchHtmlAsync

Input: HttpClient instance and a URL.

Process: Sends an HTTP GET request.

Error handling: EnsureSuccessStatusCode() makes sure you get a proper 200 response; otherwise, it throws an error.

Output: The full HTML source code as a string.

User-Agent and DefaultRequestHeaders

User-Agent: Tells the server what client is making the request (browser name, OS, etc.). Helps avoid blocks or stripped-down responses.

DefaultRequestHeaders: A set of headers automatically attached to every request made by that HttpClient.**

Accept: Specifies the content type you expect.

Accept-Language: Requests a preferred language.

Accept-Encoding: Requests compressed content (gzip, deflate, br).

Connection: Suggests whether to keep the TCP connection alive.
