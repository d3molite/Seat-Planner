using MudBlazor;
using Planner.Data.Interfaces;
using Planner.Data.Models;
using Planner.Data.Services;

namespace Planner.Ui.Helpers;

public sealed class DropZoneHelper
{
	private readonly ISeatingConfigurationService _seatingService;

	private List<Attendee> Attendees => _seatingService.Attendees;

	public DropZoneHelper(ISeatingConfigurationService seatingService)
	{
		_seatingService = seatingService;
	}

	public void ItemUpdated(MudItemDropInfo<Attendee> dropItem)
	{
		if (dropItem.DropzoneIdentifier != "")
		{
			if (ZoneHasAttendees(dropItem.DropzoneIdentifier))
			{
				var seat = FirstSeatInZone(dropItem.DropzoneIdentifier);
				seat.SeatIdentifier = dropItem.Item!.SeatIdentifier;
			}

			if (dropItem.Item!.NumberOfSeats > 1)
			{
				if (!IsLeftSeat(dropItem.DropzoneIdentifier))
				{
					if (!SeatAtThisOrNextZone(dropItem.DropzoneIdentifier))
					{
						dropItem.Item.SeatIdentifier = MoveOver(dropItem.DropzoneIdentifier);
					}
				}
			}
		}
		
		dropItem.Item!.SeatIdentifier = dropItem.DropzoneIdentifier;
	}
	
	public bool CanDrop(Attendee draggedItem, string zoneName)
	{
		if (zoneName == "") return true;

		var zoneHasAttendees = ZoneHasAttendees(zoneName);
		
		if (zoneHasAttendees)
		{
			var attendee = FirstSeatInZone(zoneName);
			return attendee.NumberOfSeats == 1 && draggedItem.NumberOfSeats == 1;
		}

		switch (draggedItem.NumberOfSeats)
		{
			case > 1 when !IsLeftSeat(zoneName):
			case > 1 when SeatAtThisOrNextZone(zoneName):
				return false;
			default: return true;
		}
	}
	
	private bool ZoneHasAttendees(string zone)
	{
		return Attendees.Any(x => x.SeatIdentifier == zone);
	}
	
	private Attendee FirstSeatInZone(string zone)
	{
		return Attendees.First(x => x.SeatIdentifier == zone);
	}
	
	private static bool IsLeftSeat(string zone)
	{
		return int.Parse(zone.Split("-")[1]) % 2 == 0;
	}
	
	private bool SeatAtThisOrNextZone(string zone)
	{
		var items = zone.Split("-");
		var nextZone = $"{items[0]}-{int.Parse(items[1]) + -1}";
		return Attendees.Any(x => x.SeatIdentifier == nextZone);
	}

	private static string MoveOver(string zoneName)
	{
		var items = zoneName.Split("-");
		return $"{items[0]}-{int.Parse(items[1]) +1}";
	}
}