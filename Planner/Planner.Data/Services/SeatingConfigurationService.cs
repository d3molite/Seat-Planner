using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Planner.Data.Interfaces;
using Planner.Data.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor;

namespace Planner.Data.Services;

public class SeatingConfigurationService : ISeatingConfigurationService
{
	private readonly ProtectedLocalStorage _storage;

	public event EventHandler? SeatingDataUpdated;

	public void UpdatedExternal(bool regenerateRows = false)
	{
		CollectOrphanedAttendees();

		if (regenerateRows)
		{
			foreach (var row in Rows)
			{
				row.RegenerateSeats();
			}
		}
		
		SeatingDataUpdated?.Invoke(this, EventArgs.Empty);
	}

	public void CollectOrphanedAttendees()
	{
		var orphaned = Attendees.Where(x => !IsPlaced(x));

		foreach (var attendee in orphaned)
		{
			attendee.SeatIdentifier = "";
		}
	}

	private bool IsPlaced(Attendee attendee)
	{
		return Rows.Exists(row => row.Seats.Exists(x => x.SeatIdentifier == attendee.SeatIdentifier));
	}

	public List<Attendee> Attendees { get; set; } = new();

	public List<SeatingRow> Rows { get; set; } = new();

	public SeatingConfigurationService(ProtectedLocalStorage storage)
	{
		_storage = storage;
	}
	
	public async Task Clear()
	{
		Attendees = new();
		Rows = new();
		await _storage.DeleteAsync("rows");
		await _storage.DeleteAsync("attendees");
		await Save();
		SeatingDataUpdated?.Invoke(this, EventArgs.Empty);
	}

	public void RemoveRow(SeatingRow row)
	{
		Rows.Remove(row);
		CollectOrphanedAttendees();
		SeatingDataUpdated?.Invoke(this, EventArgs.Empty);
	}

	public void RemoveAttendee(Attendee attendee)
	{
		Attendees.Remove(attendee);
		SeatingDataUpdated?.Invoke(this, EventArgs.Empty);
	}

	public async Task Save(ISnackbar? snackbar = null)
	{
		await _storage.SetAsync("rows", SerializeRows(Rows));
		await _storage.SetAsync("attendees", SerializeAttendees(Attendees));
		snackbar?.Add("Daten gespeichert.", Severity.Success);
	}

	public async Task Load(ISnackbar snackbar)
	{
		await SetRows(snackbar);
		await SetAttendees(snackbar);
		SeatingDataUpdated?.Invoke(this, EventArgs.Empty);
	}

	public void AddAttendee()
	{
		Attendees.Add(new Attendee(){NickName = "New Player"});
		SeatingDataUpdated?.Invoke(this, EventArgs.Empty);
	}

	public void AddRow()
	{
		Rows.Add(new SeatingRow("A", 1));
		SeatingDataUpdated?.Invoke(this, EventArgs.Empty);
	}

	private async Task SetRows(ISnackbar snackbar)
	{
		var rowResult = await LoadRows();
		
		if (rowResult is {Success: true, Value: not null})
		{
			try
			{
				Rows = DeserializeRows(rowResult.Value);

				foreach (var row in Rows)
				{
					row.RegenerateSeats();
				}
				
				snackbar.Add("Reihen geladen.", Severity.Success);

				return;
			}
			catch (Exception ex)
			{
				// Do nothing
			}
		}
		
		Rows = new List<SeatingRow>();
		snackbar.Add("Fehler beim Laden. Reihen neu initialisiert.", Severity.Error);
	}
	
	private async Task SetAttendees(ISnackbar snackbar)
	{
		var attendeeResult = await LoadAttendees();
		
		if (attendeeResult is {Success: true, Value: not null})
		{
			try
			{
				Attendees = DeserializeAttendees(attendeeResult.Value);
				snackbar.Add("Teilnehmer geladen.", Severity.Success);

				return;
			}
			catch (Exception ex)
			{
				// Do nothing
			}
		}
		
		Attendees = new List<Attendee>();
		snackbar.Add("Fehler beim Laden. Teilnehmer neu initialisiert.", Severity.Error);
	}

	private async Task<ProtectedBrowserStorageResult<string>> LoadRows()
	{
		return await _storage.GetAsync<string>("rows");
	}

	private async Task<ProtectedBrowserStorageResult<string>> LoadAttendees()
	{
		return await _storage.GetAsync<string>("attendees");
	}

	public async Task<string> Export()
	{
		var sb = new StringBuilder();
		
		sb.Append(
			JsonSerializer.Serialize(
				new DataTransfer()
				{
					Rows = Rows,
					Attendees = Attendees
				}
			)
		);

		return sb.ToString();
	}

	public async Task Import(string importData, ISnackbar snackbar)
	{
		try
		{
			var result = JsonSerializer.Deserialize<DataTransfer>(importData);

			if (result is null) return;

			Rows = result.Rows;
			Attendees = result.Attendees;
			
			SeatingDataUpdated?.Invoke(this, EventArgs.Empty);

			snackbar.Add("Daten Importiert.", Severity.Success);
		}
		catch
		{
			snackbar.Add("Daten konnten nicht gelesen werden.", Severity.Error);
		}
	}

	private string SerializeRows(List<SeatingRow> rows)
	{
		return JsonSerializer.Serialize(rows);
	}

	private string SerializeAttendees(List<Attendee> attendees)
	{
		return JsonSerializer.Serialize(attendees);
	}

	private List<SeatingRow> DeserializeRows(string input)
	{
		return JsonSerializer.Deserialize<List<SeatingRow>>(input)!;
	}
	
	private List<Attendee> DeserializeAttendees(string input)
	{
		return JsonSerializer.Deserialize<List<Attendee>>(input)!;
	}
}