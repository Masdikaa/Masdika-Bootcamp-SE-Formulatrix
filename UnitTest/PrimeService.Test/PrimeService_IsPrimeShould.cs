using NUnit.Framework;
using Prime.Services;

namespace Prime.UnitTests.Services {

    [TestFixture]
    public class PrimeService_IsPrimeShould {

        private PrimeServiceX _primeService; // Diuji

        [SetUp] //! dieksekusi sebelum setiap metode tes dijalankan.
        public void SetUp() {
            _primeService = new PrimeServiceX();
        }

        [Test] //! method test
        public void IsPrime_InputIs1_ReturnFalse() {
            var result = _primeService.IsPrime(1);

            //* Assert: Memverifikasi hasilnya.
            //* Memastikan hasilnya adalah 'false'.
            Assert.That(result, Is.False, "1 should not be prime");
        }

    }

}


/*
    * NUnit         : Framework Unit Test
    * [TestFixture] : Atribut yang menandai sebuah kelas berisi kumpulan unit test untuk NUnit
    * [SetUp]       : Atribut yang menandai method yang dijalankan sebelum test (object init)
    * [Test]        : Atribut untuk method uji kasus 
    * [TearDown]    : Method yang di jalankan setelah setiap test
    * [TestCase]    : Memberi beberapa data uji pada satu Test Method
    * [Ignore]      : Melewati test tertentu
    * Assert.That   : Verifikasi dari NUnit 
*/