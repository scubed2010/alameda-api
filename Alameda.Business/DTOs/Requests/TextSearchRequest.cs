namespace Alameda.Business.DTOs.Requests
{
    public class TextSearchRequest
    {
        public string Sentence { get; set; }
        public string SearchWord { get; set; }
        public bool FullWordSearch { get; set; }
        public bool CaseSensitiveSearch { get; set; }
    }
}
