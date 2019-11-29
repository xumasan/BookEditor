using System;
using System.Collections.Generic;

namespace BookEditor.Models
{
    public class BookEntry
    {
        public void Clear()
        {
            sfen = "";
            moves.Clear();
        }

        public string sfen;
        public List<Move> moves = new List<Move>();
    }

    public class Book
    {
        public void ReadFile(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);

            BookEntry e = new BookEntry();
            bool find = false;
            
            foreach (string line in lines)
            {
                if (line.Substring(0, 4) == "sfen")
                {
                    entries.Add(e);
                    e.Clear();
                    e.sfen = line.Substring(5);
                    find = true;
                }
                else if (find) {
                    var m = line.Split(' ');
                    // とりあえず０
                    e.moves.Add(new Move(0, m[0]));
                }
            }

            // 最後の一個
            entries.Add(e);
        }

        public List<BookEntry> entries = new List<BookEntry>(); 
    }
}