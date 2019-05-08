using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ColorSudoku_Dnet;
using ColorSudoku_Dnet.ServiceComponents.CommentService;
using ColorSudoku_Dnet.ServiceComponents.RatingService;
using ColorSudoku_Dnet.ServiceComponents.ScoreService;
using ColorSudoku_WEB.APIControllers;
using ColorSudoku_WEB.Models;
using Microsoft.AspNetCore.Mvc.Formatters.Xml.Internal;
using Microsoft.EntityFrameworkCore;

namespace ColorSudoku_WEB.Controllers
{
    public class SudokuController : Controller
    {
        IScoreService _scoreService = new ScoreServiceDatabase();
        ICommentService _commentService = new CommentServiceDatabase();
        IRatingService _ratingService = new RatingServiceDatabase();

        private string loggedUser;
        private Field field;
        SudokuModel model = new SudokuModel();

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            field= new Field();
            HttpContext.Session.SetObject("field",field);

            //var model = new SudokuModel
            //{
            //    Field = field,
            //    Message = "new field created",
            //    Scores = _scoreService.GetTopScores(),
            //    Comments = _commentService.GetLastComments(),
            //    Rating = _ratingService.GetAverageRating()
            //};
            updateModel(model);

            return View(model);
        }

        [Route("sudoku/{value}/{row}/{column}")]
        public IActionResult UpdateTile(int value, int row, int column)
        {
            field = (Field) HttpContext.Session.GetObject("field");
            field.UpdateTile(value,row,column);
            HttpContext.Session.SetObject("field",field);

           
           updateModel(model);


            return View("Index", model);
        }



        //[Route("sudoku/login")]
        [HttpPost("/sudoku/login")]
        public IActionResult Login(string login)
        {
            
            loggedUser = login;
      
            field = (Field)HttpContext.Session.GetObject("field");
            HttpContext.Session.SetObject("field", field);

            updateModel(model);
            
            return View("Index",model);
        }

        [Route("/sudoku/logout")]
        public IActionResult logout()
        {
            var field = (Field)HttpContext.Session.GetObject("field");
            HttpContext.Session.SetObject("field", field);
            loggedUser = null;
            updateModel(model);
           
            return View("Index", model);
        }

        [HttpPost("/sudoku/addComment")]
        public IActionResult addComment([FromForm] string comment)
        {
            _commentService.AddComment(new Comment
            {
                Name = model.LoggedUser,
                Message = comment,
                TimeOFComment = DateTime.Now
            });
            return View("Index", model);
        }

        [HttpPost("/sudoku/addRating")]
        public IActionResult addRating([FromForm] int rating)
        {
            _ratingService.AddRating(new Rating
            {
                Name = model.LoggedUser,
                Mark = rating,
                TimeOfRating = DateTime.Now
            });
            return View("Index", model);
        }


        private void updateModel(SudokuModel model)
        {
            model.LoggedUser = loggedUser;
            model.Field = field;
            model.Scores = _scoreService.GetTopScores();
            model.Comments = _commentService.GetLastComments();
            model.Rating = _ratingService.GetAverageRating();

        }
    }
}