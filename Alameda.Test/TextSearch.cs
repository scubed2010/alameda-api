using Alameda.Business.DTOs.Requests;
using Alameda.Business.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alameda.Test
{
    [TestClass]
    public class TextSearch
    {
        private readonly TextSearchService _textSearchService;
        private static TextSearchRequest _request;

        public TextSearch()
        {
            _textSearchService = new TextSearchService();
            _request = new TextSearchRequest
            {
                Sentence = "Coders who code don't always eat cod. Exclaimed the coder who codes CODE."
            };
        }

        [TestMethod]
        public void NotFullWordNotCaseSensitive()
        {
            _request.SearchWord = "Cod";
            _request.FullWordSearch = false;
            _request.CaseSensitiveSearch = false;

            var result = _textSearchService.ExecuteSearch(_request).Result;

            Assert.IsTrue(result.ResponseObject.TotalMatches == 6);
        }

        [TestMethod]
        public void FullWordNotCaseSensitive()
        {
            _request.SearchWord = ".";
            _request.FullWordSearch = true;
            _request.CaseSensitiveSearch = false;

            var result = _textSearchService.ExecuteSearch(_request).Result;

            Assert.IsTrue(result.ResponseObject.TotalMatches == 0);
        }

        [TestMethod]
        public void FullWordCaseSensitiveCase1()
        {
            _request.SearchWord = "cod";
            _request.FullWordSearch = true;
            _request.CaseSensitiveSearch = true;

            var result = _textSearchService.ExecuteSearch(_request).Result;

            Assert.IsTrue(result.ResponseObject.TotalMatches == 1);
        }

        [TestMethod]
        public void FullWordCaseSensitiveCase2()
        {
            _request.SearchWord = "CODE.";
            _request.FullWordSearch = true;
            _request.CaseSensitiveSearch = true;

            var result = _textSearchService.ExecuteSearch(_request).Result;

            Assert.IsTrue(result.ResponseObject.TotalMatches == 1);
        }

        [TestMethod]
        public void FullWordCaseSensitiveCase3()
        {
            _request.SearchWord = "don't always";
            _request.FullWordSearch = true;
            _request.CaseSensitiveSearch = true;

            var result = _textSearchService.ExecuteSearch(_request).Result;

            Assert.IsTrue(result.ResponseObject.TotalMatches == 1);
        }

        // Custom Test Cases
        [TestMethod]
        public void MultiWordNotFullWordCaseSensitive()
        {
            _request.SearchWord = "don't always";
            _request.FullWordSearch = false;
            _request.CaseSensitiveSearch = true;

            var result = _textSearchService.ExecuteSearch(_request).Result;

            Assert.IsTrue(result.ResponseObject.TotalMatches == 1);
        }

        [TestMethod]
        public void MultiWordNotFullWordNotCaseSensitive()
        {
            _request.SearchWord = "don't always";
            _request.FullWordSearch = false;
            _request.CaseSensitiveSearch = false;

            var result = _textSearchService.ExecuteSearch(_request).Result;

            Assert.IsTrue(result.ResponseObject.TotalMatches == 1);
        }

        [TestMethod]
        public void MultiWordFullWordNotCaseSensitive()
        {
            _request.SearchWord = "don't always";
            _request.FullWordSearch = true;
            _request.CaseSensitiveSearch = false;

            var result = _textSearchService.ExecuteSearch(_request).Result;

            Assert.IsTrue(result.ResponseObject.TotalMatches == 1);
        }

        [TestMethod]
        public void MultiWordNotFullWordNotCaseSensitiveWithPuncuation()
        {
            _request.SearchWord = "cod. Exclaimed";
            _request.FullWordSearch = false;
            _request.CaseSensitiveSearch = false;

            var result = _textSearchService.ExecuteSearch(_request).Result;

            Assert.IsTrue(result.ResponseObject.TotalMatches == 1);
        }

        [TestMethod]
        public void MultiWordNotFullWordNotCaseSensitiveWithPartialWord()
        {
            _request.SearchWord = "don't always ea";
            _request.FullWordSearch = false;
            _request.CaseSensitiveSearch = true;

            var result = _textSearchService.ExecuteSearch(_request).Result;

            Assert.IsTrue(result.ResponseObject.TotalMatches == 1);
        }

        [TestMethod]
        public void MultiWordFullWordNotCaseSensitiveWithPartialWord()
        {
            _request.SearchWord = "don't always ea";
            _request.FullWordSearch = true;
            _request.CaseSensitiveSearch = false;

            var result = _textSearchService.ExecuteSearch(_request).Result;

            Assert.IsTrue(result.ResponseObject.TotalMatches == 0);
        }

        [TestMethod]
        public void MultiWordFullWordNotCaseSensitiveWithNonConsecutiveWords()
        {
            _request.SearchWord = "code eat cod";
            _request.FullWordSearch = true;
            _request.CaseSensitiveSearch = false;

            var result = _textSearchService.ExecuteSearch(_request).Result;

            Assert.IsTrue(result.ResponseObject.TotalMatches == 0);
        }

        [TestMethod]
        public void MultiWordFullWordNotCaseSensitiveWithConsecutiveWords()
        {
            _request.SearchWord = "who code";
            _request.FullWordSearch = true;
            _request.CaseSensitiveSearch = false;

            var result = _textSearchService.ExecuteSearch(_request).Result;

            Assert.IsTrue(result.ResponseObject.TotalMatches == 1);
        }

        [TestMethod]
        public void NotFullWordCaseSensitive()
        {
            _request.SearchWord = "cod";
            _request.FullWordSearch = false;
            _request.CaseSensitiveSearch = true;

            var result = _textSearchService.ExecuteSearch(_request).Result;

            Assert.IsTrue(result.ResponseObject.TotalMatches == 4);
        }
    }
}