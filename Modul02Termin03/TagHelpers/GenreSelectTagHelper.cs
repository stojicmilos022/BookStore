using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using Modul02Termin03.Controllers;
using Modul02Termin03.Models;
using Modul02Termin03.Repository;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Modul02Termin03.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project

    public class GenreSelectTagHelper : TagHelper
    {
        public BookRepository BookRepository;
        public GenreRepository GenreRepository;

        public GenreSelectTagHelper(IConfiguration Configuration)
        {
            this.BookRepository = new BookRepository(Configuration);
            this.GenreRepository = new GenreRepository(Configuration);
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            
            output.TagName = "select";
            output.Attributes.SetAttribute("name", "genre");
            string opcije = "";
            List<Genre> genres = GenreRepository.GetAll();
            foreach (Genre g in genres)
            {
                opcije += $@"<option value='{g.Id}' >{g.GenreName}</option>";
            }
            output.Content.SetHtmlContent(opcije);
            
        }
    }
}
