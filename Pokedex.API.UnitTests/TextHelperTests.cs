using NUnit.Framework;
using NUnit.Framework.Internal;
using Pokedex.API.Helpers;

namespace Pokedex.API.UnitTests
{
    [TestFixture]
    public class TextHelperTests
    {
        ITextHelper _textHelper = new TextHelper();

        [Test]
        public void FixText_NewLine_ReplacedWithSpace()
        {
            // Arrange
            string originalText = "This is\npokemon";

            // Act
            string fixedText = _textHelper.Fix(originalText);

            // Assert
            Assert.AreEqual("This is pokemon", fixedText);
        }

        [Test]
        public void FixText_PageBreak_ReplacedWithSpace()
        {
            // Arrange
            string originalText = "This is\fpokemon";

            // Act
            string fixedText = _textHelper.Fix(originalText);

            // Assert
            Assert.AreEqual("This is pokemon", fixedText);
        }

        [Test]
        public void FixText_SoftHyphenFollowedByPageBreak_Removed()
        {
            // Arrange
            string originalText = "con\u00ad\ftinuously";

            // Act
            string fixedText = _textHelper.Fix(originalText);

            // Assert
            Assert.AreEqual("continuously", fixedText);
        }

        [Test]
        public void FixText_HardHyphens_RemainOnTheirPlace()
        {
            // Arrange
            string originalText = "break-in";

            // Act
            string fixedText = _textHelper.Fix(originalText);

            // Assert
            Assert.AreEqual("break-in", fixedText);
        }

        [Test]
        public void FixText_HardHyphenFollowedByNewLine_NewLineRemovedButHyphenRemains()
        {
            // Arrange
            string originalText = "break-\nin";

            // Act
            string fixedText = _textHelper.Fix(originalText);

            // Assert
            Assert.AreEqual("break-in", fixedText);
        }

        [Test]
        public void FixText_DashFollowedByNewLine_JustDashRemains()
        {
            // Arrange
            string originalText = "You may think she is a liar -\nshe isn't.";

            // Act
            string fixedText = _textHelper.Fix(originalText);

            // Assert
            Assert.AreEqual("You may think she is a liar - she isn't.", fixedText);
        }
    }
}
