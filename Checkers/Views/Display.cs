namespace Checkers.Views;

using Checkers.Interfaces;
using Checkers.Classes;
using Checkers.Enums;

public class Display {

    const string darkSquareBg = "\x1b[48;2;191;146;100m";     // #BF9264
    const string validSquareBg = "\x1b[48;2;149;76;46m";      // #954C2E
    const string validSquareFg = "\x1b[48;2;59;6;10m";        // #3B060A 
    const string lightSquareBg = "\x1b[48;2;248;244;225m";    // #F8F4E1
    const string blackPieceFg = "\x1b[38;2;0;0;0m";
    const string redPieceFg = "\x1b[38;2;255;0;0m";
    const string resetColor = "\x1b[0m";
    const string normalPiece = " ● ";
    const string kingPiece = " ✪ ";

    private readonly GameController _gameController;

    public Display(GameController gameController) {
        _gameController = gameController;
    }

    public void StartGame() {
        while (true) {
            DisplayBoard(_gameController.GetBoard(), null);
            IPlayer currentPlayer = _gameController.GetCurrentPlayer();
            Console.WriteLine($"{currentPlayer.Name} {currentPlayer.Color} Turn");

            if (_gameController.IsGameOver()) {
                break;
            }

            if (_gameController.IsInCaptureChain) {
                Console.WriteLine("Executing capture chain...");
                Thread.Sleep(700);

                IPiece? pieceToContinue = _gameController.ChainingPiece;

                if (pieceToContinue != null) {
                    List<Position> chainMoves = _gameController.GetCaptureChain(pieceToContinue);

                    if (chainMoves.Count > 0) {
                        Position startPos = pieceToContinue.Position;
                        Position endPos = chainMoves[0];

                        _gameController.HandleMove(startPos, endPos);
                    }
                }

                continue;
            }

            var availableMoves = _gameController.GetAllValidMovesForPlayer(currentPlayer);
            if (availableMoves.Count == 0) {
                Console.WriteLine($"No move avaible! {currentPlayer.Color} Lose 🫵🏻😹");
                _gameController.EndGame();
                break;
            }

            bool mustCapture = _gameController.HasForcedCaptures(currentPlayer);
            Dictionary<IPiece, List<Position>> movesToShow;

            if (mustCapture) {
                movesToShow = new Dictionary<IPiece, List<Position>>();

                foreach (var moveEntry in availableMoves) {
                    IPiece piece = moveEntry.Key;
                    List<Position> captureDestinations = new List<Position>();

                    foreach (Position dest in moveEntry.Value) {
                        if (_gameController.IsCapture(piece.Position, dest)) {
                            captureDestinations.Add(dest);
                        }
                    }

                    if (captureDestinations.Count > 0) {
                        movesToShow[piece] = captureDestinations;
                    }
                }
            } else {
                movesToShow = availableMoves;
            }

            IPiece selectedPiece = SelectPiece(movesToShow);
            List<Position> destinations = movesToShow[selectedPiece];

            DisplayBoard(_gameController.GetBoard(), destinations);

            Console.WriteLine($"{currentPlayer.Name} Turn");
            Console.WriteLine($"Selected piece at ({selectedPiece.Position.X}, {selectedPiece.Position.Y}).");
            Position selectedDestination = SelectDestination(destinations);

            _gameController.HandleMove(selectedPiece.Position, selectedDestination);

        }

        _gameController.EndGame();
    }

    public void DisplayBoard(IBoard board, List<Position>? highlightedSquares) {

        Console.Clear();
        Console.Write("   ");
        for (int col = 0; col < board.Size; col++) {
            Console.Write($" {col} ");
        }
        Console.WriteLine("\n");

        for (int y = 0; y < board.Size; y++) {
            Console.Write($" {y} ");
            for (int x = 0; x < board.Size; x++) {
                Position currentPos = new Position(x, y);
                string bgColor;
                bool isHighlighted = highlightedSquares != null && highlightedSquares.Contains(currentPos);

                if (isHighlighted) {
                    bgColor = validSquareBg;
                } else {
                    bgColor = ((x + y) % 2 != 0) ? darkSquareBg : lightSquareBg;
                }

                Console.Write(bgColor);

                if (isHighlighted) {
                    int moveIndex = highlightedSquares!.IndexOf(currentPos) + 1;
                    Console.Write(validSquareFg);
                    Console.Write($" {moveIndex} ");
                } else {
                    IPiece? selectedPiece = board[x, y];
                    if (selectedPiece == null) {
                        Console.Write("   ");
                    } else {
                        if (selectedPiece.Color == PieceColor.BLACK) {
                            Console.Write(blackPieceFg);
                            Console.Write(selectedPiece.PieceType == PieceType.KING ? kingPiece : normalPiece);
                        } else {
                            Console.Write(redPieceFg);
                            Console.Write(selectedPiece.PieceType == PieceType.KING ? kingPiece : normalPiece);
                        }
                    }
                }
                Console.Write(resetColor);
            }
            Console.WriteLine($" {y} ");
        }

        Console.Write("\n   ");
        for (int col = 0; col < board.Size; col++) {
            Console.Write($" {col} ");
        }
        Console.WriteLine("\n");

    }

    public IPiece SelectPiece(Dictionary<IPiece, List<Position>> movesToShow) {
        Console.WriteLine("Pieces to move:");
        int index = 1;
        var movablePieces = movesToShow.Keys.ToList();
        foreach (var piece in movablePieces) {
            Console.WriteLine($"{index++}. Piece in ({piece.Position.X}, {piece.Position.Y})");
        }

        while (true) {
            Console.Write("Select the number of piece: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= movablePieces.Count) {
                return movablePieces[choice - 1];
            }
            Console.WriteLine($"Invalid input. Please enter a number between 1 and {movablePieces.Count}.");
        }
    }

    public Position SelectDestination(List<Position> destinations) {
        while (true) {
            Console.WriteLine("Select a numbered destination on the board: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= destinations.Count) {
                return destinations[choice - 1];
            }
            Console.WriteLine($"Invalid input. Please enter a number between 1 and {destinations.Count}.");
        }
    }

}