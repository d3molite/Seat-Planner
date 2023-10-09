using System.ComponentModel.DataAnnotations.Schema;
using Planner.Db.Models;

namespace Planner.WebUI.LocalModels;

public class SeatingRow
{
    public SeatingRow(string letter)
    {
        Letter = letter;
        for (int i = 1; i <= 40; i++)
        {
            Seats.Add(new SeatingSeat(){SeatNumber = i, RowLetter = Letter});
        }
    }
    
    public string Letter { get; set; }

    [NotMapped]
    public List<SeatingSeat> Seats { get; set; } = new();
}

public class SeatingSeat
{
    public int SeatNumber { get; set; }
    
    public string RowLetter { get; set; }

    public string SeatIdentifier => $"{RowLetter}-{SeatNumber}";
}