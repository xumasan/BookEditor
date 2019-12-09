using System;

using Key = System.UInt64;

namespace BookEditor.Models
{
    public class RKISS
    {
        UInt64 a, b, c, d, e;

        // Init seed and scramble a few rounds
        internal RKISS()
        {
            a = 0xf1ea5eed;
            b = c = d = 0xd4e12c77;
            for (int i = 0; i < 73; i++)
            {
                rand();
            }
        }
        internal UInt64 rand()
        {
            e = a - ((b << 7) | (b >> 57));
            a = b ^ ((c << 13) | (c >> 51));
            b = c + ((d << 37) | (d >> 27));
            c = d + e;
            return d = e + a;
        }
    }

    public static class Zobrist {
        public static void Init()
        {
            RKISS rk = new RKISS();

            for (var pc = Piece.B_PAWN; pc < Piece.PIECE_NB; ++pc)
            {
                for (var sq = Square.SQ_9A; sq <= Square.SQ_1I; ++sq)
                {
                    psq[(int)pc, (int)sq] = rk.rand() << 1;
                }
            }

            for (var c = Color.BLACK; c < Color.COLOR_NB; ++c)
            {
                for (var pt = PieceType.PAWN; pt < PieceType.HAND_NB; ++pt)
                {
                    hand[(int)c, (int)pt] = rk.rand() << 1;
                }
            }

            side = (Key)1;
        }
        public static Key[,] psq = new Key[(int)Piece.PIECE_NB, (int)Square.SQUARE_NB];
        public static Key[,] hand = new Key[(int)Color.COLOR_NB, (int)PieceType.HAND_NB];
        public static Key side;
    }

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

            //PrintBoard();
        }

        public Key CalcHashFull()
        {
            Key key = 0;
            for (var sq = Square.SQ_9A; sq <= Square.SQ_1I; ++sq)
            {
                var pc = PieceOn(sq);
                if (pc == Piece.NO_PIECE) continue;
                key += Zobrist.psq[(int)pc, (int)sq];
            }

             for (var c = Models.Color.BLACK; c < Models.Color.COLOR_NB; ++c)
                for (var pt = PieceType.PAWN; pt < PieceType.HAND_NB; ++pt)
                {
                    key += Zobrist.hand[(int)c, (int)pt] * (Key)HandNum(c, pt);
                }

            if (sideToMove == Color.WHITE)
                key ^= Zobrist.side;

            return key;
        }

        public void DoMove(Move m)
        {
            var us = sideToMove;
            var op = Colors.Inverse(us);
            var from = m.From();
            var to = m.To();

            if (from >= Square.SQUARE_NB)
            {
                PieceType pt = (PieceType)((int)from - 80);
                Piece p = Pieces.Make(us, pt);

                hand[(int)us, (int)pt]--;
                board[(int)to] = p;
            }
            else 
            {
                Piece cap = board[(int)to];

                if (cap != Piece.NO_PIECE)
                {
                    var capType = (Piece)((int)Pieces.TypeOf(cap) & 0x07);
                    hand[(int)us, (int)capType]++;
                }

                board[(int)to] = board[(int)from];
                board[(int)from] = Piece.NO_PIECE;

                if (m.IsPromote())
                    board[(int)to] = (Piece)((int)board[(int)to] + (int)Piece.PROMOTE_FLAG);
            }

            gamePly++;

            sideToMove = op;
            m.ply = gamePly;
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
            Console.WriteLine("Hash : {0}", CalcHashFull().ToString("x4"));
        }

        const string PieceToChar = " PLNSBRGKxxxxxxx plnsbrgk";
        private Piece[] board = new Piece[(int)Square.SQUARE_NB];
        private int[,] hand = new int[(int)Color.COLOR_NB, (int)PieceType.HAND_NB];
        private Color sideToMove;
        private int gamePly;
    }
}