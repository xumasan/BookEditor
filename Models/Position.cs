using System;

namespace BookEditor.Models
{
    public class Position
    {
        public void Clear()
        {
            Array.Clear(hand, 0, hand.Length);
            Array.Clear(board, 0, board.Length);
        }

        public void Set(string sfenStr)
        {
            Clear();

            char token;
            int p, num;
            Square sq = Square.SQ_9A;

            char[] sfen = sfenStr.ToCharArray();
            int sfenPos = 0;
            bool promote = false;

            // 1. Piece placement
            while ((token = sfen[sfenPos++]) != ' ')
            {
                if (token == '+')
                {
                    promote = true;
                    continue;
                }
                if (Int32.TryParse(token.ToString(), out num))
                    sq += num; // Advance the given number of files
                else if (token == '/') {}
                else
                {
                    p = PieceToChar.IndexOf(token);
                    if (p > -1)
                    {
                        if (promote)
                            p += (int)Piece.PROMOTE_FLAG;
                        PutPiece((Piece)p, sq);
                        sq++;
                        promote = false;
                    }
                }
            }

            // 2. Active color
            token = sfen[sfenPos++];
            sideToMove = (token == 'w' ? Color.WHITE : Color.BLACK);
            token = sfen[sfenPos++];

            int pieceNum = 1;

            while ((token = sfen[sfenPos++]) != ' ')
            {
                if (token == '-')
                    continue;

                if (Int32.TryParse(token.ToString(), out num))
                {
                    if (num == 1)
                    {
                        sfenPos++;
                        pieceNum = 10 + Int32.Parse(sfen[sfenPos].ToString());
                    }
                    else {
                        pieceNum = num;
                    }
                }
                else if ((p = PieceToChar.IndexOf(token)) > -1)
                {
                    Piece pc = (Piece)p;
                    PieceType pt = Pieces.TypeOf(pc);
                    Color c = Pieces.ColorOf(pc);

                    for (int i = 0; i < pieceNum; i++)
                    {
                        AddHand(c, pt);
                    }
                    pieceNum = 1;
                }
            }

            token = sfen[sfenPos++];
            gamePly = Int32.Parse(token.ToString());

            PrintBoard();
        }

        public void PutPiece(Piece pc, Square sq)
        {
            board[(int)sq] = pc;
        }

        public Piece PieceOn(Square sq)
        {
            return board[(int)sq];
        }

        public int HandNum(Color c, PieceType pt)
        {
            return hand[(int)c, (int)pt];
        }

        public void AddHand(Color c, PieceType pt)
        {
            hand[(int)c, (int)pt]++;
        }

        public void PrintBoard()
        {
            for (var sq = Square.SQ_9A; sq <= Square.SQ_1I; ++sq)
            {
                Console.Write("[{0}]", PieceToChar[(int)PieceOn(sq)]);
                if (Files.FileIndex[(int)sq] == File.FILE_9)
                    Console.WriteLine("");
            }

            for (var c = Color.BLACK; c < Color.COLOR_NB; ++c)
            {
                Console.Write("{0} Hand : ", c);
                for (var pt = PieceType.PAWN; pt < PieceType.HAND_NB; ++pt)
                {
                    int num = HandNum(c, pt);
                    if (num > 0)
                    {
                        Console.Write("{0}-{1}", PieceToChar[(int)pt], num);
                    }
                }
                Console.WriteLine("");
            }

            Console.WriteLine("GamePly : {0}", gamePly);
            Console.WriteLine("Turn : {0}", sideToMove);
        }

        const string PieceToChar = " PLNSBRGKxxxxxxx plnsbrgk";
        private Piece[] board = new Piece[(int)Square.SQUARE_NB];
        private int[,] hand = new int[(int)Color.COLOR_NB, (int)PieceType.HAND_NB];
        private Color sideToMove;
        private int gamePly;
    }
}