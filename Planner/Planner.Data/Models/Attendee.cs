﻿namespace Planner.Data.Models;

public class Attendee
{
	public string? FirstName { get; set; }
	
	public string? LastName { get; set; }
	
	public required string NickName { get; set; }

	public string? MailAddress { get; set; }
	
	public bool Confirmed { get; set; }

	public int NumberOfSeats { get; set; } = 1;
	
	public string? LeftSeat { get; set; }
	
	public string? RightSeat { get; set; }
	
	public string SeatIdentifier { get; set; } = "";

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
			RightSeat = RightSeat
		};
	}
}