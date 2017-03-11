using Moq;
using System;
using Xunit;

namespace Examiner.Domain.UnitTest
{
	public class TaskElementTests
	{
		private readonly string arbitraryName = Guid.NewGuid().ToString();
		private readonly string arbitraryDescription = Guid.NewGuid().ToString();
		private const decimal arbitraryValue = 42m;

		[Fact]
		public void SutIsTaskComponent()
		{
			var sut = new TaskElement(arbitraryName, arbitraryValue);

			Assert.IsAssignableFrom<ITaskComponent>(sut);
		}

		[Fact]
		public void NameIsCorrect()
		{
			var expected = Guid.NewGuid().ToString();
			var sut = new TaskElement(expected, arbitraryValue);

			Assert.Equal(expected, sut.Name);
		}

		[Fact]
		public void NameCannotBeNull()
		{
			var exception = Record.Exception(() => new TaskElement(null, arbitraryValue));

			Assert.IsType<ArgumentNullException>(exception);
		}

		[Fact]
		public void NameCannotBeEmptyString()
		{
			var exception = Record.Exception(() => new TaskElement(string.Empty, arbitraryValue));

			Assert.IsType<ArgumentException>(exception);
		}

		[Theory]
		[InlineData(" name")]
		[InlineData("nam e")]
		[InlineData("name ")]
		public void NameCannotContainWhitespace(string nameWithWhitespace)
		{
			var exception = Record.Exception(() => new TaskElement(nameWithWhitespace, arbitraryValue));

			Assert.IsType<ArgumentException>(exception);
		}

		[Fact]
		public void DescriptionDefaultValueIsEmptyString()
		{
			var sut = new TaskElement(arbitraryName, arbitraryValue);

			Assert.Equal(string.Empty, sut.Description);
		}

		[Fact]
		public void DescriptionIsCorrect()
		{
			var expected = Guid.NewGuid().ToString();
			var sut = new TaskElement(arbitraryName, expected, arbitraryValue);

			Assert.Equal(expected, sut.Description);
		}

		[Fact]
		public void DescriptionCannotBeNull()
		{
			var exception = Record.Exception(() => new TaskElement(arbitraryName, null, arbitraryValue));

			Assert.IsType<ArgumentNullException>(exception);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		public void ValueIsCorrect(decimal expected)
		{
			var sut = new TaskElement(arbitraryName, expected);

			Assert.Equal(expected, sut.Value);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		public void ValueMustBeGreaterThanZero(decimal value)
		{
			var exception = Record.Exception(() => new TaskElement(arbitraryName, value));

			Assert.IsType<ArgumentException>(exception);
		}

		[Fact]
		public void AcceptCallsVisitor()
		{
			var sut = new TaskElement(arbitraryName, arbitraryValue);
			var visitorStub = new Mock<ITaskVisitor>();

			sut.Accept(visitorStub.Object);

			visitorStub.Verify(v => v.Visit(sut));
		}
	}
}
