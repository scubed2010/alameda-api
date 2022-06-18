using Alameda.Business.DTOs.Requests;
using Alameda.Business.DTOs.Responses;
using System.Text.RegularExpressions;

namespace Alameda.Business.Services
{
    public class TextSearchService
    {
        public Task<ServiceResponse<TextSearchResponse>> ExecuteSearch(TextSearchRequest request)
        {
            var matchCount = 0;

            // Remove end of line punctuation
            request.Sentence = Regex.Replace(request.Sentence, @"[.?!]", "");
            request.SearchWord = Regex.Replace(request.SearchWord, @"[.?!]", "");

            if (request.FullWordSearch && request.CaseSensitiveSearch)
            {
                matchCount = FullWordSearch(request.Sentence, request.SearchWord);
            }

            if (!request.FullWordSearch && !request.CaseSensitiveSearch)
            {
                matchCount = WordSearch(request.Sentence.ToLower(), request.SearchWord.ToLower());
            }

            if (!request.FullWordSearch && request.CaseSensitiveSearch)
            {
                matchCount = WordSearch(request.Sentence, request.SearchWord);
            }

            if (request.FullWordSearch && !request.CaseSensitiveSearch)
            {
                matchCount = FullWordSearch(request.Sentence.ToLower(), request.SearchWord.ToLower());
            }

            return Task.FromResult(
                    new ServiceResponse<TextSearchResponse>
                    {
                        Success = true,
                        ResponseObject = new TextSearchResponse { TotalMatches = matchCount }
                    }
                );
        }

        #region Private Methods
        private static int FullWordSearch(string sentence, string searchWord)
        {
            var matchCount = 0;
            var words = sentence.Split(" ");

            var searchWords = searchWord.Split(" ");

            if (searchWords.Length > 1)
            {
                int searchWordIndex = 0;
                int previousMatchIndex = 0;

                for (int i = 0; i < words.Length && searchWordIndex < searchWords.Length; i++)
                {
                    if (words[i] == searchWords[searchWordIndex])
                    {
                        if (searchWordIndex == searchWords.Length - 1)
                        {
                            matchCount++;

                            continue;
                        }

                        // Continue searching only if this is a first match or previous word was a match
                        if (previousMatchIndex == 0 || i - 1 == previousMatchIndex)
                        {
                            previousMatchIndex = i;
                            searchWordIndex++;
                        }
                    }
                    else
                    {
                        // Reset searchWordIndex and previousMatchIndex
                        searchWordIndex = 0;
                        previousMatchIndex = 0;
                    }
                }
            }
            else
            {
                foreach (var word in words)
                {
                    if (word == searchWord)
                    {
                        matchCount++;
                    }
                }
            }

            return matchCount;
        }

        private static int WordSearch(string sentence, string searchWord)
        {
            // Remove any white space from search word
            searchWord = searchWord.Replace(" ", "");

            if (searchWord.Length == 0)
            {
                return 0;
            }

            var sentenceNoWhiteSpace = sentence.Replace(" ", "");
            var removeMatches = sentenceNoWhiteSpace.Replace(searchWord, "");

            if (sentenceNoWhiteSpace.Length == removeMatches.Length)
            {
                return 0;
            }
            else
            {
                return (sentenceNoWhiteSpace.Length - removeMatches.Length) / searchWord.Length;
            }
        }
        #endregion
    }
}
