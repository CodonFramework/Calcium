#region File and License Information
/*
<File>
	<License>
		Copyright � 2009 - 2024, Daniel Vaughan. All rights reserved.
		This file is part of Calcium (http://calciumframework.com), 
		which is released under the MIT License.
		See file /Documentation/License.txt for details.
	</License>
	<CreationDate>2024-10-20 05:03:12Z</CreationDate>
</File>
*/
#endregion

using FluentAssertions;

namespace Calcium.ResourcesModel
{
	public partial class StringParserServiceTests
	{
		[Fact]
		public void ParseWithCustomDelimiters_ShouldReturnValue()
		{
			StringParserService service = new();
			var customDelimiters = new TagDelimiters("<%=", "%>");

			string content = "<%=Tag1%>";
			var substitutions = new Dictionary<string, string> { { "Tag1", "CustomReplacement" } };

			var result = service.Parse(content, substitutions, customDelimiters);

			result.Should().Be("CustomReplacement");
		}

		[Fact]
		public void ParseWithCustomDelimiters_MultipleSubstitutions()
		{
			StringParserService service = new();
			var customDelimiters = new TagDelimiters("<%=", "%>");

			string content = "<%=Tag1%> <%=Tag2%>";
			var substitutions = new Dictionary<string, string>
			{
				{ "Tag1", "CustomReplacement1" },
				{ "Tag2", "CustomReplacement2" }
			};

			var result = service.Parse(content, substitutions, customDelimiters);

			result.Should().Be("CustomReplacement1 CustomReplacement2");
		}

		[Fact]
		public void ParseWithCustomDelimiters_ShouldReturnOriginalTextForUnknownTags()
		{
			StringParserService service = new();
			var customDelimiters = new TagDelimiters("<%=", "%>");

			string content = "<%=UnknownTag%>";

			var result = service.Parse(content, new Dictionary<string, string>(), customDelimiters);

			result.Should().Be(content); // Should return the original text if the tag is unknown
		}
	}
}