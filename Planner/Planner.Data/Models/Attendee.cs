using System.Text.Json.Serialization;

namespace Planner.Data.Models;

public class Attendee
{
	private string? _leftSeat;
	private string? _rightSeat;

	public string? FirstName { get; set; }
	
	public string? LastName { get; set; }
	
	public required string NickName { get; set; }

	public string? MailAddress { get; set; }
	
	public bool Confirmed { get; set; }

	public int NumberOfSeats { get; set; } = 1;

	public string? LeftSeat
	{
		get => _leftSeat;
		set
		{
			_leftSeat = value;

			if (_leftSeat != "Expert" && _rightSeat != "Expert")
				IsLegend = false;
		}
	}

	public string? RightSeat
	{
		get => _rightSeat;
		set
		{
			_rightSeat = value;
			
			if (_leftSeat != "Expert" && _rightSeat != "Expert")
				IsLegend = false;
		}
	}

	public string SeatIdentifier { get; set; } = "";
	
	public string? MemberGroup { get; set; }
	
	public bool IsLegend { get; set; }

	[JsonIgnore]
	public int SeatIdentifierNumber => !string.IsNullOrEmpty(SeatIdentifier) ? int.Parse(SeatIdentifier.Split('-')[1]) : 0;

	public void SetSeatNumber(int number)
	{
		if (string.IsNullOrEmpty(SeatIdentifier)) return;

		var seat = SeatIdentifier.Split('-')[0];
		SeatIdentifier = $"{seat}-{number}";
	}

	public void UpdateSelf(Attendee a)
	{
		FirstName = a.FirstName;
		LastName = a.LastName;
		NickName = a.NickName;
		MailAddress = a.MailAddress;
		Confirmed = a.Confirmed;
		NumberOfSeats = a.NumberOfSeats;
		SeatIdentifier = a.SeatIdentifier;
		LeftSeat = a.LeftSeat;
		RightSeat = a.RightSeat;
		MemberGroup = a.MemberGroup;
		IsLegend = a.IsLegend;
	}

	public Attendee Clone()
	{
		return new Attendee()
		{
			FirstName = FirstName,
			LastName = LastName,
			NickName = NickName,
			MailAddress = MailAddress,
			Confirmed = Confirmed,
			NumberOfSeats = NumberOfSeats,
			SeatIdentifier = SeatIdentifier,
			LeftSeat = LeftSeat,
			RightSeat = RightSeat,
			MemberGroup = MemberGroup,
			IsLegend = IsLegend,
		};
	}
}