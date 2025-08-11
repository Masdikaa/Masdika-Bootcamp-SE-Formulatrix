using NuGet.Frameworks;
using NUnit.Framework;
using Prime.Services;

namespace Prime.UnitTests.Services {

    [TestFixture]
    public class PrimeService_IsPrimeShould {

        private PrimeServiceX _primeService; // Class yang diuji

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

        [TestCase(-4)] //* Running 3 test
        [TestCase(0)]
        [TestCase(1)]
        public void IsPrime_ValueLessThan2_ReturnFalse(int value) {
            var result = _primeService?.IsPrime(value);
            Assert.That(result, Is.False, $"{value} should be prime");
        }

        [Test]
        public void IsEvent_ValueEvent_ReturnTrue() {
            var result = _primeService.IsEvent(99918);
            Assert.That(result, Is.True, $"Input should be event number");
        }

        [Test]
        public void IsEvent_ValueEvent_ReturnFalse() {
            var result = _primeService.IsEvent(3);
            Assert.That(result, Is.False, $"Input should be odd number");
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

    ? NOTE 
    ? - AAA Pattern (Arrange Act Assert) - Industrial Standart Pattern dalam menyusun Unit Test
    ? - FIRST Character (Fast, Isolated/Independent, Repeatable, Self-Validating, Timely)
    ? - Red Green Refactor -> Gagal, Berhasil, dan Refactor 
    ? Test Method Naming Convertion
    ? NamaMetodeYangDiuji_SkenarioPengujian_HasilYangDiharapkan => IsPrime_ValueLessThan2_ReturnFalse
    ? Given-When-Then Pattern -> Should_HasilYangDiharapkan_When_SkenarioPengujian => Should_ReturnFalse_When_InputIsLessThan2
*/