using System;
using System.Collections.Generic;
using Xunit;

namespace Examiner.Domain.UnitTest
{
	public class ValueTaskVisitorUnitTests
	{
		private readonly string arbitraryName = Guid.NewGuid().ToString();

		[Fact]
		public void SutIsTaskVisitor()
		{
			var sut = new ValueTaskVisitor();

			Assert.IsAssignableFrom<ITaskVisitor>(sut);
		}

		[Fact]
		public void SutDefaultValueIsZero()
		{
			var expected = decimal.Zero;
			var sut = new ValueTaskVisitor();

			Assert.Equal(expected, sut.Value);
		}

		[Theory]
		[InlineData(1, 2, 3)]
		[InlineData(13, 21, 34)]
		public void ValueIsCorrect(decimal value1, decimal value2, decimal value3)
		{
			var expected = value1 + value2 + value3;
			var e1 = new TaskElement(arbitraryName, value1);
			var e2 = new TaskElement(arbitraryName, value2);
			var e3 = new TaskElement(arbitraryName, value3);
			var c1 = new TaskComposite(arbitraryName, components: new List<ITaskComponent> { e2, e3 });
			var rootComposite = new TaskComposite(arbitraryName, components: new List<ITaskComponent> { e1, c1 });
			var sut = new ValueTaskVisitor();

			rootComposite.Accept(sut);

			Assert.Equal(expected, sut.Value);
		}
	}
}
