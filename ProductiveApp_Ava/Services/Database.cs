using Avalonia;
using ProductiveApp_Ava.Models;
using ProductiveApp_Ava.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductiveApp_Ava.Services
{
    public class Database
    {
        public IEnumerable<Note> GetNotes() => new[]
        {
            new Note {x = 300, y = 100 },
            new Image_Note {x = 500, y = 200 },
            new Text_Note {x = 200, y = 200, text = "Hello World!" },
        };
    }
}
