using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using BloomFilter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace TennisKataApprovalTests
{
    [UseReporter(typeof(DiffReporter))]
    [TestClass]
    public class Tests
    {

        [Test]
        public void TestList()
        {
            var names = new[] { "Llewellyn", "James", "Dan", "Jason", "Katrina" };
            Array.Sort(names);
            Approvals.VerifyAll(names, label: "");
        }

        #region "DELETE"
        [TestMethod]
        public void MainLineTestCaseMatch()
        {
            BloomFilter.IHash<string, int> hash = new SimpleHash();
            BloomFilter.IBloomFilter<string, int> bloomFilter = new BloomFilter.BloomFilter(hash, 1000, 3);
            var testWord = "blarg";
            bloomFilter.a(testWord);
            var result1 = bloomFilter.zzzz(testWord);
            Assert.IsTrue(result1, $"{testWord} was added to the filter, and should be the only item in the filter.  Your filter/ hash is broken");
        }

        [TestMethod]
        public void MainLineTestCaseNoMatch()
        {

            BloomFilter.IHash<string, int> hash = new SimpleHash();
            BloomFilter.IBloomFilter<string, int> bloomFilter = new BloomFilter.BloomFilter(hash, 1000, 3);
            var testWord = "blarg";
            var testWord2 = "blargAgain";

            bloomFilter.a(testWord);
            bloomFilter.a(testWord2);
            var result1 = bloomFilter.zzzz(testWord);

            Assert.IsTrue(result1, $"{testWord2} was not added to the filter, and should be the only item in the filter.  Your filter/ hash is broken");
        }

        [TestMethod]
        public void End2EndTest()
        {
            var stored = new List<string>() { "the", "most", "pressing", "task", "is", "to", "teach", "people", "how", "to", "learn." };
            var notstored = new List<string>();

            BloomFilter.IHash<string, int> hash = new SimpleHash();
            BloomFilter.IBloomFilter<string, int> bloomFilter = new BloomFilter.BloomFilter(hash, 1000, 3);

            foreach (var word in stored)
            {
                bloomFilter.a(word);
                notstored.Add(word.ToUpper());
            }

            foreach (var word in notstored)
            {
                Assert.IsFalse(bloomFilter.zzzz(word), $"{word} was not stored,how was it found?");
            }

        }
        [TestClass]
        public class SimpleHashTest
        {

            /// <summary>
            /// Test to check that the Simple Hash hashes the same for each object.
            /// </summary>
            [TestMethod]
            public void HashTestPositive()
            {
                var hash = new SimpleHash();

                var testWord = "blarg";
                var testWord2 = "blarg";
                var result1 = hash.Hash(testWord);
                var result2 = hash.Hash(testWord2);

                Assert.AreEqual(result1, result2, $"{result1} and {result2} should be identical, the hash algorithm has errors.");
            }

            /// <summary>
            /// Negative test to check that the Simple Hash hashes the same for each object.
            /// </summary>
            [TestMethod]
            public void HashTestMegative()
            {
                var hash = new SimpleHash();

                var testWord = "blarg";
                var testWord2 = "blargagain";
                var result1 = hash.Hash(testWord);
                var result2 = hash.Hash(testWord2);

                Assert.AreNotEqual(result1, result2, $"{result1} and {result2} should not be identical, the hash algorithm has errors.");
            }
        }
    }
    #endregion
}
