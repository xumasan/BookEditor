using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Key = System.UInt64;

namespace BookEditor.Models
{
    public class BookEntry
    {
        public void Clear()
        {
            key = 0;
            sfen = "";
            moves.Clear();
        }

        public Key key;
        public string sfen;
        public List<Move> moves = new List<Move>();
    }

    public class Book
    {
        public void ReadFile(string path)
        {
            var bookFile = new StreamReader(path);

            BookEntry e = new BookEntry();
            bool find = false;

            Position pos = new Position();

            string line;
            
            while ((line = bookFile.ReadLine()) != null)
            {
                if (line.Substring(0, 4) == "sfen")
                {
                    entries.Add(e);
                    e = new BookEntry();

                    var sfen = line.Substring(5);
                    pos.Set(sfen);
                    e.key = pos.CalcHashFull();
                    e.sfen = sfen;
                    find = true;
                }
                else if (find) {
                    var m = line.Split(' ');
                    e.moves.Add(new Move(m[0]));
                }
            }

            // 最後の一個
            entries.Add(e);

            Console.WriteLine("Size : {0}", entries.Count);
        }

        public BookEntry FindEntry(Key key)
        {
            var es = entries.FindAll(e => e.key == key);
            return es.Count == 0 ? null : es[0];
        }

        public BookEntry FindEntry(string sfen)
        {
            var es = entries.FindAll(e => e.sfen == sfen);
            return es.Count == 0 ? null : es[0];
        }

        public List<BookEntry> entries = new List<BookEntry>(); 
    }
}