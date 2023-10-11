using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor;
using Planner.Data.Models;

namespace Planner.Data.Interfaces;

public interface ISeatingConfigurationService
{
	public event EventHandler? SeatingDataUpdated;

	public void UpdatedExternal(bool regenerateRows = false);

	public void CollectOrphanedAttendees();
	
	public List<Attendee> Attendees { get; set; }

	public List<SeatingRow> Rows { get; set; }
	
	public Task Save(ISnackbar? snackbar);

	public Task Load(ISnackbar snackbar);

	public void AddAttendee();

	public void AddRow();

	public Task Export();

	public Task Import();

	public Task Clear();

	void RemoveRow(SeatingRow row);
	
	void RemoveAttendee(Attendee attendee);
}