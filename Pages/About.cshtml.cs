﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace audit.Pages
{
    public class AboutModel : PageModel
    {
        public string Message { get; set; }
        public string Description{get;set;}
        public List<ProgrammingLanguage> ProgrammingLanguages{get;set;}

        public void OnGet()
        {
            Message = "Audit";
            Description ="Provides a tool for logging and tracking objects";
            ProgrammingLanguages = new List<ProgrammingLanguage>();
            ProgrammingLanguages.Add(
                new ProgrammingLanguage(){
                    Name = "curl",
                    CodeExample ="curl -X POST \"http://localhost:55777/api/Audit\" -H \"accept: application/json\" -H \"Content-Type: application/json-patch+json\" -d \"{\"id\": \"00000000-0000-0000-0000-000000000000\", \"body\": { }}\""
                }
            );
            ProgrammingLanguages.Add(
                new ProgrammingLanguage(){
                    Name = "JavaScript",
                    CodeExample =@"
                    qwe
                    qwe
                    qwe
                    "
                }
            );
            ProgrammingLanguages.Add(
                new ProgrammingLanguage(){
                    Name = "C#",
                    CodeExample =@"
                    qw
                    qwewe
                    qwewe
                    "
                }
            );
        }
    }
    public class ProgrammingLanguage
    {
        public string Name{get;set;}
        public string CodeExample{get;set;}
    }
}
