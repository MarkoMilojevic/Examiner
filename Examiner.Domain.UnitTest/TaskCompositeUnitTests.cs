using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Examiner.Domain.UnitTest
{
	public class TaskCompositeUnitTests
	{
		private readonly string arbitraryName = Guid.NewGuid().ToString();
		private readonly string arbitraryDescription = Guid.NewGuid().ToString();

		[Fact]
		public void SutIsTaskComponent()
		{
			var sut = new TaskComposite(arbitraryName);

			Assert.IsAssignableFrom<ITaskComponent>(sut);
		}

		[Fact]
		public void NameIsCorrect()
		{
			var expected = Guid.NewGuid().ToString();
			var sut = new TaskComposite(expected);

			Assert.Equal(expected, sut.Name);
		}

		[Fact]
		public void NameCannotBeNull()
		{
			var exception = Record.Exception(() => new TaskComposite(null));

			Assert.IsType<ArgumentNullException>(exception);
		}

		[Fact]
		public void NameCannotBeEmptyString()
		{
			var exception = Record.Exception(() => new TaskComposite(string.Empty));

			Assert.IsType<ArgumentException>(exception);
		}

		[Theory]
		[InlineData(" name")]
		[InlineData("nam e")]
		[InlineData("name ")]
		public void NameCannotContainWhitespace(string nameWithWhitespace)
		{
			var exception = Record.Exception(() => new TaskComposite(nameWithWhitespace));

			Assert.IsType<ArgumentException>(exception);
		}

		[Fact]
		public void DescriptionIsCorrect()
		{
			var expected = Guid.NewGuid().ToString();
			var sut = new TaskComposite(arbitraryName, expected);

			Assert.Equal(expected, sut.Description);
		}

		[Fact]
		public void DescriptionDefaultValueIsEmptyString1()
		{
			var sut = new TaskComposite(arbitraryName);

			Assert.Equal(string.Empty, sut.Description);
		}

		[Fact]
		public void DescriptionDefaultValueIsEmptyString2()
		{
			var sut = new TaskComposite(arbitraryName, new List<ITaskComponent>());

			Assert.Equal(string.Empty, sut.Description);
		}

		[Fact]
		public void DescriptionCannotBeNull1()
		{
			var exception = Record.Exception(() => new TaskComposite(arbitraryName, description: null));

			Assert.IsType<ArgumentNullException>(exception);
		}

		[Fact]
		public void DescriptionCannotBeNull2()
		{
			var exception = Record.Exception(() => new TaskComposite(arbitraryName, null, new List<ITaskComponent>()));

			Assert.IsType<ArgumentNullException>(exception);
		}

		[Fact]
		public void ComponentsIsCorrect1()
		{
			var sut = new TaskComposite(arbitraryName);

			Assert.False(sut.Any());
		}

		[Fact]
		public void ComponentsIsCorrect2()
		{
			var sut = new TaskComposite(arbitraryName, arbitraryDescription);

			Assert.False(sut.Any());
		}

		[Fact]
		public void ComponentsIsCorrect3()
		{
			var sut = new TaskComposite(arbitraryName, components: new List<ITaskComponent>());

			Assert.False(sut.Any());
		}

		[Fact]
		public void ComponentsIsCorrect4()
		{
			var sut = new TaskComposite(arbitraryName, arbitraryDescription, new List<ITaskComponent>());

			Assert.False(sut.Any());
		}

		[Fact]
		public void ComponentsIsCorrect5()
		{
			var components = new List<ITaskComponent> { new Mock<ITaskComponent>().Object };
			var sut = new TaskComposite(arbitraryName, arbitraryDescription, components);

			Assert.True(sut.Count() == components.Count && sut.SequenceEqual(components));
		}

		[Fact]
		public void ComponentsCannotBeNull1()
		{
			var exception = Record.Exception(() => new TaskComposite(arbitraryName, components: null));

			Assert.IsType<ArgumentNullException>(exception);
		}

		[Fact]
		public void ComponentsCannotBeNull2()
		{
			var exception = Record.Exception(() => new TaskComposite(arbitraryName, arbitraryDescription, null));

			Assert.IsType<ArgumentNullException>(exception);
		}

		[Fact]
		public void ComponentsCannotContainNull1()
		{
			var exception = Record.Exception(() => new TaskComposite(arbitraryName, components: new List<ITaskComponent> { null }));

			Assert.IsType<ArgumentException>(exception);
		}

		[Fact]
		public void ComponentsCannotContainNull2()
		{
			var exception = Record.Exception(() => new TaskComposite(arbitraryName, arbitraryDescription, new List<ITaskComponent> { null }));

			Assert.IsType<ArgumentException>(exception);
		}

		[Fact]
		public void AcceptCallsVisitor()
		{
			var sut = new TaskComposite(arbitraryName);
			var visitorStub = new Mock<ITaskVisitor>();

			sut.Accept(visitorStub.Object);

			visitorStub.Verify(v => v.Visit(sut));
		}

		[Fact]
		public void AddComponentToComposite()
		{
			var sut = new TaskComposite(arbitraryName);
			var component = new Mock<ITaskComponent>().Object;

			sut.Add(component);

			Assert.True(sut.Any(tc => tc == component));
		}

		[Fact]
		public void CannotAddNullToComposite()
		{
			var sut = new TaskComposite(arbitraryName);

			var exception = Record.Exception(() => sut.Add(null));

			Assert.IsType<ArgumentNullException>(exception);
		}

		[Fact]
		public void RemoveComponentFromComposite1()
		{
			var component = new Mock<ITaskComponent>().Object;
			var sut = new TaskComposite(arbitraryName, components: new List<ITaskComponent>() { component });
			
			sut.Remove(component);

			Assert.False(sut.Any(tc => tc == component));
		}

		[Fact]
		public void RemoveComponentFromComposite2()
		{
			var sut = new TaskComposite(arbitraryName);
			var component = new Mock<ITaskComponent>().Object;

			sut.Add(component);
			sut.Remove(component);

			Assert.False(sut.Any(tc => tc == component));
		}

		[Fact]
		public void ContainsComponentInComposite1()
		{
			var component = new Mock<ITaskComponent>().Object;
			var sut = new TaskComposite(arbitraryName, components: new List<ITaskComponent>() { component });
			
			Assert.True(sut.Contains(component));
		}

		[Fact]
		public void ContainsComponentInComposite2()
		{
			var sut = new TaskComposite(arbitraryName);
			var component = new Mock<ITaskComponent>().Object;

			sut.Add(component);

			Assert.True(sut.Contains(component));
		}

		[Fact]
		public void ContainsComponentInComposite3()
		{
			var component = new Mock<ITaskComponent>().Object;
			var sut = new TaskComposite(arbitraryName, components: new List<ITaskComponent>() { component });

			sut.Remove(component);

			Assert.False(sut.Contains(component));
		}

		[Fact]
		public void ContainsComponentInComposite4()
		{
			var sut = new TaskComposite(arbitraryName);
			var component = new Mock<ITaskComponent>().Object;

			sut.Add(component);
			sut.Remove(component);

			Assert.False(sut.Contains(component));
		}
	}
}
