using System.Collections.Generic;

namespace LS.IntegrationTests.Models
{
    public class ApiResponseError
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public Error Errors { get; set; }
    }

    public class Error
    {
        public List<string> Messages { get; set; }
    }
}
