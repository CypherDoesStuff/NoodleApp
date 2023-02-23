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
        private Dictionary<ulong, Board> boards;
        private Dictionary<ulong, string> boardPaths;
        ulong selectedBoard;

        //File junk
        public string filePath;

        public const string mainBoardName = "\\main";
        public const ulong mainBoardIndex = 1;
        private const string boardFolder = "\\boards";
        private const string fileExt = ".nood";

        public Database()
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + boardFolder);
            filePath = Directory.GetCurrentDirectory() + boardFolder + "\\";

            boards = new Dictionary<ulong, Board>();
            boardPaths = new Dictionary<ulong, string>();

            string[] paths = Directory.GetFiles(filePath);

            if(paths.Length == 0)
            {
                //Create new board
                Board main = new Board(1, "Main");
                boards.Add(main.index, main);
            }
            else 
            {
                //Load boards
                foreach (string path in paths)
                {
                    Board? board = SerializationUtils.DeserializeBoardFromFile(path);
                    if (board is not null)
                    {
                        boards.Add(board.index, board);
                        boardPaths.Add(board.index, path);
                    }
                }
            }

            SelectBoard(mainBoardIndex);

            SaveAll();
        }

        public void SaveBoard(ulong index)
        {
            Board board = boards[index];
                            SerializationUtils.SerializeBoardToFile(board, filePath + "board" + board.index + fileExt);
        }

        public void SaveAll()
        {
            foreach (Board board in boards.Values)
            {
                Debug.WriteLine("Saving " + board.name);
                SerializationUtils.SerializeBoardToFile(board, filePath + "board" + board.index + fileExt);
            }
        }

        public ulong AddBoard(string name)
        {
            ulong index = (ulong)boards.Count + 2;
            Board board = new Board(index, name);
            board.parent = selectedBoard;

            //Note 0 is an unset index and should be considered null
            if (boards.TryAdd(board.index, board))
            {
                //Add dummy file to path
                string path = filePath + "board" + index + fileExt;
                SerializationUtils.SerializeBoardToFile(board, filePath + "board" + board.index + fileExt);
                boardPaths.Add(index, path);

                return index;
            }
            else
                return 0;
        }

        public bool RenameBoard(ulong index, string name)
        {
            if (boards.ContainsKey(index))
            {
                boards[index].name = name;
                return true;
            }

            return false;
        }

        public bool DeleteBoard(ulong index) 
        {
            if (boardPaths.ContainsKey(index))
            {
                File.Delete(boardPaths[index]);
                boardPaths.Remove(index);
            }

            return boards.Remove(index);
        }

        public void SelectBoard(ulong index)
        {
            selectedBoard = index; 
        }

        public ulong GetSelectedBoard()
        {
            return selectedBoard;
        }

        public string GetBoardName(ulong index)
        {
            return boards[index].name;
        }

        public ulong[] GetSelectedPath()
        {
            List<ulong> parents = new List<ulong>();

            ulong currentParent = boards[selectedBoard].parent;

            while(currentParent != 0)
            {
                parents.Add(currentParent);
                currentParent = boards[currentParent].parent;
            }

            parents.Reverse();
            return parents.ToArray();
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
