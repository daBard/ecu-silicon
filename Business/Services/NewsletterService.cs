namespace Business.Services;

// !! TESTING

public class NewsletterService
{
    string apiUrl = @"https://localhost:7125/api/Subscriber";

    public async Task<bool> SubscribeToNewsletterAsync()
    {
        // Create HttpClient instance
        using var client = new HttpClient();

        // Make GET request to API
        //HttpResponseMessage response = await client.PostAsync();

        return true;
    }
    
}

    
    
    