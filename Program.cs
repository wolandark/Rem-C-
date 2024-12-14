using Spectre.Console;

class Program
{
  static void Main(string[] args)
  {
    AnsiConsole.Write(
        new FigletText("REM Sleep Calculator")
        .Centered()
        .Color(Color.Yellow));

    DateTime sleepTime = DateTime.Now;
    int cycleDuration = 90; // Duration of one sleep cycle in minutes
    int cyclesNum = 7; // Number of cycles to calculate

    AnsiConsole.Live(new Table())
      .Start(ctx =>
          {
          while (true)
          {
          var table = new Table()
          .Centered()
          .Border(TableBorder.Rounded)
          .AddColumn("Cycle")
          .AddColumn("Wake-Up Time")
          .AddColumn("Total Sleep");

          for (int i = 1; i <= cyclesNum; i++)
          {
          int totalMinutes = i * cycleDuration;
          DateTime wakeUpTime = sleepTime.AddMinutes(totalMinutes);
          int sleepHours = totalMinutes / 60;
          int sleepMinutes = totalMinutes % 60;

          table.AddRow(
              $"[blue]{i}[/]",
              $"[yellow]{wakeUpTime:HH:mm}[/]",
              $"[cyan]{sleepHours} hours and {sleepMinutes} minutes[/]"
              );
          }

          var panel = new Panel(table)
            .Header("[green]Optimal wake-up times[/]")
            .Border(BoxBorder.Rounded)
            .Expand();

          var currentTimeText = new Markup($"Current sleep time: [green]{sleepTime:HH:mm}[/]");
          var instructionsText = new Markup("[yellow]Press '+' to increment minutes, '-' to decrement minutes, 'Up' to increment hours, 'Down' to decrement hours, or 'q' to quit.[/]");

          ctx.UpdateTarget(new Rows(panel, currentTimeText, instructionsText));

          var key = Console.ReadKey(intercept: true).Key;
          if (key == ConsoleKey.OemPlus || key == ConsoleKey.Add)
          {
            sleepTime = sleepTime.AddMinutes(1);
          }
          else if (key == ConsoleKey.OemMinus || key == ConsoleKey.Subtract)
          {
            sleepTime = sleepTime.AddMinutes(-1);
          }
          else if (key == ConsoleKey.UpArrow)
          {
            sleepTime = sleepTime.AddHours(1);
          }
          else if (key == ConsoleKey.DownArrow)
          {
            sleepTime = sleepTime.AddHours(-1);
          }
          else if (key == ConsoleKey.Q)
          {
            break;
          }

          Thread.Sleep(100); // Small delay to avoid rapid updates
          }
          });

    AnsiConsole.MarkupLine("[yellow]Thank you for using the REM Sleep Calculator![/]");
  }
}

//
// vim: sw=2 ts=2
//
