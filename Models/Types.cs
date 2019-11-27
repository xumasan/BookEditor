using System;
using System.ComponentModel;

namespace BookEditor.Models
{
    public enum Color
    {
        BLACK, WHITE, COLOR_NB = 2
    };

    public enum PieceType
    {
        NO_PIECE_TYPE , 
        PAWN          ,
        LANCE         ,
        KNIGHT        ,
        SILVER        ,
        BISHOP        ,
        ROOK          ,
        GOLD          ,
        KING          ,
        PRO_PAWN      ,
        PRO_LANCE     ,
        PRO_KNIGHT    ,
        PRO_SILVER    ,
        HORSE         ,
        DRAGON        ,
        PIECE_TYPE_NB ,
        HAND_NB    = 8,
        ALL_PIECES = 0,
    }

    public enum Piece
    {
        NO_PIECE         ,
        B_PAWN           ,
        B_LANCE          ,
        B_KNIGHT         ,
        B_SILVER         ,
        B_BISHOP         ,
        B_ROOK           ,
        B_GOLD           ,
        B_KING           ,
        B_PRO_PAWN       ,
        B_PRO_LANCE      ,
        B_PRO_KNIGHT     ,
        B_PRO_SILVER     ,
        B_HORSE          ,
        B_DRAGON         ,
        W_PAWN       = 17,
        W_LANCE          ,
        W_KNIGHT         ,
        W_SILVER         ,
        W_BISHOP         ,
        W_ROOK           ,
        W_GOLD           ,
        W_KING           ,
        W_PRO_PAWN       ,
        W_PRO_LANCE      ,
        W_PRO_KNIGHT     ,
        W_PRO_SILVER     ,
        W_HORSE          ,
        W_DRAGON         ,
        PIECE_NB     = 32,
        PROMOTE_FLAG  = 8,
    }

    public static class Pieces
    {
        public static Color ColorOf(Piece p)
        {
            return (Color)((int)p >> 4);
        }

        public static PieceType TypeOf(Piece p)
        {
            return (PieceType)((int)p & 0x0f);
        }
    }

    public enum Square
    {
        SQ_9A, SQ_8A, SQ_7A, SQ_6A, SQ_5A, SQ_4A, SQ_3A, SQ_2A, SQ_1A,
        SQ_9B, SQ_8B, SQ_7B, SQ_6B, SQ_5B, SQ_4B, SQ_3B, SQ_2B, SQ_1B,
        SQ_9C, SQ_8C, SQ_7C, SQ_6C, SQ_5C, SQ_4C, SQ_3C, SQ_2C, SQ_1C,
        // x < SQ_9Dの範囲が後手陣。先手にとって駒がなれるのはこの範囲。
        SQ_9D, SQ_8D, SQ_7D, SQ_6D, SQ_5D, SQ_4D, SQ_3D, SQ_2D, SQ_1D,
        SQ_9E, SQ_8E, SQ_7E, SQ_6E, SQ_5E, SQ_4E, SQ_3E, SQ_2E, SQ_1E,
        SQ_9F, SQ_8F, SQ_7F, SQ_6F, SQ_5F, SQ_4F, SQ_3F, SQ_2F, SQ_1F,
        // x > SQ_1Fの範囲が先手陣。後手にとって駒がなれるのはこの範囲。
        SQ_9G, SQ_8G, SQ_7G, SQ_6G, SQ_5G, SQ_4G, SQ_3G, SQ_2G, SQ_1G,
        SQ_9H, SQ_8H, SQ_7H, SQ_6H, SQ_5H, SQ_4H, SQ_3H, SQ_2H, SQ_1H,
        SQ_9I, SQ_8I, SQ_7I, SQ_6I, SQ_5I, SQ_4I, SQ_3I, SQ_2I, SQ_1I,
        SQ_NONE,
        SQUARE_NB = 81,
    }

    public enum File
    {
        FILE_1, FILE_2, FILE_3, FILE_4, FILE_5, FILE_6, FILE_7, FILE_8, FILE_9, FILE_NB
    }

    public static class Files
    {
        public readonly static File[] FileIndex =  {
            File.FILE_1, File.FILE_2, File.FILE_3, File.FILE_4, File.FILE_5, File.FILE_6, File.FILE_7, File.FILE_8, File.FILE_9,
            File.FILE_1, File.FILE_2, File.FILE_3, File.FILE_4, File.FILE_5, File.FILE_6, File.FILE_7, File.FILE_8, File.FILE_9,
            File.FILE_1, File.FILE_2, File.FILE_3, File.FILE_4, File.FILE_5, File.FILE_6, File.FILE_7, File.FILE_8, File.FILE_9,
            File.FILE_1, File.FILE_2, File.FILE_3, File.FILE_4, File.FILE_5, File.FILE_6, File.FILE_7, File.FILE_8, File.FILE_9,
            File.FILE_1, File.FILE_2, File.FILE_3, File.FILE_4, File.FILE_5, File.FILE_6, File.FILE_7, File.FILE_8, File.FILE_9,
            File.FILE_1, File.FILE_2, File.FILE_3, File.FILE_4, File.FILE_5, File.FILE_6, File.FILE_7, File.FILE_8, File.FILE_9,
            File.FILE_1, File.FILE_2, File.FILE_3, File.FILE_4, File.FILE_5, File.FILE_6, File.FILE_7, File.FILE_8, File.FILE_9,
            File.FILE_1, File.FILE_2, File.FILE_3, File.FILE_4, File.FILE_5, File.FILE_6, File.FILE_7, File.FILE_8, File.FILE_9,
            File.FILE_1, File.FILE_2, File.FILE_3, File.FILE_4, File.FILE_5, File.FILE_6, File.FILE_7, File.FILE_8, File.FILE_9
        };
    }
    
    public enum Rank
    {
        RANK_1, RANK_2, RANK_3, RANK_4, RANK_5, RANK_6, RANK_7, RANK_8, RANK_9, RANK_NB
    }

    public static class Ranks
    {
        public readonly static Rank[] RankIndex = {
            Rank.RANK_1, Rank.RANK_1, Rank.RANK_1, Rank.RANK_1, Rank.RANK_1, Rank.RANK_1, Rank.RANK_1, Rank.RANK_1, Rank.RANK_1,
            Rank.RANK_2, Rank.RANK_2, Rank.RANK_2, Rank.RANK_2, Rank.RANK_2, Rank.RANK_2, Rank.RANK_2, Rank.RANK_2, Rank.RANK_2,
            Rank.RANK_3, Rank.RANK_3, Rank.RANK_3, Rank.RANK_3, Rank.RANK_3, Rank.RANK_3, Rank.RANK_3, Rank.RANK_3, Rank.RANK_3,
            Rank.RANK_4, Rank.RANK_4, Rank.RANK_4, Rank.RANK_4, Rank.RANK_4, Rank.RANK_4, Rank.RANK_4, Rank.RANK_4, Rank.RANK_4,
            Rank.RANK_5, Rank.RANK_5, Rank.RANK_5, Rank.RANK_5, Rank.RANK_5, Rank.RANK_5, Rank.RANK_5, Rank.RANK_5, Rank.RANK_5,
            Rank.RANK_6, Rank.RANK_6, Rank.RANK_6, Rank.RANK_6, Rank.RANK_6, Rank.RANK_6, Rank.RANK_6, Rank.RANK_6, Rank.RANK_6,
            Rank.RANK_7, Rank.RANK_7, Rank.RANK_7, Rank.RANK_7, Rank.RANK_7, Rank.RANK_7, Rank.RANK_7, Rank.RANK_7, Rank.RANK_7,
            Rank.RANK_8, Rank.RANK_8, Rank.RANK_8, Rank.RANK_8, Rank.RANK_8, Rank.RANK_8, Rank.RANK_8, Rank.RANK_8, Rank.RANK_8,
            Rank.RANK_9, Rank.RANK_9, Rank.RANK_9, Rank.RANK_9, Rank.RANK_9, Rank.RANK_9, Rank.RANK_9, Rank.RANK_9, Rank.RANK_9
        };
    }

    public class Move
    {
        public Move(int m, string s)
        {
            move = m;
            sfen = s;
        }
        public int move { get; }
        public string sfen { get; }
    }
}
