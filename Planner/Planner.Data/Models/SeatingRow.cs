using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Data.Models;

public class SeatingRow
{
    
    public SeatingRow(string letter, int startingSeat)
    {
        Letter = letter;
        StartingSeat = startingSeat;
        RegenerateSeats();
    }

    public int StartingSeat { get; set; }

    [NotMapped]
    private int MaxSeat => StartingSeat + 39;

    public string Letter { get; set; }

    [NotMapped]
    public List<Seat> Seats { get; set; } = new();

    private void RegenerateSeats()
    {
        for (int i = StartingSeat; i <= MaxSeat; i++)
        {
            Seats.Add(new Seat(i, Letter){SeatNumber = i, RowLetter = Letter});
        }
    }
}

public class Seat
{
    public Seat(int seatNumber, string rowLetter)
    {
        SeatNumber = seatNumber;
        RowLetter = rowLetter;
    }
    
    public int SeatNumber { get; init; }
    
    public string RowLetter { get; init; }

    public string SeatIdentifier => $"{RowLetter}-{SeatNumber}";
}