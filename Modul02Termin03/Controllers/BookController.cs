﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Modul02Termin03.Models;
using Modul02Termin03.Repository;
using Modul02Termin03.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace Modul02Termin03.Controllers
{
    public class BookController : Controller
    {
        //public static List<Book> knjige = new List<Book>();
        //public static List<Book> obrisana= new List<Book>();

        public BookRepository BookRepository;
        public GenreRepository GenreRepository;

        public BookController(IConfiguration Configuration)
        {
            this.BookRepository = new BookRepository(Configuration);
            this.GenreRepository = new GenreRepository(Configuration);
        }


        public IActionResult Index()
        {
            GenreViewModel gvm=new GenreViewModel();
            gvm.GenresList = new List<SelectListItem>();

            List<Genre> genres = GenreRepository.GetAll();
            foreach (Genre g in genres)
            {
                gvm.GenresList.Add(new SelectListItem() { Text = g.GenreName, Value = g.GenreName });
            }
            return View(gvm);
            
        }

        public IActionResult ShowAllBooks()
        {

            return View(BookRepository.GetAll());
        }


        public IActionResult ShowAllDeleted()
        {

            return View(BookRepository.GetAllDeleted());
        }

        public IActionResult BookSort(int sort)
        {
            List<Book> sortedBy = new List<Book>();

            var knjige = BookRepository.GetAll();
            int sortId = 0;

            switch (sort)
            {
                case 1:
                    sortedBy = knjige.OrderBy(b => b.BookName).ToList();
                    sortId = sort;
                    break;
                case 2:
                    sortedBy = knjige.OrderByDescending(b => b.BookName).ToList();
                    sortId = sort;
                    break;
                case 3:
                    sortedBy = knjige.OrderBy(b => b.Price).ToList();
                    sortId = sort;
                    break;
                case 4:
                    sortedBy = knjige.OrderByDescending(b => b.Price).ToList();
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


            return View("ShowAllBooks", sortedBy);

        }

        public IActionResult Add(string name, double price, int genre) 
        {
            Book k = new Book();
            var knjige = BookRepository.GetAll();
            Genre genreNew = this.GenreRepository.GetOne(genre);
            //int temp = MaxId();
            // k.Id = ++temp;
            k.BookName = name;
            k.Price = price;
            k.Genre = genreNew;

            Book BookExists = knjige.Find(x => x.BookName.Equals(name));
            if (BookExists != null) 
            {
                return View("ErrorBookExists");
            }
            else
            {
                this.BookRepository.Create(k);


            }
            return View("ShowAllBooks",this.BookRepository.GetAll());
        }

        public IActionResult ErrorBookExists()
        {
            return View("ErrorBookExists");
        }

        public IActionResult Delete(int Id)
        {
            BookRepository.Delete(Id);


            return View("ShowAllBooks", BookRepository.GetAll());
        }

        public IActionResult Restore(int Id)
        {
            BookRepository.Restore(Id);


            return View("ShowAllDeleted", BookRepository.GetAllDeleted());
        }

        [HttpGet]
        public IActionResult Modify(int Id)
        {
            Book book = BookRepository.GetOne(Id);

            BookGenreViewModel bgvm = new BookGenreViewModel();
            bgvm.Book = book;
            bgvm.Genres = GenreRepository.GetAll();
            
            return View(bgvm);

        }

        [HttpPost]
        public IActionResult Modify(Book Book, int oldId)
        {
            BookRepository.Update(Book, oldId);

            return View("ShowAllBooks", BookRepository.GetAll());
        }
        /*
        public static int MaxId()
        {
            
            int maxId = 0; 

            foreach (Book k in knjige)
            {
                if (k.Id > maxId)
                {
                    maxId =k.Id;
                }
            }
            return maxId;
        }



        public IActionResult ShowAllDeleted()
        {
            ViewBag.obrisana = obrisana;
            return View("ShowAllDeleted",obrisana);
        }

        
        */
    }
}
