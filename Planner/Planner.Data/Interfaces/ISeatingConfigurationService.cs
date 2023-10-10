using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Planner.Data.Models;

namespace Planner.Data.Interfaces;

public interface ISeatingConfigurationService
{
	public Task Save(List<SeatingRow> rows, List<Attendee> attendees);

	public Task<ProtectedBrowserStorageResult<string>> LoadRows();

	public Task<ProtectedBrowserStorageResult<string>> LoadAttendees();

	public Task Export();

	public Task Import();
	
	public List<SeatingRow> DeserializeRows(string input);
	
	public List<Attendee> DeserializeAttendees(string input);

	public Task Clear();
}