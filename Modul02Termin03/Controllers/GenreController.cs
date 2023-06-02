using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Modul02Termin03.Models;
using Modul02Termin03.Repository;
using Modul02Termin03.TagHelpers;
using Modul02Termin03.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
namespace Modul02Termin03.Controllers
{
    public class GenreController : Controller
    {

        public BookRepository BookRepository;
        public GenreRepository GenreRepository;

        public GenreController(IConfiguration Configuration)
        {
            this.BookRepository = new BookRepository(Configuration);
            this.GenreRepository = new GenreRepository(Configuration);
        }
        public IActionResult Index()
        {
           GenreViewModel gvm = new GenreViewModel();
            gvm.Genre = new Genre();

            return View(gvm);
        }


        public IActionResult Add(Genre Genre)
        {
            /*
            Genre g = new Genre();
            var genres = GenreRepository.GetAll();
            //Genre genreNew = this.GenreRepository.GetOne(genreN);
            //int temp = MaxId();
            // k.Id = ++temp;
            g.GenreName = name;


            Genre GenreExists = genres.Find(x => x.GenreName.Equals(name));
            if (GenreExists != null)
            {
                return View("ErrorGenreExists");
            }
            else
            {
                this.GenreRepository.Create(g);


            }
            return View("ShowAllGenres", this.GenreRepository.GetAll());
            */


            if (!ModelState.IsValid)
            {

                GenreViewModel gvm = new GenreViewModel();
                gvm.Genre = Genre;

                return View("Index", gvm);
            }
            this.GenreRepository.Create(Genre);
            return View("ShowAllGenres", this.GenreRepository.GetAll());
        }
        /*
        public static int MaxId()
        {

            int maxId = 0;

            foreach (Genre g in genres)
            {
                if (g.Id > maxId)
                {
                    maxId = g.Id;
                }
            }
            return maxId;
        }
        */

        public IActionResult GenreSort(int sort)
        {
            List<Genre> sortedBy = new List<Genre>();
            int sortId = 0;
            var genres = GenreRepository.GetAll();
            switch (sort)
            {
                case 1:
                    sortedBy = genres.OrderBy(b => b.GenreName).ToList();
                    sortId = sort;
                    break;
                case 2:
                    sortedBy = genres.OrderByDescending(b => b.GenreName).ToList();
                    sortId = sort;
                    break;
                default:
                    break;

            }
            if (sortId != 0)
            {
                ViewBag.sortId = sortId;
            }
            else
            {
                ViewBag.sortId = 1;
            }


            return View("ShowAllGenres", sortedBy);

        }

        public IActionResult ShowAllGenres()
        {

            return View("ShowAllGenres", GenreRepository.GetAll());
        }

        public IActionResult ShowAllDeleted()
        {

            return View(GenreRepository.GetAllDeleted());
        }

        public IActionResult Delete(int Id)
        {
           GenreRepository.Delete(Id);


            return View("ShowAllGenres", GenreRepository.GetAll());
        }

        public IActionResult Restore(int Id)
        {
            GenreRepository.Restore(Id);


            return View("ShowAllDeleted", GenreRepository.GetAllDeleted());
        }


        [HttpGet]
        public IActionResult ModifyGenre(int Id)
        {
            Genre genre = GenreRepository.GetOne(Id);

            return View(genre);

        }

        [HttpPost]
        public IActionResult ModifyGenre(Genre Genre, int oldId)
        {
            if (!ModelState.IsValid)
            {
                GenreViewModel gvm = new GenreViewModel();
                gvm.Genre = Genre;

                return View("Modify", gvm);
            }
            GenreRepository.Update(Genre, oldId);

            return View("ShowAllGenres", GenreRepository.GetAll());
        }


        /*
        [HttpGet]
        public IActionResult Izmeni(int Id)
        {

            Genre izmenjen = genres.Find(x => x.Id.Equals(Id));


            return View("Izmeni", izmenjen);

        }

        [HttpPost]
        public IActionResult Izmeni(int Id, string genreN)
        {


            foreach (Genre g in genres)
            {
                if (g.Id.Equals(Id))
                {
                    g.GenreName = genreN;
                }

            }

            return View("ShowAllGenres", genres);


        }
        */
    }

}

