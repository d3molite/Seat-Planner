using System.Text.Json;
using System.Text.Json.Serialization;
using Planner.Data.Interfaces;
using Planner.Data.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Planner.Data.Services;

public class SeatingConfigurationService : ISeatingConfigurationService
{
	private readonly ProtectedLocalStorage _storage;

	public SeatingConfigurationService(ProtectedLocalStorage storage)
	{
		_storage = storage;
	}
	
	public async Task Clear()
	{
		await _storage.DeleteAsync("rows");
		await _storage.DeleteAsync("attendees");
	}

	public async Task Save(List<SeatingRow> rows, List<Attendee> attendees)
	{
		await _storage.SetAsync("rows", SerializeRows(rows));
		await _storage.SetAsync("attendees", SerializeAttendees(attendees));
	}

	public async Task<ProtectedBrowserStorageResult<string>> LoadRows()
	{
		return await _storage.GetAsync<string>("rows");
	}

	public async Task<ProtectedBrowserStorageResult<string>> LoadAttendees()
	{
		return await _storage.GetAsync<string>("attendees");
	}

	public async Task Export()
	{
		throw new NotImplementedException();
	}

	public async Task Import()
	{
		throw new NotImplementedException();
	}

	private string SerializeRows(List<SeatingRow> rows)
	{
		return JsonSerializer.Serialize(rows);
	}

	private string SerializeAttendees(List<Attendee> attendees)
	{
		return JsonSerializer.Serialize(attendees);
	}

	public List<SeatingRow> DeserializeRows(string input)
	{
		return JsonSerializer.Deserialize<List<SeatingRow>>(input)!;
	}
	
	public List<Attendee> DeserializeAttendees(string input)
	{
		return JsonSerializer.Deserialize<List<Attendee>>(input)!;
	}
}