using Avalonia;
using DynamicData;
using ProductiveApp_Ava.Models;
using ProductiveApp_Ava.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductiveApp_Ava.Services
{
    public class Database
    {
        private Dictionary<string, Board> boards;
        string selectedBoard;

        //File junk
        public const string mainBoardName = "\\main";
        private const string filePath = "\\boards";
        private const string fileExt = ".nood";

        public Database()
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + filePath);

            boards = new Dictionary<string, Board>();

            string[] boardPaths = Directory.GetFiles(Directory.GetCurrentDirectory() + filePath);

            if(boardPaths.Length == 0)
            {
                //Create new board
                Board main = new Board("Main");
                boards.Add(main.name, main);
            }
            else 
            {
                //Load boards
                foreach (string path in boardPaths)
                {
                    Board? board = SerializationUtils.DeserializeBoardFromFile(path);
                    if (board is not null)
                    {
                        boards.Add(board.name, board);
                    }
                }
            }

            SelectBoard("Main");

            SaveAll();
        }

        public void SaveAll()
        {
            string directory = Directory.GetCurrentDirectory();
            foreach (Board board in boards.Values)
            {
                Debug.WriteLine("Saving " + board.name);
                SerializationUtils.SerializeBoardToFile(board, directory + filePath + "\\" + board.name + fileExt);
            }
        }

        public bool CanAddBoard(string name)
        {
            return !boards.ContainsKey(name);
        }

        public bool AddBoard(string name)
        {
            Board board = new Board(name);
            return boards.TryAdd(board.name, board);
        }

        public void SelectBoard(string boardKey)
        {
            selectedBoard = boardKey;
        }

        public void AddNote(Note note) 
        {
            boards[selectedBoard].notes.Add(note);
        }

        public bool RemoveNote(Note note) 
        {
            return boards[selectedBoard].notes.Remove(note);
        }

        public IEnumerable<Note> GetNotes() => boards[selectedBoard].notes;
    }
}
