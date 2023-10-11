using System.ComponentModel.DataAnnotations.Schema;
using Planner.Data.Enums;

namespace Planner.Data.Models;

public class SeatingRow
{
    private int _startingSeat;

    public SeatingRow(string letter, int startingSeat)
    {
        Letter = letter;
        StartingSeat = startingSeat;
        RegenerateSeats();
    }

    public int StartingSeat
    {
        get => _startingSeat;
        set
        {
            _startingSeat = value;
            RegenerateSeats();
        }
    }

    [NotMapped]
    private int MaxSeat => StartingSeat + 39;

    public string Letter { get; set; }

    [NotMapped]
    public List<Seat> Seats { get; set; } = new();

    public CarpetPosition CarpetPosition { get; set; } = CarpetPosition.None;

    public void RegenerateSeats()
    {
        Seats = new();
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