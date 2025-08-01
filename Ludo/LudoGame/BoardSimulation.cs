public class SimulationBoard {
    public static void DisplayBoard() {
        int[,] board = new int[15, 15];
        // Console.ForegroundColor = ConsoleColor.White;

        for (int i = 0; i < board.GetLength(0); i++) {
            for (int j = 0; j < board.GetLength(1); j++) {
                Console.Write($" ({j},{i}) ");
            }
            Console.WriteLine();
        }

        //     Console.ResetColor();

        //     for (int i = 0; i < board.GetLength(0); i++) {
        //         for (int j = 0; j < board.GetLength(1); j++) {

        //             Console.ResetColor();

        //             if ((i == 6 && j == 6) || (i == 6 && j == 8) || (i == 8 && j == 8) || (i == 8 && j == 6) || (i == 7 && j == 7)) {
        //                 Console.Write(" * ");
        //                 continue;
        //             }

        //             // === SAFE ZONE (Eksplisit) ===
        //             if (
        //                 (i == 8 && j == 2) ||
        //                 (i == 2 && j == 6) ||
        //                 (i == 6 && j == 12) ||
        //                 (i == 12 && j == 8)
        //             ) {
        //                 Console.ForegroundColor = ConsoleColor.White;
        //                 Console.Write(" ★ ");
        //                 continue;
        //             }

        //             // === BASE ZONES ===
        //             if (i >= 0 && i <= 5 && j >= 0 && j <= 5) {
        //                 Console.ForegroundColor = ConsoleColor.Red; // Base Merah
        //             } else if (i >= 0 && i <= 5 && j >= 9 && j <= 14) {
        //                 Console.ForegroundColor = ConsoleColor.Green; // Base Hijau
        //             } else if (i >= 9 && i <= 14 && j >= 0 && j <= 5) {
        //                 Console.ForegroundColor = ConsoleColor.Blue; // Base Biru
        //             } else if (i >= 9 && i <= 14 && j >= 9 && j <= 14) {
        //                 Console.ForegroundColor = ConsoleColor.Yellow; // Base Kuning
        //             }

        //               // === HOME POINTS ===
        //               else if (i == 7 && j == 6) {
        //                 Console.ForegroundColor = ConsoleColor.Red; // Home Merah
        //             } else if (i == 6 && j == 7) {
        //                 Console.ForegroundColor = ConsoleColor.Green; // Home Hijau
        //             } else if (i == 7 && j == 8) {
        //                 Console.ForegroundColor = ConsoleColor.Yellow; // Home Kuning
        //             } else if (i == 8 && j == 7) {
        //                 Console.ForegroundColor = ConsoleColor.Blue; // Home Biru
        //             }

        //               // === START POINTS ===
        //               else if (i == 6 && j == 1) {
        //                 Console.ForegroundColor = ConsoleColor.Red; // Start Merah
        //             } else if (i == 1 && j == 8) {
        //                 Console.ForegroundColor = ConsoleColor.Green; // Start Hijau
        //             } else if (i == 8 && j == 13) {
        //                 Console.ForegroundColor = ConsoleColor.Yellow; // Start Kuning
        //             } else if (i == 13 && j == 6) {
        //                 Console.ForegroundColor = ConsoleColor.Blue; // Start Biru
        //             }

        //               // === HOME PATHS ===
        //               else if (i == 7 && j >= 1 && j <= 5) {
        //                 Console.ForegroundColor = ConsoleColor.Red; // Path Merah
        //             } else if (j == 7 && i >= 1 && i <= 5) {
        //                 Console.ForegroundColor = ConsoleColor.Green; // Path Hijau
        //             } else if (i == 7 && j >= 9 && j <= 13) {
        //                 Console.ForegroundColor = ConsoleColor.Yellow; // Path Kuning
        //             } else if (j == 7 && i >= 9 && i <= 13) {
        //                 Console.ForegroundColor = ConsoleColor.Blue; // Path Biru
        //             } else {
        //                 Console.ForegroundColor = ConsoleColor.White;
        //             }

        //             Console.Write(" ■ ");
        //             // Console.Write($" ({i},{j}) ");
        //         }
        //         Console.WriteLine();
        //     }

        // }
    }
}


// public static class ExBoard {
//     public static Piece[,] pc = new Piece[15, 15];
//     public static int[,] intArr = new int[15, 15];
// }

//  (0,0)   (0,1)   (0,2)   (0,3)   (0,4)   (0,5)   (0,6)   (0,7)   (0,8)   (0,9)   (0,10)   (0,11)   (0,12)   (0,13)   (0,14)
//  (1,0)   (1,1)   (1,2)   (1,3)   (1,4)   (1,5)   (1,6)   (1,7)   (1,8)   (1,9)   (1,10)   (1,11)   (1,12)   (1,13)   (1,14)
//  (2,0)   (2,1)   (2,2)   (2,3)   (2,4)   (2,5)   (2,6)   (2,7)   (2,8)   (2,9)   (2,10)   (2,11)   (2,12)   (2,13)   (2,14)
//  (3,0)   (3,1)   (3,2)   (3,3)   (3,4)   (3,5)   (3,6)   (3,7)   (3,8)   (3,9)   (3,10)   (3,11)   (3,12)   (3,13)   (3,14)
//  (4,0)   (4,1)   (4,2)   (4,3)   (4,4)   (4,5)   (4,6)   (4,7)   (4,8)   (4,9)   (4,10)   (4,11)   (4,12)   (4,13)   (4,14)
//  (5,0)   (5,1)   (5,2)   (5,3)   (5,4)   (5,5)   (5,6)   (5,7)   (5,8)   (5,9)   (5,10)   (5,11)   (5,12)   (5,13)   (5,14)
//  (6,0)   (6,1)   (6,2)   (6,3)   (6,4)   (6,5)   (6,6)   (6,7)   (6,8)   (6,9)   (6,10)   (6,11)   (6,12)   (6,13)   (6,14)
//  (7,0)   (7,1)   (7,2)   (7,3)   (7,4)   (7,5)   (7,6)   (7,7)   (7,8)   (7,9)   (7,10)   (7,11)   (7,12)   (7,13)   (7,14)
//  (8,0)   (8,1)   (8,2)   (8,3)   (8,4)   (8,5)   (8,6)   (8,7)   (8,8)   (8,9)   (8,10)   (8,11)   (8,12)   (8,13)   (8,14)
//  (9,0)   (9,1)   (9,2)   (9,3)   (9,4)   (9,5)   (9,6)   (9,7)   (9,8)   (9,9)   (9,10)   (9,11)   (9,12)   (9,13)   (9,14)
//  (10,0)  (10,1)  (10,2)  (10,3)  (10,4)  (10,5)  (10,6)  (10,7)  (10,8)  (10,9)  (10,10)  (10,11)  (10,12)  (10,13)  (10,14)
//  (11,0)  (11,1)  (11,2)  (11,3)  (11,4)  (11,5)  (11,6)  (11,7)  (11,8)  (11,9)  (11,10)  (11,11)  (11,12)  (11,13)  (11,14)
//  (12,0)  (12,1)  (12,2)  (12,3)  (12,4)  (12,5)  (12,6)  (12,7)  (12,8)  (12,9)  (12,10)  (12,11)  (12,12)  (12,13)  (12,14)
//  (13,0)  (13,1)  (13,2)  (13,3)  (13,4)  (13,5)  (13,6)  (13,7)  (13,8)  (13,9)  (13,10)  (13,11)  (13,12)  (13,13)  (13,14)
//  (14,0)  (14,1)  (14,2)  (14,3)  (14,4)  (14,5)  (14,6)  (14,7)  (14,8)  (14,9)  (14,10)  (14,11)  (14,12)  (14,13)  (14,14)