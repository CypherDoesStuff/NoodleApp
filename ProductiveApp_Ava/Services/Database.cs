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
        public const string mainBoardName = "\\main";
        private const string filePath = "\\boards";
        private const string fileExt = ".nood";

        private List<Board> boards;
        public Dictionary<string, Board> boardDict;

        public Database()
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + filePath);

            boards = new List<Board>();


            Board? main = SerializationUtils.DeserializeFromFile(Directory.GetCurrentDirectory() + filePath + mainBoardName + fileExt);
            if(main is not null)
                boards.Add(main);
            else
                boards.Add(new Board("Main"));

            //boards[0].notes.Add(new Image_Note { x = 500, y = 200, url = "https://media.macphun.com/img/uploads/customer/how-to/579/15531840725c93b5489d84e9.43781620.jpg?q=85&w=1340" });
            //boards[0].notes.Add(new Text_Note { x = 200, y = 200, text = "Hello World!" });

            boardDict = new Dictionary<string, Board>();
            SaveAll();
        }

        public void SaveAll()
        {
            string directory = Directory.GetCurrentDirectory();
            foreach (Board board in boards)
            {
                Debug.WriteLine("Saving " + board.name);
                SerializationUtils.SerializeToFile(board, directory + filePath + "\\" + board.name + fileExt);
            }
        }

        public IEnumerable<Note> GetNotes() => boards[0].notes;
    }
}
