using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieBuff.Core.Services.MediaService;
using MovieBuff.Data;
using MovieBuff.DTOs.Movie;
using MovieBuff.Entities;
using MovieBuff.Infrastructure.Services.MediaService;
using MovieBuff.Models;
using MovieBuff.Queries;
using MovieBuff.Services.RatingService;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBuff.Test
{
    [TestFixture]
    class AverageRatingTest
    {
        private DataContext _context;
        private IRatingService _ratingService;
        private IMediaService _mediaService;
        private IMapper _mapper;

        [SetUp]
        public async Task Setup()
        {
            #region InitializeDbContext
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "movie_temp").Options;

            _context = new DataContext(options);
            #endregion

            #region InitializeMediaService
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            _mapper = mapperConfig.CreateMapper();

            _ratingService = new RatingService(_context, _mapper);
            _mediaService = new MediaService(_context, _mapper);
            #endregion

            #region AddDataToDb
            var m1 = new Media
            {
                Id = 1,
                Title = "Star Wars: A New Hope (Episode IV)",
                Description = "After Princess Leia, the leader of the Rebel Alliance, is held hostage by Darth Vader, Luke and Han Solo must free her and destroy the powerful weapon created by the Galactic Empire.",
                ReleaseDate = new DateTime(1997, 5, 25),
                CoverImage = "https://kbimages1-a.akamaihd.net/538b1473-6d45-47f4-b16e-32a0a6ba7f9a/1200/1200/False/star-wars-episode-iv-a-new-hope-3.jpg",
                Rating = 3,
                MediaType = MediaType.Movie
            };
            var ratings = new List<Rating>
            {
                new Rating { Id = 1, Value = 5, RatedMediaId = m1.Id },
                new Rating { Id = 2, Value = 4, RatedMediaId = m1.Id },
                new Rating { Id = 3, Value = 3, RatedMediaId = m1.Id },
                new Rating { Id = 4, Value = 2, RatedMediaId = m1.Id },
                new Rating { Id = 5, Value = 1, RatedMediaId = m1.Id },
                new Rating { Id = 6, Value = 5, RatedMediaId = m1.Id },
                new Rating { Id = 7, Value = 4, RatedMediaId = m1.Id },
                new Rating { Id = 8, Value = 3, RatedMediaId = m1.Id },
                new Rating { Id = 9, Value = 2, RatedMediaId = m1.Id },
                new Rating { Id = 10, Value = 1, RatedMediaId = m1.Id }
            };

            var m2 = new Media
            {
                Id = 2,
                Title = "Star Wars: The Empire Strikes Back (Episode V)",
                Description = "Darth Vader is adamant about turning Luke Skywalker to the dark side. Master Yoda trains Luke to become a Jedi Knight while his friends try to fend off the Imperial fleet.",
                ReleaseDate = new DateTime(1980, 5, 21),
                CoverImage = "https://images.penguinrandomhouse.com/cover/9780345320223",
                MediaType = MediaType.Movie
            };

            m1.RatingList = ratings;
            _context.Medias.Add(m1);
            _context.Medias.Add(m2);
            await _context.SaveChangesAsync();
            #endregion
        }

        [TearDown]
        public async Task TearDown()
        {
            await _context.Database.EnsureDeletedAsync();
        }

        [Test]
        public async Task AverageRating_InputRatingInfo_CorrectRatingCalculation()
        {
            var newRating = new AddRatingDto
            {
                RatedMediaId = 1,
                Value = 5
            };

            var response = await _ratingService.AddMediaRating(newRating);
            var query = new PaginationQuery 
            { 
                MediaType = MediaType.Movie
            };
            var movieResponse = await _mediaService.GetMedia(query);
            var updatedMovie = movieResponse.Data.FirstOrDefault(m => m.Id == newRating.RatedMediaId);

            Assert.Multiple(() =>
            {
                Assert.That(response.Success, Is.True);
                Assert.That(updatedMovie.Rating, Is.EqualTo(3.1818).Within(0.0001));
                Assert.That(updatedMovie.RatingList.Count, Is.EqualTo(11));
            });
        }

        [Test]
        public async Task AverageRating_InputRatingInfo_CorrectRatingWithOneValue()
        {
            var newRating = new AddRatingDto
            {
                RatedMediaId = 2,
                Value = 4
            };

            var response = await _ratingService.AddMediaRating(newRating);
            var updatedMovie = response.Data;

            Assert.Multiple(() =>
            {
                Assert.That(response.Success, Is.True);
                Assert.That(updatedMovie.Value, Is.EqualTo(4));
            });
        }

        [Test]
        public async Task AverageRating_InputRatingInfo_InvalidRatingRange()
        {
            var newRating = new AddRatingDto
            {
                RatedMediaId = 1,
                Value = 10
            };

            var response = await _ratingService.AddMediaRating(newRating);

            Assert.Multiple(() =>
            {
                Assert.That(response.Success, Is.False);
                Assert.That(response.Message, Is.EqualTo("Invalid rating range. Must be between 1 and 5."));
                Assert.That(response.Data, Is.Null);
            });
        }
    }
}
