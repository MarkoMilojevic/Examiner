using System;

namespace Examiner.Domain
{
	public class Note
	{
		public string Text { get; }

		public NoteType Type { get; }
		
		public Note(string text, NoteType type)
		{
			Text = text ?? throw new ArgumentNullException(nameof(text));
			Type = type;
		}
	}
}
