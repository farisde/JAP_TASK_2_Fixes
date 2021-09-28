using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieBuff.Core.Services.MediaService;
using MovieBuff.Data;
using MovieBuff.Entities;
using MovieBuff.Infrastructure.Services.MediaService;
using MovieBuff.Models;
using MovieBuff.Queries;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBuff.Test
{
    [TestFixture]
    class MovieSearchTests
    {
        private DataContext _context;
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

            #region InitializeMovieService
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            _mapper = mapperConfig.CreateMapper();

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
                MediaType = MediaType.Movie,
                Rating = 3,
                Cast = new List<CastMember>
                {
                    new CastMember { Id = 1, Name = "Carrie Fisher" },
                    new CastMember { Id = 2, Name = "Mark Hamil" }
                }
            };

            var m2 = new Media
            {
                Id = 2,
                Title = "Star Wars: The Empire Strikes Back (Episode V)",
                Description = "Darth Vader is adamant about turning Luke Skywalker to the dark side. Master Yoda trains Luke to become a Jedi Knight while his friends try to fend off the Imperial fleet.",
                ReleaseDate = new DateTime(1980, 5, 21),
                CoverImage = "https://images.penguinrandomhouse.com/cover/9780345320223",
                Rating = 2,
                MediaType = MediaType.Movie
            };

            var m3 = new Media
            {
                Id = 3,
                Title = "Riverdale",
                Description = "Archie, Betty, Jughead and Veronica tackle being teenagers in a town that is rife with sinister happenings and blood-thirsty criminals.",
                ReleaseDate = new DateTime(2017, 1, 26),
                CoverImage = "https://static.wikia.nocookie.net/riverdalearchie/images/3/3a/Season_2_Poster.jpg",
                Rating = 4.5,
                MediaType = MediaType.TvShow
            };

            var m4 = new Media
            {
                Id = 4,
                Title = "The Blacklist",
                Description = "A wanted fugitive mysteriously surrenders himself to the FBI and offers to help them capture deadly criminals. His sole condition is that he will work only with the new profiler, Elizabeth Keen.",
                ReleaseDate = new DateTime(2013, 9, 23),
                CoverImage = "https://static.wikia.nocookie.net/blacklist/images/5/57/Season_7_Poster.jpg",
                Rating = 5,
                MediaType = MediaType.TvShow
            };


            var ratings = new List<Rating>
            {
                new Rating { Id = 1, Value = 5, RatedMediaId = m1.Id },
                new Rating { Id = 2, Value = 4, RatedMediaId = m1.Id },
                new Rating { Id = 3, Value = 3, RatedMediaId = m1.Id },
                new Rating { Id = 4, Value = 2, RatedMediaId = m1.Id },
                new Rating { Id = 5, Value = 1, RatedMediaId = m1.Id }
            };

            var r2 = new Rating { Id = 8, Value = 2, RatedMediaId = m2.Id };
            var r3 = new Rating { Id = 6, Value = 4.5, RatedMediaId = m3.Id };
            var r4 = new Rating { Id = 7, Value = 5, RatedMediaId = m4.Id };

            m1.RatingList = ratings;
            m2.RatingList = new List<Rating> { r2 };
            m3.RatingList = new List<Rating> { r3 };
            m4.RatingList = new List<Rating> { r4 };
            _context.Medias.Add(m1);
            _context.Medias.Add(m2);
            _context.Medias.Add(m3);
            _context.Medias.Add(m4);
            await _context.SaveChangesAsync();
            #endregion
        }

        [TearDown]
        public async Task TearDown()
        {
            await _context.Database.EnsureDeletedAsync();
        }

        [Test]
        public async Task MovieShowSearch_InputSearchQuery_5StarContent()
        {
            var query = new PaginationQuery
            {
                SearchPhrase = "5 stars"
            };

            var response = await _mediaService.GetMedia(query);

            Assert.Multiple(() =>
            {
                Assert.That(response.Success, Is.True);
                Assert.That(response.Data.Count, Is.EqualTo(1));
                Assert.That(response.Data.FirstOrDefault().Rating, Is.EqualTo(5));
            });
        }

        [Test]
        public async Task MovieShowSearch_InputSearchQuery_AtLeast3StarContent()
        {
            var query = new PaginationQuery
            {
                SearchPhrase = "at least 3 stars"
            };

            var response = await _mediaService.GetMedia(query);

            Assert.Multiple(() =>
            {
                Assert.That(response.Success, Is.True);
                Assert.That(response.Data.Count, Is.EqualTo(3));
                Assert.That(response.Data.TrueForAll(m => m.Rating >= 3), Is.True);
            });
        }

        [Test]
        public async Task MovieShowSearch_InputSearchQuery_After2015Content()
        {
            var query = new PaginationQuery
            {
                SearchPhrase = "after 2015"
            };

            var response = await _mediaService.GetMedia(query);

            Assert.Multiple(() =>
            {
                Assert.That(response.Success, Is.True);
                Assert.That(response.Data.Count, Is.EqualTo(1));
                Assert.That(response.Data.TrueForAll(m => m.ReleaseDate.Year > 2015), Is.True);
            });
        }

        [Test]
        public async Task MovieShowSearch_InputSearchQuery_OlderThan5YearsContent()
        {
            var query = new PaginationQuery
            {
                SearchPhrase = "older than 5 years"
            };

            var response = await _mediaService.GetMedia(query);

            Assert.Multiple(() =>
            {
                Assert.That(response.Success, Is.True);
                Assert.That(response.Data.Count, Is.EqualTo(3));
                Assert.That(response.Data.TrueForAll(m => DateTime.Now.Year - m.ReleaseDate.Year > 5), Is.True);
            });
        }

        [Test]
        public async Task MovieShowSearch_InputSearchQuery_ContentWithDescriptionMatch()
        {
            var query = new PaginationQuery
            {
                SearchPhrase = "Betty"
            };

            var response = await _mediaService.GetMedia(query);

            Assert.Multiple(() =>
            {
                Assert.That(response.Success, Is.True);
                Assert.That(response.Data.Count, Is.EqualTo(1));
                Assert.That(response.Data.TrueForAll(m => m.Description.Contains(query.SearchPhrase)), Is.True);
            });
        }

        [Test]
        public async Task MovieShowSearch_InputSearchQuery_ContentWithTitleMatch()
        {
            var query = new PaginationQuery
            {
                SearchPhrase = "Star"
            };

            var response = await _mediaService.GetMedia(query);

            Assert.Multiple(() =>
            {
                Assert.That(response.Success, Is.True);
                Assert.That(response.Data.Count, Is.EqualTo(2));
            });
        }

        [Test]
        public async Task MovieShowSearch_InputSearchQuery_ContentWithCastMemberMatch()
        {
            var query = new PaginationQuery
            {
                SearchPhrase = "Hamil"
            };

            var response = await _mediaService.GetMedia(query);

            Assert.Multiple(() =>
            {
                Assert.That(response.Success, Is.True);
                Assert.That(response.Data.Count, Is.EqualTo(1));
            });
        }
    }
}
