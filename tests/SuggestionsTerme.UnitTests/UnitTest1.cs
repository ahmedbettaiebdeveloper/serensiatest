using SuggestionsTerme.Core;
using System;
using System.Collections.Generic;
using Xunit;

namespace SuggestionsTerme.UnitTests
{
    public class UnitTest1
    {
        private readonly IAmTheTest _test;
        public UnitTest1()
        {
            _test = new AmTheTest();
        }

        [Fact]
        public void GetSuggestions_RenvoyerMatch()
        {
            var suggestions = _test.GetSuggestions("gros", new List<string> { "gros", "gras", "graisse", "aggressif", "go", "ros", "gro" }, 2);
            Assert.Equal(new List<string> { "gros", "gras" }, suggestions);
        }

        [Fact]
        public void GetSuggestions_ListVide()
        {
            var suggestions = _test.GetSuggestions("foo", new List<string> { "bar", "baz", "qux" }, 2);
            Assert.Empty(suggestions);

        }
        [Fact]
        public void GetSuggestions_TestMultipleChoicesAvecMemeScore()
        {
            var suggestions = _test.GetSuggestions("abc", new List<string> { "abb", "aac", "bbc", "bbb", "aba" }, 2);
            Assert.Equal(new List<string> { "aac", "aba" }, suggestions);

        }
        [Fact]
        public void GetSuggestions_ValeurNull()
        {
            Assert.Throws<ArgumentException>(() => _test.GetSuggestions(null, new List<string> { null,null },0));


        }
    }
}