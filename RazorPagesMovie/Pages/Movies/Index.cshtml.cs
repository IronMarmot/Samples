using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovie.Data.RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }
        [BindProperty(SupportsGet =true)]
        public string SearchString { get; set; }
        public SelectList Genres { get; set; }
        [BindProperty(SupportsGet =true)]
        public string MovieGenre { get; set; }
        public async Task OnGetAsync()
        {
            IQueryable<string> genres = from m in _context.Movie
                         orderby m.Genre
                         select m.Genre;

            var movies = from m in _context.Movie
                         select m;
            if (!String.IsNullOrEmpty(MovieGenre))
            {
                movies = movies.Where(o => o.Genre == MovieGenre);
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(o => o.Title.Contains(SearchString));
            }
            Genres = new SelectList(await genres.Distinct().ToListAsync());
            Movie = await movies.ToListAsync();
        }
    }
}
