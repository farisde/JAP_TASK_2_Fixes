using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using MovieBuff.Core.Services.TicketService;
using MovieBuff.Data;
using MovieBuff.DTOs.Ticket;
using MovieBuff.Entities;
using MovieBuff.Services.TicketService;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieBuff.Test
{
    [TestFixture]
    class TicketTests
    {
        private ITicketService _ticketService;
        private DataContext _context;
        private IAuthRepository _authRepository;
        private IMapper _mapper;

        [SetUp]
        public async Task Setup()
        {
            #region InitializeDbContext
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "movie_temp").Options;

            _context = new DataContext(options);
            #endregion

            #region AddScreeningsToDb
            _context.Screenings.Add(new Screening
            {
                Id = 1,
                Name = "Screening 1",
                StartTime = DateTime.Now.AddDays(20).AddHours(1),
                Duration = 120,
                AvailableSeats = 30,
                ReservedSeats = 20,
                MediaId = 1
            });

            _context.Screenings.Add(new Screening
            {
                Id = 2,
                Name = "Screening 2",
                StartTime = DateTime.Now.AddDays(-5).AddHours(1),
                Duration = 120,
                AvailableSeats = 30,
                ReservedSeats = 29,
                MediaId = 1
            });

            _context.Screenings.Add(new Screening
            {
                Id = 3,
                Name = "Screening 3",
                StartTime = DateTime.Now.AddDays(22).AddHours(1),
                Duration = 120,
                AvailableSeats = 30,
                ReservedSeats = 30,
                MediaId = 1
            });

            await _context.SaveChangesAsync();
            #endregion

            #region InitializeAuthRepository
            var inMemoryConfig = new Dictionary<string, string>
            {
                { "AppSettings:Token", "my top secrete testing key" }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemoryConfig)
                .Build();

            _authRepository = new AuthRepository(_context, configuration);
            #endregion

            #region AddUserToDb
            AuthRepository.CreatePasswordHash("password", out byte[] passwordHash, out byte[] passwordSalt);
            var u1 = new User
            {
                Id = 1,
                Name = "Administrator",
                Email = "admin@admin.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _context.SaveChangesAsync();
            #endregion

            #region InitializeTicketService
            var loggedInUser = await _authRepository.Login("admin@admin.com", "password");

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "admin@admin.com")
            };

            var defaultHttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(claims))
            };

            defaultHttpContext.Request.Headers["Authorization"] = "Bearer " + loggedInUser.Data;
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(defaultHttpContext);

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            _mapper = mapperConfig.CreateMapper();

            _ticketService = new TicketService(_context, mockHttpContextAccessor.Object, _mapper);
            #endregion
        }

        [TearDown]
        public async Task TearDown()
        {
            await _context.Database.EnsureDeletedAsync();
        }

        [Test]
        public async Task BuyTicketsForScreenings_InputTicketInfo_ValidPurchase()
        {
            var screening = await _context.Screenings
                .Include(s => s.Tickets)
                .FirstOrDefaultAsync(s => s.Id == 1);
            var buyTicket = new BuyTicketDto
            {
                ScreeningID = screening.Id,
                TicketAmount = 3,
                TicketPrice = 8
            };

            var response = await _ticketService.BuyTicketsForScreening(buyTicket);

            Assert.Multiple(() =>
            {
                Assert.That(response.Success, Is.True);
                //screening is a reference to a db object and it will get updated to the
                //newest value after the service method call
                Assert.That(response.Data.ReservedSeats, Is.EqualTo(screening.ReservedSeats));
                Assert.That(response.Data.Tickets.Count, Is.EqualTo(3));
            });
        }

        [Test]
        public async Task BuyTicketsForScreenings_InputTicketInfo_InvalidScreeningTime()
        {
            var screening = await _context.Screenings
                .Include(s => s.Tickets)
                .FirstOrDefaultAsync(s => s.Id == 2);

            var buyTicket = new BuyTicketDto
            {
                ScreeningID = screening.Id,
                TicketAmount = 3,
                TicketPrice = 8
            };

            var response = await _ticketService.BuyTicketsForScreening(buyTicket);

            Assert.Multiple(() =>
            {
                Assert.That(response.Success, Is.False);
                Assert.That(response.Message, Is.EqualTo("The selected screening is not available anymore."));
                Assert.That(response.Data, Is.Null);
            });
        }

        [Test]
        public async Task BuyTicketsForScreenings_InputTicketInfo_InvalidScreeningID()
        {
            var screeningID = -99;

            var buyTicket = new BuyTicketDto
            {
                ScreeningID = screeningID,
                TicketAmount = 3,
                TicketPrice = 8
            };

            var response = await _ticketService.BuyTicketsForScreening(buyTicket);

            Assert.Multiple(() =>
            {
                Assert.That(response.Success, Is.False);
                Assert.That(response.Message, Is.EqualTo("Selected screening doesn't exist."));
                Assert.That(response.Data, Is.Null);
            });
        }

        [Test]
        public async Task BuyTicketsForScreenings_InputTicketInfo_NotEnoughSeats()
        {
            var screening = await _context.Screenings
                .Include(s => s.Tickets)
                .FirstOrDefaultAsync(s => s.Id == 3);

            var buyTicket = new BuyTicketDto
            {
                ScreeningID = screening.Id,
                TicketAmount = 3,
                TicketPrice = 8
            };

            var response = await _ticketService.BuyTicketsForScreening(buyTicket);

            Assert.Multiple(() =>
            {
                Assert.That(response.Success, Is.False);
                Assert.That(response.Message, Is.EqualTo("There are not enough available seats for the requested number of tickets."));
                Assert.That(response.Data, Is.Null);
            });
        }

        [Test]
        public async Task BuyTicketsForScreenings_InputTicketInfo_NegativeAmount()
        {
            var screening = await _context.Screenings
                .Include(s => s.Tickets)
                .FirstOrDefaultAsync(s => s.Id == 1);

            var buyTicket = new BuyTicketDto
            {
                ScreeningID = screening.Id,
                TicketAmount = -3,
                TicketPrice = 8
            };

            var response = await _ticketService.BuyTicketsForScreening(buyTicket);

            Assert.Multiple(() =>
            {
                Assert.That(response.Success, Is.False);
                Assert.That(response.Message, Is.EqualTo("Ticket amount needs to be greater than zero."));
                Assert.That(response.Data, Is.Null);
            });
        }
    }
}
