using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieBuff.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CastMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CastMembers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoverImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    MediaType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MostSoldMoviesWithoutRatingsResults",
                columns: table => new
                {
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    MovieTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreeningName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketsSold = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Top10MoviesByRatingResults",
                columns: table => new
                {
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    MovieTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RatingsCount = table.Column<int>(type: "int", nullable: false),
                    MovieRating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Top10MoviesByScreeningsForPeriodResults",
                columns: table => new
                {
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    MovieTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreeningsCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CastMemberMedia",
                columns: table => new
                {
                    CastId = table.Column<int>(type: "int", nullable: false),
                    StarredMediaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CastMemberMedia", x => new { x.CastId, x.StarredMediaId });
                    table.ForeignKey(
                        name: "FK_CastMemberMedia_CastMembers_CastId",
                        column: x => x.CastId,
                        principalTable: "CastMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CastMemberMedia_Medias_StarredMediaId",
                        column: x => x.StarredMediaId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Screenings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<float>(type: "real", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false),
                    ReservedSeats = table.Column<int>(type: "int", nullable: false),
                    MediaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screenings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Screenings_Medias_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<double>(type: "float", nullable: false),
                    RatedMediaId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Medias_RatedMediaId",
                        column: x => x.RatedMediaId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ScreeningId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Screenings_ScreeningId",
                        column: x => x.ScreeningId,
                        principalTable: "Screenings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CastMembers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Carrie Fisher" },
                    { 33, "Carl Weathers" },
                    { 34, "Daniel Radcliffe" },
                    { 35, "Emma Watson" },
                    { 36, "Rupert Grint" },
                    { 37, "Chloe Bennet" },
                    { 38, "Elizabeth Henstridge" },
                    { 39, "Ming‑Na Wen" },
                    { 40, "Emma Thompson" },
                    { 41, "Russell Tovey" },
                    { 42, "Ruth Madeley" },
                    { 43, "Lucas Till" },
                    { 44, "Tristin Mays" },
                    { 45, "Meredith Eaton" },
                    { 46, "Ellen Pompeo" },
                    { 47, "Chandra Wilson" },
                    { 48, "James Pickens Jr." },
                    { 49, "Zendaya" },
                    { 50, "Hunter Schafer" },
                    { 51, "Jacob Elordi" },
                    { 52, "Henry Cavil" },
                    { 53, "Anya Chalotra" },
                    { 54, "Freya Allan" },
                    { 55, "Kiernan Shipka" },
                    { 56, "Ross Lynch" },
                    { 58, "Zoe Saldana" },
                    { 59, "Sam Worthington" },
                    { 60, "Sigourney Weaver" },
                    { 32, "Giancarlo Esposito" },
                    { 31, "Gina Carano" },
                    { 57, "Gavin Leatherwood" },
                    { 29, "Sarah Hyland" },
                    { 30, "Pedro Pascal" },
                    { 2, "Mark Hamil" },
                    { 3, "Harrison Ford" },
                    { 5, "Lili Reinhart" },
                    { 6, "Camila Mendes" },
                    { 7, "KJ Apa" },
                    { 8, "James Spader" },
                    { 9, "Megan Boone" },
                    { 10, "Diego Klattenhoff" },
                    { 11, "Henry Lennix" }
                });

            migrationBuilder.InsertData(
                table: "CastMembers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 12, "Daisy Ridley" },
                    { 13, "Adam Driver" },
                    { 14, "Natalie Portman" },
                    { 4, "Cole Sprouse" },
                    { 16, "Liam Neeson" },
                    { 15, "Ewan McGregor" },
                    { 27, "Eric Stonestreet" },
                    { 25, "Miguel Herrán" },
                    { 24, "Itziar Ituño" },
                    { 23, "Álvaro Morte" },
                    { 26, "Sofía Vergara" },
                    { 21, "Paul Ritter" },
                    { 17, "Hayden Christensen" },
                    { 22, "Úrsula Corberó" },
                    { 18, "Jared Harris" },
                    { 28, "Julie Bowen" },
                    { 19, "Stellan Skarsgård" },
                    { 20, "Emily Watson" }
                });

            migrationBuilder.InsertData(
                table: "Medias",
                columns: new[] { "Id", "CoverImage", "Description", "MediaType", "Rating", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 68, "https://lumiere-a.akamaihd.net/v1/images/p_fyc_themandalorian_19097_de619ea9.jpeg", "After the defeat of the Empire at the hands of Rebel forces, a lone bounty hunter operating in the Outer Rim, away from the dominion of the New Republic, goes on many surprising and risky adventures.", 1, 4.2000000000000002, new DateTime(2019, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 68" },
                    { 78, "https://images-na.ssl-images-amazon.com/images/I/81bLWVTR3sL.jpg", "This adaptation of the \"Sabrina the Teenage Witch\" tale is a dark coming-of-age story that traffics in horror and the occult. In the reimagined origin story, Sabrina Spellman wrestles to reconcile her dual nature - half-witch, half-mortal.", 1, 4.0999999999999996, new DateTime(2018, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 78" },
                    { 76, "https://lanetechchampion.org/wp-content/uploads/2020/09/zendya-euphoria.jpg", "An American adaptation of the Israeli show of the same name, \"Euphoria\" follows the troubled life of 17-year-old Rue, a drug addict fresh from rehab with no plans to stay clean.", 1, 3.0, new DateTime(2019, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 76" },
                    { 74, "https://m.media-amazon.com/images/M/MV5BMTg1NzUzNmQtMWI2ZC00YzdlLWI5ZTgtMGM3MjJlZTgwYTVlXkEyXkFqcGdeQXVyMTIzMzY2NDQ2._V1_FMjpg_UX1000_.jpg", "MacGyver, a contemporary hero and role model, applies his scientific knowledge to ordinary items to create a means of escape for himself and others from impending doom.", 1, 3.2999999999999998, new DateTime(2016, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 74" },
                    { 73, "https://i.pinimg.com/474x/4e/96/06/4e9606a7f386cafa7903e28e94e43627.jpg", "An ordinary British family contends with the hopes, anxieties and joys of an uncertain future in this six-part limited series that begins in 2019 and propels the characters 15 years forward into an unstable world.", 1, 2.7000000000000002, new DateTime(2019, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 73" },
                    { 72, "https://static.wikia.nocookie.net/marvelcinematicuniverse/images/1/10/AoS_Season_One_Poster.jpg", "Agent Phil Coulson leads a team of highly skilled agents from the global law-enforcement organisation known as S.H.I.E.L.D. Together, they combat extraordinary and inexplicable threats.", 1, 4.9000000000000004, new DateTime(2013, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 72" },
                    { 71, "https://static.wikia.nocookie.net/warner-bros-entertainment/images/c/cb/Harry-potter-and-the-prisoner-of-azkaban-movie-poster-style-f-11x17.jpg", "Harry, Ron and Hermoine return to Hogwarts just as they learn about Sirius Black and his plans to kill Harry. However, when Harry runs into him, he learns that the truth is far from reality.", 1, 4.0, new DateTime(2004, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 71" },
                    { 70, "https://www.hylandcinema.com/files/hyland/movie-posters/hp-_chamber.jpg", "A house-elf warns Harry against returning to Hogwarts, but he decides to ignore it. When students and creatures at the school begin to get petrified, Harry finds himself surrounded in mystery.", 1, 4.4000000000000004, new DateTime(2002, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 70" },
                    { 69, "https://movieguise.files.wordpress.com/2016/01/potter.jpg", "Harry Potter, an eleven-year-old orphan, discovers that he is a wizard and is invited to study at Hogwarts. Even as he escapes a dreary life and enters a world of magic, he finds trouble awaiting him.", 1, 4.5999999999999996, new DateTime(2001, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 69" },
                    { 77, "https://www.syfy.com/sites/syfy/files/styles/1170xauto/public/the-witcher-season-2-poster-vertical.jpg", "The witcher Geralt, a mutated monster hunter, struggles to find his place in a world in which people often prove more wicked than beasts.", 1, 5.0, new DateTime(2019, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 77" },
                    { 75, "http://www.gstatic.com/tv/thumb/tvbanners/17114349/p17114349_b_v12_aa.jpg", "Surgical interns and their supervisors embark on a medical journey where they become part of heart-wrenching stories and make life-changing decisions in order to become the finest doctors.", 1, 5.0, new DateTime(2005, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 75" },
                    { 62, "https://www.themoviedb.org/t/p/w780/ic0intvXZSfBlYPIvWXpU1ivUCO.jpg", "Ryder and the pups are called to Adventure City to stop Mayor Humdinger from turning the bustling metropolis into a state of chaos.", 1, 3.7999999999999998, new DateTime(2021, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 62" },
                    { 66, "https://www.enigma-mag.com/wp-content/uploads/2019/08/1563490297.jpg", "A criminal mastermind who goes by \"The Professor\" has a plan to pull off the biggest heist in recorded history -- to print billions of euros in the Royal Mint of Spain. To help him carry out the ambitious plan, he recruits eight people with certain abilities and who have nothing to lose.", 1, 4.0, new DateTime(2017, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 66" },
                    { 65, "https://i.pinimg.com/originals/06/b2/2f/06b22f1ec7b9a6914ec6255f40953e98.jpg", "In April 1986, the city of Chernobyl in the Soviet Union suffers one of the worst nuclear disasters in the history of mankind. Consequently, many heroes put their lives on the line to save Europe.", 1, 3.6000000000000001, new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 65" },
                    { 64, "https://i.pinimg.com/originals/17/aa/71/17aa718c1ab15b482505b8431cf596fc.jpg", "Jake, who is paraplegic, replaces his twin on the Na'vi inhabited Pandora for a corporate mission. After the natives accept him as one of their own, he must decide where his loyalties lie.", 1, 4.9000000000000004, new DateTime(2009, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 64" },
                    { 63, "https://play-lh.googleusercontent.com/mMyoXM8bf72KK-Udap4-hAvqqdXgn0AIBXkS8zejT0RXITIh8oK9a-SYIVk89CA0rHJi", "Anakin joins forces with Obi-Wan and sets Palpatine free from the evil clutches of Count Doku. However, he falls prey to Palpatine and the Jedis' mind games and gives into temptation.", 1, 4.2000000000000002, new DateTime(2005, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 63" },
                    { 61, "https://www.themoviedb.org/t/p/w780/clDFqATL4zcE7LzUwkrVj3zHvk7.jpg", "Cinderella, an orphaned girl with an evil stepmother, has big dreams and with the help of her Fabulous Godmother, she perseveres to make them come true.", 1, 3.5, new DateTime(2021, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 61" },
                    { 60, "https://www.themoviedb.org/t/p/w780/9dKCd55IuTT5QRs989m9Qlb7d2B.jpg", "Dr. Lily Houghton enlists the aid of wisecracking skipper Frank Wolff to take her down the Amazon in his dilapidated boat. Together, they search for an ancient tree that holds the power to heal – a discovery that will change the future of medicine.", 1, 4.0, new DateTime(2021, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 60" },
                    { 59, "https://www.themoviedb.org/t/p/w780/6Y9fl8tD1xtyUrOHV2MkCYTpzgi.jpg", "An off-duty SAS soldier, Tom Buckingham, must thwart a terror attack on a train running through the Channel Tunnel. As the action escalates on the train, events transpire in the corridors of power that may make the difference as to whether Buckingham and the civilian passengers make it out of the tunnel alive.", 1, 4.9000000000000004, new DateTime(2021, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 59" },
                    { 58, "https://www.themoviedb.org/t/p/w780/kb4s0ML0iVZlG6wAKbbs9NAm6X.jpg", "Supervillains Harley Quinn, Bloodsport, Peacemaker and a collection of nutty cons at Belle Reve prison join the super-secret, super-shady Task Force X as they are dropped off at the remote, enemy-infused island of Corto Maltese.", 1, 4.5, new DateTime(2021, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 58" },
                    { 57, "https://www.themoviedb.org/t/p/w780/34nDCQZwaEvsy4CFO5hkGRFDCVU.jpg", "The world is stunned when a group of time travelers arrive from the year 2051 to deliver an urgent message: Thirty years in the future, mankind is losing a global war against a deadly alien species.", 1, 4.9000000000000004, new DateTime(2021, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 57" },
                    { 79, "https://www.themoviedb.org/t/p/w780/Czhr00kB8awffakEcQS5ON1ELm.jpg", "Female adventurer Parker joins a crew of male trophy hunters in a remote wilderness park. Their goal: slaughter genetically recreated dinosaurs for sport using rifles, arrows, and grenades.", 1, 5.0, new DateTime(2021, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 79" },
                    { 56, "https://www.themoviedb.org/t/p/w780/34nDCQZwaEvsy4CFO5hkGRFDCVU.jpg", "The world is stunned when a group of time travelers arrive from the year 2051 to deliver an urgent message: Thirty years in the future, mankind is losing a global war against a deadly alien species.", 1, 5.0, new DateTime(2021, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 56" },
                    { 67, "https://upload.wikimedia.org/wikipedia/en/thumb/0/09/Modern_Family_Season_Two_DVD_Cover.png/250px-Modern_Family_Season_Two_DVD_Cover.png", "Three modern-day families from California try to deal with their kids, quirky spouses and jobs in their own unique ways, often falling into hilarious situations.", 1, 4.2999999999999998, new DateTime(2009, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 67" }
                });

            migrationBuilder.InsertData(
                table: "Medias",
                columns: new[] { "Id", "CoverImage", "Description", "MediaType", "Rating", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 80, "https://www.themoviedb.org/t/p/w780/pS1XUGjC6ASC1kvDCP3OJnwjk1t.jpg", "Shang-Chi must confront the past he thought he left behind when he is drawn into the web of the mysterious Ten Rings organization.", 1, 4.9000000000000004, new DateTime(2021, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 80" },
                    { 95, "https://movieguise.files.wordpress.com/2016/01/potter.jpg", "Harry Potter, an eleven-year-old orphan, discovers that he is a wizard and is invited to study at Hogwarts. Even as he escapes a dreary life and enters a world of magic, he finds trouble awaiting him.", 1, 4.5999999999999996, new DateTime(2001, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 95" },
                    { 82, "https://www.themoviedb.org/t/p/w780/34nDCQZwaEvsy4CFO5hkGRFDCVU.jpg", "The world is stunned when a group of time travelers arrive from the year 2051 to deliver an urgent message: Thirty years in the future, mankind is losing a global war against a deadly alien species.", 1, 5.0, new DateTime(2021, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 82" },
                    { 55, "https://www.themoviedb.org/t/p/w780/hRMfgGFRAZIlvwVWy8DYJdLTpvN.jpg", "The Blind Man has been hiding out for several years in an isolated cabin and has taken in and raised a young girl orphaned from a devastating house fire. Their quiet life together is shattered when a group of criminals kidnap the girl.", 1, 4.5, new DateTime(2021, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 55" },
                    { 106, "https://lumiere-a.akamaihd.net/v1/images/p_fyc_themandalorian_19097_de619ea9.jpeg", "After the defeat of the Empire at the hands of Rebel forces, a lone bounty hunter operating in the Outer Rim, away from the dominion of the New Republic, goes on many surprising and risky adventures.", 1, 0.0, new DateTime(2019, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 106" },
                    { 105, "https://www.hylandcinema.com/files/hyland/movie-posters/hp-_chamber.jpg", "A house-elf warns Harry against returning to Hogwarts, but he decides to ignore it. When students and creatures at the school begin to get petrified, Harry finds himself surrounded in mystery.", 1, 4.4000000000000004, new DateTime(2002, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 105" },
                    { 104, "https://static.wikia.nocookie.net/marvelcinematicuniverse/images/1/10/AoS_Season_One_Poster.jpg", "Agent Phil Coulson leads a team of highly skilled agents from the global law-enforcement organisation known as S.H.I.E.L.D. Together, they combat extraordinary and inexplicable threats.", 1, 0.0, new DateTime(2013, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 104" },
                    { 103, "https://m.media-amazon.com/images/M/MV5BMTg1NzUzNmQtMWI2ZC00YzdlLWI5ZTgtMGM3MjJlZTgwYTVlXkEyXkFqcGdeQXVyMTIzMzY2NDQ2._V1_FMjpg_UX1000_.jpg", "MacGyver, a contemporary hero and role model, applies his scientific knowledge to ordinary items to create a means of escape for himself and others from impending doom.", 1, 0.0, new DateTime(2016, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 103" },
                    { 102, "http://www.gstatic.com/tv/thumb/tvbanners/17114349/p17114349_b_v12_aa.jpg", "Surgical interns and their supervisors embark on a medical journey where they become part of heart-wrenching stories and make life-changing decisions in order to become the finest doctors.", 1, 0.0, new DateTime(2005, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 102" },
                    { 101, "http://www.gstatic.com/tv/thumb/tvbanners/17114349/p17114349_b_v12_aa.jpg", "Surgical interns and their supervisors embark on a medical journey where they become part of heart-wrenching stories and make life-changing decisions in order to become the finest doctors.", 1, 5.0, new DateTime(2005, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 101" },
                    { 100, "https://m.media-amazon.com/images/M/MV5BMTg1NzUzNmQtMWI2ZC00YzdlLWI5ZTgtMGM3MjJlZTgwYTVlXkEyXkFqcGdeQXVyMTIzMzY2NDQ2._V1_FMjpg_UX1000_.jpg", "MacGyver, a contemporary hero and role model, applies his scientific knowledge to ordinary items to create a means of escape for himself and others from impending doom.", 1, 3.2999999999999998, new DateTime(2016, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 100" },
                    { 99, "https://i.pinimg.com/474x/4e/96/06/4e9606a7f386cafa7903e28e94e43627.jpg", "An ordinary British family contends with the hopes, anxieties and joys of an uncertain future in this six-part limited series that begins in 2019 and propels the characters 15 years forward into an unstable world.", 1, 2.7000000000000002, new DateTime(2019, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 99" },
                    { 98, "https://static.wikia.nocookie.net/marvelcinematicuniverse/images/1/10/AoS_Season_One_Poster.jpg", "Agent Phil Coulson leads a team of highly skilled agents from the global law-enforcement organisation known as S.H.I.E.L.D. Together, they combat extraordinary and inexplicable threats.", 1, 4.9000000000000004, new DateTime(2013, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 98" },
                    { 97, "https://static.wikia.nocookie.net/warner-bros-entertainment/images/c/cb/Harry-potter-and-the-prisoner-of-azkaban-movie-poster-style-f-11x17.jpg", "Harry, Ron and Hermoine return to Hogwarts just as they learn about Sirius Black and his plans to kill Harry. However, when Harry runs into him, he learns that the truth is far from reality.", 1, 4.0, new DateTime(2004, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 97" },
                    { 81, "https://www.themoviedb.org/t/p/w780/hRMfgGFRAZIlvwVWy8DYJdLTpvN.jpg", "The Blind Man has been hiding out for several years in an isolated cabin and has taken in and raised a young girl orphaned from a devastating house fire. Their quiet life together is shattered when a group of criminals kidnap the girl.", 1, 4.5, new DateTime(2021, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 81" },
                    { 96, "https://www.hylandcinema.com/files/hyland/movie-posters/hp-_chamber.jpg", "A house-elf warns Harry against returning to Hogwarts, but he decides to ignore it. When students and creatures at the school begin to get petrified, Harry finds himself surrounded in mystery.", 1, 4.4000000000000004, new DateTime(2002, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 96" },
                    { 93, "https://upload.wikimedia.org/wikipedia/en/thumb/0/09/Modern_Family_Season_Two_DVD_Cover.png/250px-Modern_Family_Season_Two_DVD_Cover.png", "Three modern-day families from California try to deal with their kids, quirky spouses and jobs in their own unique ways, often falling into hilarious situations.", 1, 4.2999999999999998, new DateTime(2009, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 93" },
                    { 92, "https://www.enigma-mag.com/wp-content/uploads/2019/08/1563490297.jpg", "A criminal mastermind who goes by \"The Professor\" has a plan to pull off the biggest heist in recorded history -- to print billions of euros in the Royal Mint of Spain. To help him carry out the ambitious plan, he recruits eight people with certain abilities and who have nothing to lose.", 1, 4.0, new DateTime(2017, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 92" },
                    { 91, "https://i.pinimg.com/originals/06/b2/2f/06b22f1ec7b9a6914ec6255f40953e98.jpg", "In April 1986, the city of Chernobyl in the Soviet Union suffers one of the worst nuclear disasters in the history of mankind. Consequently, many heroes put their lives on the line to save Europe.", 1, 3.6000000000000001, new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 91" },
                    { 90, "https://i.pinimg.com/originals/17/aa/71/17aa718c1ab15b482505b8431cf596fc.jpg", "Jake, who is paraplegic, replaces his twin on the Na'vi inhabited Pandora for a corporate mission. After the natives accept him as one of their own, he must decide where his loyalties lie.", 1, 4.9000000000000004, new DateTime(2009, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 90" },
                    { 89, "https://play-lh.googleusercontent.com/mMyoXM8bf72KK-Udap4-hAvqqdXgn0AIBXkS8zejT0RXITIh8oK9a-SYIVk89CA0rHJi", "Anakin joins forces with Obi-Wan and sets Palpatine free from the evil clutches of Count Doku. However, he falls prey to Palpatine and the Jedis' mind games and gives into temptation.", 1, 4.2000000000000002, new DateTime(2005, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 89" },
                    { 88, "https://www.themoviedb.org/t/p/w780/ic0intvXZSfBlYPIvWXpU1ivUCO.jpg", "Ryder and the pups are called to Adventure City to stop Mayor Humdinger from turning the bustling metropolis into a state of chaos.", 1, 3.7999999999999998, new DateTime(2021, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 88" },
                    { 87, "https://www.themoviedb.org/t/p/w780/clDFqATL4zcE7LzUwkrVj3zHvk7.jpg", "Cinderella, an orphaned girl with an evil stepmother, has big dreams and with the help of her Fabulous Godmother, she perseveres to make them come true.", 1, 3.5, new DateTime(2021, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 87" },
                    { 86, "https://www.themoviedb.org/t/p/w780/9dKCd55IuTT5QRs989m9Qlb7d2B.jpg", "Dr. Lily Houghton enlists the aid of wisecracking skipper Frank Wolff to take her down the Amazon in his dilapidated boat. Together, they search for an ancient tree that holds the power to heal – a discovery that will change the future of medicine.", 1, 4.0, new DateTime(2021, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 86" },
                    { 85, "https://www.themoviedb.org/t/p/w780/6Y9fl8tD1xtyUrOHV2MkCYTpzgi.jpg", "An off-duty SAS soldier, Tom Buckingham, must thwart a terror attack on a train running through the Channel Tunnel. As the action escalates on the train, events transpire in the corridors of power that may make the difference as to whether Buckingham and the civilian passengers make it out of the tunnel alive.", 1, 4.9000000000000004, new DateTime(2021, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 85" },
                    { 84, "https://www.themoviedb.org/t/p/w780/kb4s0ML0iVZlG6wAKbbs9NAm6X.jpg", "Supervillains Harley Quinn, Bloodsport, Peacemaker and a collection of nutty cons at Belle Reve prison join the super-secret, super-shady Task Force X as they are dropped off at the remote, enemy-infused island of Corto Maltese.", 1, 4.5, new DateTime(2021, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 84" },
                    { 83, "https://www.themoviedb.org/t/p/w780/34nDCQZwaEvsy4CFO5hkGRFDCVU.jpg", "The world is stunned when a group of time travelers arrive from the year 2051 to deliver an urgent message: Thirty years in the future, mankind is losing a global war against a deadly alien species.", 1, 4.9000000000000004, new DateTime(2021, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 83" },
                    { 94, "https://lumiere-a.akamaihd.net/v1/images/p_fyc_themandalorian_19097_de619ea9.jpeg", "After the defeat of the Empire at the hands of Rebel forces, a lone bounty hunter operating in the Outer Rim, away from the dominion of the New Republic, goes on many surprising and risky adventures.", 1, 4.2000000000000002, new DateTime(2019, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 94" },
                    { 54, "https://www.themoviedb.org/t/p/w780/pS1XUGjC6ASC1kvDCP3OJnwjk1t.jpg", "Shang-Chi must confront the past he thought he left behind when he is drawn into the web of the mysterious Ten Rings organization.", 1, 4.9000000000000004, new DateTime(2021, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 54" },
                    { 24, "https://lanetechchampion.org/wp-content/uploads/2020/09/zendya-euphoria.jpg", "An American adaptation of the Israeli show of the same name, \"Euphoria\" follows the troubled life of 17-year-old Rue, a drug addict fresh from rehab with no plans to stay clean.", 2, 3.0, new DateTime(2019, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Euphoria" },
                    { 52, "https://images-na.ssl-images-amazon.com/images/I/81bLWVTR3sL.jpg", "This adaptation of the \"Sabrina the Teenage Witch\" tale is a dark coming-of-age story that traffics in horror and the occult. In the reimagined origin story, Sabrina Spellman wrestles to reconcile her dual nature - half-witch, half-mortal.", 1, 4.0999999999999996, new DateTime(2018, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 52" },
                    { 23, "http://www.gstatic.com/tv/thumb/tvbanners/17114349/p17114349_b_v12_aa.jpg", "Surgical interns and their supervisors embark on a medical journey where they become part of heart-wrenching stories and make life-changing decisions in order to become the finest doctors.", 2, 5.0, new DateTime(2005, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grey's Anatomy" },
                    { 22, "https://m.media-amazon.com/images/M/MV5BMTg1NzUzNmQtMWI2ZC00YzdlLWI5ZTgtMGM3MjJlZTgwYTVlXkEyXkFqcGdeQXVyMTIzMzY2NDQ2._V1_FMjpg_UX1000_.jpg", "MacGyver, a contemporary hero and role model, applies his scientific knowledge to ordinary items to create a means of escape for himself and others from impending doom.", 2, 3.2999999999999998, new DateTime(2016, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "MacGyver" },
                    { 21, "https://i.pinimg.com/474x/4e/96/06/4e9606a7f386cafa7903e28e94e43627.jpg", "An ordinary British family contends with the hopes, anxieties and joys of an uncertain future in this six-part limited series that begins in 2019 and propels the characters 15 years forward into an unstable world.", 2, 2.7000000000000002, new DateTime(2019, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Years and Years" },
                    { 20, "https://static.wikia.nocookie.net/marvelcinematicuniverse/images/1/10/AoS_Season_One_Poster.jpg", "Agent Phil Coulson leads a team of highly skilled agents from the global law-enforcement organisation known as S.H.I.E.L.D. Together, they combat extraordinary and inexplicable threats.", 2, 4.9000000000000004, new DateTime(2013, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Agents of S.H.I.E.L.D." },
                    { 19, "https://static.wikia.nocookie.net/warner-bros-entertainment/images/c/cb/Harry-potter-and-the-prisoner-of-azkaban-movie-poster-style-f-11x17.jpg", "Harry, Ron and Hermoine return to Hogwarts just as they learn about Sirius Black and his plans to kill Harry. However, when Harry runs into him, he learns that the truth is far from reality.", 1, 4.0, new DateTime(2004, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and The Prisoner Of Azkaban" },
                    { 18, "https://www.hylandcinema.com/files/hyland/movie-posters/hp-_chamber.jpg", "A house-elf warns Harry against returning to Hogwarts, but he decides to ignore it. When students and creatures at the school begin to get petrified, Harry finds himself surrounded in mystery.", 1, 4.4000000000000004, new DateTime(2002, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and The Chamber Of Secrets" },
                    { 17, "https://movieguise.files.wordpress.com/2016/01/potter.jpg", "Harry Potter, an eleven-year-old orphan, discovers that he is a wizard and is invited to study at Hogwarts. Even as he escapes a dreary life and enters a world of magic, he finds trouble awaiting him.", 1, 4.5999999999999996, new DateTime(2001, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Philosopher's Stone" },
                    { 16, "https://lumiere-a.akamaihd.net/v1/images/p_fyc_themandalorian_19097_de619ea9.jpeg", "After the defeat of the Empire at the hands of Rebel forces, a lone bounty hunter operating in the Outer Rim, away from the dominion of the New Republic, goes on many surprising and risky adventures.", 2, 4.2000000000000002, new DateTime(2019, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Mandalorian" },
                    { 15, "https://upload.wikimedia.org/wikipedia/en/thumb/0/09/Modern_Family_Season_Two_DVD_Cover.png/250px-Modern_Family_Season_Two_DVD_Cover.png", "Three modern-day families from California try to deal with their kids, quirky spouses and jobs in their own unique ways, often falling into hilarious situations.", 2, 4.2999999999999998, new DateTime(2009, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Modern Family" },
                    { 14, "https://www.enigma-mag.com/wp-content/uploads/2019/08/1563490297.jpg", "A criminal mastermind who goes by \"The Professor\" has a plan to pull off the biggest heist in recorded history -- to print billions of euros in the Royal Mint of Spain. To help him carry out the ambitious plan, he recruits eight people with certain abilities and who have nothing to lose.", 2, 4.0, new DateTime(2017, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "La Casa De Papel" },
                    { 13, "https://i.pinimg.com/originals/06/b2/2f/06b22f1ec7b9a6914ec6255f40953e98.jpg", "In April 1986, the city of Chernobyl in the Soviet Union suffers one of the worst nuclear disasters in the history of mankind. Consequently, many heroes put their lives on the line to save Europe.", 2, 3.6000000000000001, new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chernobyl" }
                });

            migrationBuilder.InsertData(
                table: "Medias",
                columns: new[] { "Id", "CoverImage", "Description", "MediaType", "Rating", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 12, "https://i.pinimg.com/originals/17/aa/71/17aa718c1ab15b482505b8431cf596fc.jpg", "Jake, who is paraplegic, replaces his twin on the Na'vi inhabited Pandora for a corporate mission. After the natives accept him as one of their own, he must decide where his loyalties lie.", 1, 4.9000000000000004, new DateTime(2009, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Avatar" },
                    { 11, "https://play-lh.googleusercontent.com/mMyoXM8bf72KK-Udap4-hAvqqdXgn0AIBXkS8zejT0RXITIh8oK9a-SYIVk89CA0rHJi", "Anakin joins forces with Obi-Wan and sets Palpatine free from the evil clutches of Count Doku. However, he falls prey to Palpatine and the Jedis' mind games and gives into temptation.", 1, 4.2000000000000002, new DateTime(2005, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars: Revenge of the Sith (Episode III)" },
                    { 10, "https://m.media-amazon.com/images/M/MV5BMDAzM2M0Y2UtZjRmZi00MzVlLTg4MjEtOTE3NzU5ZDVlMTU5XkEyXkFqcGdeQXVyNDUyOTg3Njg@._V1_.jpg", "While pursuing an assassin, Obi Wan uncovers a sinister plot to destroy the Republic. With the fate of the galaxy hanging in the balance, the Jedi must defend the galaxy against the evil Sith.", 1, 3.7999999999999998, new DateTime(2002, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars: Attack of the Clones (Episode II)" },
                    { 9, "https://play-lh.googleusercontent.com/sR1pzOxnF50WLR3vUqXYFvY01_tLD4XPn1RDHf0Xh-W04Vek_3iiZ98U7Db2JcmrqS8", "Jedi Knights Qui-Gon Jinn and Obi-Wan Kenobi set out to stop the Trade Federation from invading Naboo. While travelling, they come across a gifted boy, Anakin, and learn that the Sith have returned.", 1, 3.5, new DateTime(1999, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars: The Phantom Menace (Episode I)" },
                    { 8, "https://lumiere-a.akamaihd.net/v1/images/star-wars-the-rise-of-skywalker-theatrical-poster-1000_ebc74357.jpeg?region=0%2C0%2C891%2C1372", "The revival of Emperor Palpatine resurrects the battle between the Resistance and the First Order while the Jedi's legendary conflict with the Sith Lord comes to a head.", 1, 4.0, new DateTime(2019, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars: The Rise of Skywalker (Episode IX)" },
                    { 7, "https://i.pinimg.com/originals/f4/5a/ea/f45aea75f65c0feb5cbe168f17a9a087.jpg", "Rey seeks to learn the ways of the Jedi under Luke Skywalker, its remaining member, to reinvigorate the Resistance's war against the First Order.", 1, 4.9000000000000004, new DateTime(2017, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars: The Last Jedi (Episode VIII)" },
                    { 6, "https://m.media-amazon.com/images/M/MV5BOTAzODEzNDAzMl5BMl5BanBnXkFtZTgwMDU1MTgzNzE@._V1_.jpg", "A new order threatens to destroy the New Republic. Finn, Rey and Poe, backed by the Resistance and the Republic, must find a way to stop them and find Luke, the last surviving Jedi.", 1, 4.5, new DateTime(2015, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars: The Force Awakens (Episode VII)" },
                    { 5, "https://m.media-amazon.com/images/I/91LlN7J+Z9L._SL1500_.jpg", "Luke Skywalker attempts to bring his father back to the light side of the Force. At the same time, the rebels hatch a plan to destroy the second Death Star.", 1, 4.9000000000000004, new DateTime(1983, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars: Return of the Jedi (Episode VI)" },
                    { 4, "https://static.wikia.nocookie.net/blacklist/images/5/57/Season_7_Poster.jpg", "A wanted fugitive mysteriously surrenders himself to the FBI and offers to help them capture deadly criminals. His sole condition is that he will work only with the new profiler, Elizabeth Keen.", 2, 5.0, new DateTime(2013, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Blacklist" },
                    { 3, "https://static.wikia.nocookie.net/riverdalearchie/images/3/3a/Season_2_Poster.jpg", "Archie, Betty, Jughead and Veronica tackle being teenagers in a town that is rife with sinister happenings and blood-thirsty criminals.", 2, 4.5, new DateTime(2017, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Riverdale" },
                    { 2, "https://images.penguinrandomhouse.com/cover/9780345320223", "Darth Vader is adamant about turning Luke Skywalker to the dark side. Master Yoda trains Luke to become a Jedi Knight while his friends try to fend off the Imperial fleet.", 1, 4.7999999999999998, new DateTime(1980, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars: The Empire Strikes Back (Episode V)" },
                    { 1, "https://kbimages1-a.akamaihd.net/538b1473-6d45-47f4-b16e-32a0a6ba7f9a/1200/1200/False/star-wars-episode-iv-a-new-hope-3.jpg", "After Princess Leia, the leader of the Rebel Alliance, is held hostage by Darth Vader, Luke and Han Solo must free her and destroy the powerful weapon created by the Galactic Empire.", 1, 5.0, new DateTime(1997, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars: A New Hope (Episode IV)" },
                    { 107, "https://movieguise.files.wordpress.com/2016/01/potter.jpg", "Harry Potter, an eleven-year-old orphan, discovers that he is a wizard and is invited to study at Hogwarts. Even as he escapes a dreary life and enters a world of magic, he finds trouble awaiting him.", 1, 0.0, new DateTime(2001, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 107" },
                    { 25, "https://www.syfy.com/sites/syfy/files/styles/1170xauto/public/the-witcher-season-2-poster-vertical.jpg", "The witcher Geralt, a mutated monster hunter, struggles to find his place in a world in which people often prove more wicked than beasts.", 2, 5.0, new DateTime(2019, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Witcher" },
                    { 26, "https://images-na.ssl-images-amazon.com/images/I/81bLWVTR3sL.jpg", "This adaptation of the \"Sabrina the Teenage Witch\" tale is a dark coming-of-age story that traffics in horror and the occult. In the reimagined origin story, Sabrina Spellman wrestles to reconcile her dual nature - half-witch, half-mortal.", 2, 4.0999999999999996, new DateTime(2018, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chilling Adventures of Sabrina" },
                    { 27, "https://www.themoviedb.org/t/p/w780/Czhr00kB8awffakEcQS5ON1ELm.jpg", "Female adventurer Parker joins a crew of male trophy hunters in a remote wilderness park. Their goal: slaughter genetically recreated dinosaurs for sport using rifles, arrows, and grenades.", 1, 5.0, new DateTime(2021, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jurassic Hunt" },
                    { 51, "https://www.syfy.com/sites/syfy/files/styles/1170xauto/public/the-witcher-season-2-poster-vertical.jpg", "The witcher Geralt, a mutated monster hunter, struggles to find his place in a world in which people often prove more wicked than beasts.", 1, 5.0, new DateTime(2019, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 51" },
                    { 50, "https://lanetechchampion.org/wp-content/uploads/2020/09/zendya-euphoria.jpg", "An American adaptation of the Israeli show of the same name, \"Euphoria\" follows the troubled life of 17-year-old Rue, a drug addict fresh from rehab with no plans to stay clean.", 1, 3.0, new DateTime(2019, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 50" },
                    { 49, "http://www.gstatic.com/tv/thumb/tvbanners/17114349/p17114349_b_v12_aa.jpg", "Surgical interns and their supervisors embark on a medical journey where they become part of heart-wrenching stories and make life-changing decisions in order to become the finest doctors.", 1, 5.0, new DateTime(2005, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 49" },
                    { 48, "https://m.media-amazon.com/images/M/MV5BMTg1NzUzNmQtMWI2ZC00YzdlLWI5ZTgtMGM3MjJlZTgwYTVlXkEyXkFqcGdeQXVyMTIzMzY2NDQ2._V1_FMjpg_UX1000_.jpg", "MacGyver, a contemporary hero and role model, applies his scientific knowledge to ordinary items to create a means of escape for himself and others from impending doom.", 1, 3.2999999999999998, new DateTime(2016, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 48" },
                    { 47, "https://i.pinimg.com/474x/4e/96/06/4e9606a7f386cafa7903e28e94e43627.jpg", "An ordinary British family contends with the hopes, anxieties and joys of an uncertain future in this six-part limited series that begins in 2019 and propels the characters 15 years forward into an unstable world.", 1, 2.7000000000000002, new DateTime(2019, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 47" },
                    { 46, "https://static.wikia.nocookie.net/marvelcinematicuniverse/images/1/10/AoS_Season_One_Poster.jpg", "Agent Phil Coulson leads a team of highly skilled agents from the global law-enforcement organisation known as S.H.I.E.L.D. Together, they combat extraordinary and inexplicable threats.", 1, 4.9000000000000004, new DateTime(2013, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 46" },
                    { 45, "https://static.wikia.nocookie.net/warner-bros-entertainment/images/c/cb/Harry-potter-and-the-prisoner-of-azkaban-movie-poster-style-f-11x17.jpg", "Harry, Ron and Hermoine return to Hogwarts just as they learn about Sirius Black and his plans to kill Harry. However, when Harry runs into him, he learns that the truth is far from reality.", 1, 4.0, new DateTime(2004, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 45" },
                    { 44, "https://www.hylandcinema.com/files/hyland/movie-posters/hp-_chamber.jpg", "A house-elf warns Harry against returning to Hogwarts, but he decides to ignore it. When students and creatures at the school begin to get petrified, Harry finds himself surrounded in mystery.", 1, 4.4000000000000004, new DateTime(2002, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 44" },
                    { 43, "https://movieguise.files.wordpress.com/2016/01/potter.jpg", "Harry Potter, an eleven-year-old orphan, discovers that he is a wizard and is invited to study at Hogwarts. Even as he escapes a dreary life and enters a world of magic, he finds trouble awaiting him.", 1, 4.5999999999999996, new DateTime(2001, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 43" },
                    { 42, "https://lumiere-a.akamaihd.net/v1/images/p_fyc_themandalorian_19097_de619ea9.jpeg", "After the defeat of the Empire at the hands of Rebel forces, a lone bounty hunter operating in the Outer Rim, away from the dominion of the New Republic, goes on many surprising and risky adventures.", 1, 4.2000000000000002, new DateTime(2019, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 42" },
                    { 41, "https://upload.wikimedia.org/wikipedia/en/thumb/0/09/Modern_Family_Season_Two_DVD_Cover.png/250px-Modern_Family_Season_Two_DVD_Cover.png", "Three modern-day families from California try to deal with their kids, quirky spouses and jobs in their own unique ways, often falling into hilarious situations.", 1, 4.2999999999999998, new DateTime(2009, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 41" },
                    { 53, "https://www.themoviedb.org/t/p/w780/Czhr00kB8awffakEcQS5ON1ELm.jpg", "Female adventurer Parker joins a crew of male trophy hunters in a remote wilderness park. Their goal: slaughter genetically recreated dinosaurs for sport using rifles, arrows, and grenades.", 1, 5.0, new DateTime(2021, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 53" },
                    { 40, "https://www.enigma-mag.com/wp-content/uploads/2019/08/1563490297.jpg", "A criminal mastermind who goes by \"The Professor\" has a plan to pull off the biggest heist in recorded history -- to print billions of euros in the Royal Mint of Spain. To help him carry out the ambitious plan, he recruits eight people with certain abilities and who have nothing to lose.", 1, 4.0, new DateTime(2017, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 40" },
                    { 38, "https://i.pinimg.com/originals/17/aa/71/17aa718c1ab15b482505b8431cf596fc.jpg", "Jake, who is paraplegic, replaces his twin on the Na'vi inhabited Pandora for a corporate mission. After the natives accept him as one of their own, he must decide where his loyalties lie.", 1, 4.9000000000000004, new DateTime(2009, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 38" },
                    { 37, "https://play-lh.googleusercontent.com/mMyoXM8bf72KK-Udap4-hAvqqdXgn0AIBXkS8zejT0RXITIh8oK9a-SYIVk89CA0rHJi", "Anakin joins forces with Obi-Wan and sets Palpatine free from the evil clutches of Count Doku. However, he falls prey to Palpatine and the Jedis' mind games and gives into temptation.", 1, 4.2000000000000002, new DateTime(2005, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 37" },
                    { 36, "https://www.themoviedb.org/t/p/w780/ic0intvXZSfBlYPIvWXpU1ivUCO.jpg", "Ryder and the pups are called to Adventure City to stop Mayor Humdinger from turning the bustling metropolis into a state of chaos.", 1, 3.7999999999999998, new DateTime(2021, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "PAW Patrol: The Movie" },
                    { 35, "https://www.themoviedb.org/t/p/w780/clDFqATL4zcE7LzUwkrVj3zHvk7.jpg", "Cinderella, an orphaned girl with an evil stepmother, has big dreams and with the help of her Fabulous Godmother, she perseveres to make them come true.", 1, 3.5, new DateTime(2021, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cinderella" },
                    { 34, "https://www.themoviedb.org/t/p/w780/9dKCd55IuTT5QRs989m9Qlb7d2B.jpg", "Dr. Lily Houghton enlists the aid of wisecracking skipper Frank Wolff to take her down the Amazon in his dilapidated boat. Together, they search for an ancient tree that holds the power to heal – a discovery that will change the future of medicine.", 1, 4.0, new DateTime(2021, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jungle Cruise" },
                    { 33, "https://www.themoviedb.org/t/p/w780/6Y9fl8tD1xtyUrOHV2MkCYTpzgi.jpg", "An off-duty SAS soldier, Tom Buckingham, must thwart a terror attack on a train running through the Channel Tunnel. As the action escalates on the train, events transpire in the corridors of power that may make the difference as to whether Buckingham and the civilian passengers make it out of the tunnel alive.", 1, 4.9000000000000004, new DateTime(2021, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "SAS: Red Notice" },
                    { 32, "https://www.themoviedb.org/t/p/w780/kb4s0ML0iVZlG6wAKbbs9NAm6X.jpg", "Supervillains Harley Quinn, Bloodsport, Peacemaker and a collection of nutty cons at Belle Reve prison join the super-secret, super-shady Task Force X as they are dropped off at the remote, enemy-infused island of Corto Maltese.", 1, 4.5, new DateTime(2021, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Suicide Squad" },
                    { 31, "https://www.themoviedb.org/t/p/w780/34nDCQZwaEvsy4CFO5hkGRFDCVU.jpg", "The world is stunned when a group of time travelers arrive from the year 2051 to deliver an urgent message: Thirty years in the future, mankind is losing a global war against a deadly alien species.", 1, 4.9000000000000004, new DateTime(2021, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Tomorrow War" },
                    { 30, "https://www.themoviedb.org/t/p/w780/34nDCQZwaEvsy4CFO5hkGRFDCVU.jpg", "The world is stunned when a group of time travelers arrive from the year 2051 to deliver an urgent message: Thirty years in the future, mankind is losing a global war against a deadly alien species.", 1, 5.0, new DateTime(2021, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Don't Breathe 2" },
                    { 29, "https://www.themoviedb.org/t/p/w780/hRMfgGFRAZIlvwVWy8DYJdLTpvN.jpg", "The Blind Man has been hiding out for several years in an isolated cabin and has taken in and raised a young girl orphaned from a devastating house fire. Their quiet life together is shattered when a group of criminals kidnap the girl.", 1, 4.5, new DateTime(2021, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Don't Breathe 2" },
                    { 28, "https://www.themoviedb.org/t/p/w780/pS1XUGjC6ASC1kvDCP3OJnwjk1t.jpg", "Shang-Chi must confront the past he thought he left behind when he is drawn into the web of the mysterious Ten Rings organization.", 1, 4.9000000000000004, new DateTime(2021, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shang-Chi and the Legend of the Ten Rings" },
                    { 39, "https://i.pinimg.com/originals/06/b2/2f/06b22f1ec7b9a6914ec6255f40953e98.jpg", "In April 1986, the city of Chernobyl in the Soviet Union suffers one of the worst nuclear disasters in the history of mankind. Consequently, many heroes put their lives on the line to save Europe.", 1, 3.6000000000000001, new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Title 39" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "PasswordSalt" },
                values: new object[] { 1, "admin@admin.com", "Administrator", new byte[] { 250, 115, 80, 182, 26, 185, 103, 212, 86, 16, 177, 153, 14, 85, 162, 84, 140, 187, 225, 144, 241, 129, 186, 198, 21, 88, 98, 66, 184, 233, 115, 132, 34, 34, 115, 163, 75, 230, 103, 94, 6, 16, 167, 153, 243, 226, 190, 251, 210, 77, 154, 220, 4, 231, 203, 71, 175, 213, 131, 174, 209, 171, 6, 102 }, new byte[] { 158, 19, 171, 111, 206, 198, 189, 125, 90, 117, 190, 35, 11, 245, 253, 132, 7, 183, 253, 133, 155, 124, 174, 110, 143, 76, 65, 138, 29, 141, 142, 109, 57, 12, 131, 73, 133, 152, 25, 71, 48, 59, 191, 127, 23, 174, 7, 14, 70, 26, 153, 239, 106, 218, 165, 58, 141, 52, 171, 107, 13, 99, 64, 13, 157, 247, 204, 156, 65, 241, 64, 219, 34, 162, 193, 66, 119, 72, 175, 244, 88, 19, 97, 188, 111, 121, 37, 37, 100, 235, 60, 5, 40, 88, 249, 236, 179, 114, 218, 237, 8, 106, 50, 159, 182, 77, 127, 229, 150, 153, 245, 145, 198, 2, 57, 216, 114, 168, 215, 127, 70, 212, 165, 52, 76, 81, 131, 104 } });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "RatedMediaId", "UserId", "Value" },
                values: new object[,]
                {
                    { 1, 1, null, 5.0 },
                    { 70, 70, null, 4.0 },
                    { 71, 71, null, 4.9000000000000004 },
                    { 39, 39, null, 3.6000000000000001 },
                    { 38, 38, null, 4.9000000000000004 },
                    { 37, 37, null, 4.2000000000000002 },
                    { 72, 72, null, 2.7000000000000002 },
                    { 73, 73, null, 3.2999999999999998 },
                    { 74, 74, null, 5.0 },
                    { 75, 75, null, 3.0 },
                    { 76, 76, null, 5.0 },
                    { 36, 36, null, 3.7999999999999998 },
                    { 40, 40, null, 4.0 },
                    { 77, 77, null, 5.0 },
                    { 34, 34, null, 4.0 },
                    { 78, 78, null, 4.7999999999999998 },
                    { 33, 33, null, 4.9000000000000004 },
                    { 32, 32, null, 4.5 },
                    { 79, 79, null, 4.5 },
                    { 59, 59, null, 4.0 },
                    { 81, 81, null, 4.9000000000000004 },
                    { 82, 82, null, 4.5 },
                    { 83, 83, null, 4.9000000000000004 },
                    { 31, 31, null, 4.9000000000000004 },
                    { 84, 84, null, 4.0 },
                    { 35, 35, null, 3.5 },
                    { 41, 41, null, 4.2999999999999998 },
                    { 42, 42, null, 4.2000000000000002 },
                    { 69, 69, null, 4.4000000000000004 },
                    { 60, 60, null, 3.5 },
                    { 58, 58, null, 4.9000000000000004 },
                    { 57, 57, null, 4.5 },
                    { 56, 56, null, 4.9000000000000004 },
                    { 61, 61, null, 3.7999999999999998 },
                    { 55, 55, null, 5.0 },
                    { 62, 62, null, 4.2000000000000002 },
                    { 54, 54, null, 4.5 },
                    { 53, 53, null, 4.7999999999999998 },
                    { 52, 52, null, 5.0 },
                    { 51, 51, null, 5.0 },
                    { 63, 63, null, 4.9000000000000004 },
                    { 50, 50, null, 3.0 }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "RatedMediaId", "UserId", "Value" },
                values: new object[,]
                {
                    { 64, 64, null, 3.6000000000000001 },
                    { 49, 49, null, 5.0 },
                    { 48, 48, null, 3.2999999999999998 },
                    { 65, 65, null, 4.0 },
                    { 47, 47, null, 2.7000000000000002 },
                    { 66, 66, null, 4.2999999999999998 },
                    { 46, 46, null, 4.9000000000000004 },
                    { 45, 45, null, 4.0 },
                    { 67, 67, null, 4.2000000000000002 },
                    { 68, 68, null, 4.5999999999999996 },
                    { 44, 44, null, 4.4000000000000004 },
                    { 43, 43, null, 4.5999999999999996 },
                    { 30, 30, null, 5.0 },
                    { 29, 29, null, 4.5 },
                    { 80, 80, null, 5.0 },
                    { 28, 28, null, 4.7999999999999998 },
                    { 11, 11, null, 4.2000000000000002 },
                    { 10, 10, null, 3.7999999999999998 },
                    { 9, 9, null, 3.5 },
                    { 8, 8, null, 4.0 },
                    { 7, 7, null, 4.9000000000000004 },
                    { 6, 6, null, 4.5 },
                    { 85, 85, null, 3.5 },
                    { 90, 90, null, 4.0 },
                    { 4, 4, null, 5.0 },
                    { 91, 91, null, 4.2999999999999998 },
                    { 12, 12, null, 4.9000000000000004 },
                    { 92, 92, null, 4.2000000000000002 },
                    { 94, 94, null, 4.4000000000000004 },
                    { 95, 95, null, 4.0 },
                    { 96, 96, null, 4.9000000000000004 },
                    { 97, 97, null, 2.7000000000000002 },
                    { 3, 3, null, 4.5 },
                    { 2, 2, null, 4.7999999999999998 },
                    { 98, 98, null, 3.2999999999999998 },
                    { 99, 99, null, 5.0 },
                    { 100, 100, null, 3.0 },
                    { 101, 101, null, 5.0 },
                    { 93, 93, null, 4.5999999999999996 },
                    { 13, 13, null, 3.6000000000000001 },
                    { 5, 5, null, 4.9000000000000004 },
                    { 14, 14, null, 4.0 }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "RatedMediaId", "UserId", "Value" },
                values: new object[,]
                {
                    { 23, 23, null, 5.0 },
                    { 24, 24, null, 3.0 },
                    { 25, 25, null, 5.0 },
                    { 87, 87, null, 4.2000000000000002 },
                    { 88, 88, null, 4.9000000000000004 },
                    { 22, 22, null, 3.2999999999999998 },
                    { 89, 89, null, 3.6000000000000001 },
                    { 21, 21, null, 2.7000000000000002 },
                    { 86, 86, null, 3.7999999999999998 },
                    { 27, 27, null, 5.0 },
                    { 26, 26, null, 4.0999999999999996 },
                    { 15, 15, null, 4.2999999999999998 },
                    { 16, 16, null, 4.2000000000000002 },
                    { 17, 17, null, 4.5999999999999996 },
                    { 20, 20, null, 4.9000000000000004 },
                    { 19, 19, null, 4.0 },
                    { 18, 18, null, 4.4000000000000004 }
                });

            migrationBuilder.InsertData(
                table: "Screenings",
                columns: new[] { "Id", "AvailableSeats", "Duration", "MediaId", "Name", "ReservedSeats", "StartTime" },
                values: new object[,]
                {
                    { 103, 40, 100f, 103, "Screening 103", 4, new DateTime(2021, 10, 22, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(2034) },
                    { 77, 40, 100f, 59, "Screening 77", 0, new DateTime(2021, 10, 19, 23, 6, 1, 349, DateTimeKind.Local).AddTicks(1974) },
                    { 40, 40, 100f, 62, "Screening 40", 0, new DateTime(2021, 10, 13, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(1888) },
                    { 33, 40, 100f, 62, "Screening 33", 0, new DateTime(2021, 10, 17, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(1845) },
                    { 102, 40, 100f, 102, "Screening 102", 7, new DateTime(2021, 10, 19, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(2032) },
                    { 105, 40, 100f, 105, "Screening 105", 2, new DateTime(2021, 10, 23, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(2039) },
                    { 101, 40, 100f, 99, "Screening 101", 0, new DateTime(2021, 11, 1, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(2030) },
                    { 32, 40, 100f, 62, "Screening 32", 0, new DateTime(2021, 10, 13, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(1842) },
                    { 104, 40, 100f, 104, "Screening 104", 5, new DateTime(2021, 10, 21, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(2036) },
                    { 80, 40, 100f, 100, "Screening 80", 0, new DateTime(2021, 10, 16, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(1981) },
                    { 34, 40, 100f, 62, "Screening 34", 0, new DateTime(2021, 10, 19, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(1847) },
                    { 74, 40, 100f, 81, "Screening 74", 9, new DateTime(2021, 10, 29, 22, 6, 1, 349, DateTimeKind.Local).AddTicks(1967) },
                    { 78, 40, 100f, 64, "Screening 78", 0, new DateTime(2021, 11, 13, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(1976) },
                    { 63, 40, 100f, 63, "Screening 63", 0, new DateTime(2021, 11, 7, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(1942) },
                    { 95, 40, 100f, 72, "Screening 95", 0, new DateTime(2021, 10, 21, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(2016) },
                    { 39, 40, 100f, 89, "Screening 39", 0, new DateTime(2021, 10, 14, 19, 6, 1, 349, DateTimeKind.Local).AddTicks(1886) },
                    { 65, 40, 100f, 68, "Screening 65", 0, new DateTime(2021, 11, 10, 0, 6, 1, 349, DateTimeKind.Local).AddTicks(1946) },
                    { 79, 40, 100f, 89, "Screening 79", 0, new DateTime(2021, 11, 4, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(1979) },
                    { 38, 40, 100f, 89, "Screening 38", 0, new DateTime(2021, 10, 11, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(1883) },
                    { 67, 40, 100f, 67, "Screening 67", 0, new DateTime(2021, 11, 10, 22, 6, 1, 349, DateTimeKind.Local).AddTicks(1950) },
                    { 35, 40, 100f, 89, "Screening 35", 0, new DateTime(2021, 10, 18, 22, 6, 1, 349, DateTimeKind.Local).AddTicks(1850) },
                    { 37, 40, 100f, 77, "Screening 37", 0, new DateTime(2021, 10, 21, 22, 6, 1, 349, DateTimeKind.Local).AddTicks(1854) },
                    { 73, 40, 100f, 66, "Screening 73", 0, new DateTime(2021, 10, 30, 22, 6, 1, 349, DateTimeKind.Local).AddTicks(1964) },
                    { 72, 40, 100f, 66, "Screening 72", 0, new DateTime(2021, 11, 21, 19, 6, 1, 349, DateTimeKind.Local).AddTicks(1962) },
                    { 88, 40, 100f, 65, "Screening 88", 0, new DateTime(2021, 10, 24, 23, 6, 1, 349, DateTimeKind.Local).AddTicks(2000) }
                });

            migrationBuilder.InsertData(
                table: "Screenings",
                columns: new[] { "Id", "AvailableSeats", "Duration", "MediaId", "Name", "ReservedSeats", "StartTime" },
                values: new object[,]
                {
                    { 43, 40, 100f, 88, "Screening 43", 0, new DateTime(2021, 10, 22, 15, 6, 1, 349, DateTimeKind.Local).AddTicks(1895) },
                    { 52, 40, 100f, 71, "Screening 52", 0, new DateTime(2021, 10, 13, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(1917) },
                    { 89, 40, 100f, 96, "Screening 89", 0, new DateTime(2021, 12, 3, 23, 6, 1, 349, DateTimeKind.Local).AddTicks(2002) },
                    { 64, 40, 100f, 63, "Screening 64", 0, new DateTime(2021, 10, 30, 23, 6, 1, 349, DateTimeKind.Local).AddTicks(1944) },
                    { 62, 40, 100f, 63, "Screening 62", 8, new DateTime(2021, 11, 21, 22, 6, 1, 349, DateTimeKind.Local).AddTicks(1939) },
                    { 49, 40, 100f, 58, "Screening 49", 0, new DateTime(2021, 10, 24, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(1910) },
                    { 50, 40, 100f, 47, "Screening 50", 0, new DateTime(2021, 10, 19, 19, 6, 1, 349, DateTimeKind.Local).AddTicks(1912) },
                    { 47, 40, 100f, 58, "Screening 47", 0, new DateTime(2021, 10, 18, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(1906) },
                    { 85, 40, 100f, 5, "Screening 85", 0, new DateTime(2021, 10, 23, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(1993) },
                    { 86, 40, 100f, 5, "Screening 86", 0, new DateTime(2021, 10, 23, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(1995) },
                    { 16, 40, 100f, 20, "Screening 16", 0, new DateTime(2021, 10, 15, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(1806) },
                    { 17, 40, 100f, 20, "Screening 17", 0, new DateTime(2021, 10, 9, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(1808) },
                    { 36, 40, 100f, 21, "Screening 36", 10, new DateTime(2021, 10, 18, 19, 6, 1, 349, DateTimeKind.Local).AddTicks(1852) },
                    { 18, 40, 100f, 22, "Screening 18", 0, new DateTime(2021, 10, 7, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(1810) },
                    { 19, 40, 100f, 22, "Screening 19", 0, new DateTime(2021, 10, 7, 23, 6, 1, 349, DateTimeKind.Local).AddTicks(1813) },
                    { 20, 40, 100f, 22, "Screening 20", 0, new DateTime(2021, 10, 10, 19, 6, 1, 349, DateTimeKind.Local).AddTicks(1815) },
                    { 21, 40, 100f, 22, "Screening 21", 0, new DateTime(2021, 10, 11, 19, 6, 1, 349, DateTimeKind.Local).AddTicks(1817) },
                    { 96, 40, 100f, 28, "Screening 96", 0, new DateTime(2021, 10, 21, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(2018) },
                    { 22, 40, 100f, 30, "Screening 22", 1, new DateTime(2021, 10, 10, 19, 6, 1, 349, DateTimeKind.Local).AddTicks(1820) },
                    { 54, 40, 100f, 31, "Screening 54", 0, new DateTime(2021, 10, 15, 22, 6, 1, 349, DateTimeKind.Local).AddTicks(1921) },
                    { 55, 40, 100f, 31, "Screening 55", 0, new DateTime(2021, 10, 30, 22, 6, 1, 349, DateTimeKind.Local).AddTicks(1924) },
                    { 56, 40, 100f, 31, "Screening 56", 0, new DateTime(2021, 10, 30, 19, 6, 1, 349, DateTimeKind.Local).AddTicks(1926) },
                    { 57, 40, 100f, 31, "Screening 57", 0, new DateTime(2021, 10, 20, 22, 6, 1, 349, DateTimeKind.Local).AddTicks(1928) },
                    { 84, 40, 100f, 4, "Screening 84", 0, new DateTime(2021, 10, 29, 19, 6, 1, 349, DateTimeKind.Local).AddTicks(1990) },
                    { 58, 40, 100f, 31, "Screening 58", 0, new DateTime(2021, 10, 30, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(1930) },
                    { 83, 40, 100f, 3, "Screening 83", 0, new DateTime(2021, 10, 28, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(1988) },
                    { 81, 40, 100f, 3, "Screening 81", 0, new DateTime(2021, 11, 9, 22, 6, 1, 349, DateTimeKind.Local).AddTicks(1983) },
                    { 1, 40, 120f, 1, "Screening 1", 23, new DateTime(2021, 10, 7, 20, 6, 1, 347, DateTimeKind.Local).AddTicks(4573) },
                    { 2, 40, 80f, 1, "Screening 2", 0, new DateTime(2021, 10, 7, 23, 6, 1, 349, DateTimeKind.Local).AddTicks(1747) },
                    { 3, 40, 100f, 1, "Screening 3", 13, new DateTime(2021, 10, 8, 23, 6, 1, 349, DateTimeKind.Local).AddTicks(1772) },
                    { 4, 40, 80f, 1, "Screening 4", 0, new DateTime(2021, 10, 7, 23, 6, 1, 349, DateTimeKind.Local).AddTicks(1777) },
                    { 5, 40, 80f, 1, "Screening 5", 0, new DateTime(2021, 10, 9, 23, 6, 1, 349, DateTimeKind.Local).AddTicks(1779) },
                    { 6, 40, 100f, 1, "Screening 6", 0, new DateTime(2021, 10, 10, 23, 6, 1, 349, DateTimeKind.Local).AddTicks(1782) },
                    { 7, 40, 120f, 1, "Screening 7", 0, new DateTime(2021, 10, 7, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(1784) },
                    { 8, 40, 120f, 1, "Screening 8", 0, new DateTime(2021, 10, 8, 22, 6, 1, 349, DateTimeKind.Local).AddTicks(1787) },
                    { 9, 40, 120f, 1, "Screening 9", 0, new DateTime(2021, 10, 9, 0, 6, 1, 349, DateTimeKind.Local).AddTicks(1789) },
                    { 10, 40, 120f, 1, "Screening 10", 0, new DateTime(2021, 10, 11, 0, 6, 1, 349, DateTimeKind.Local).AddTicks(1792) },
                    { 11, 40, 100f, 3, "Screening 11", 0, new DateTime(2021, 10, 13, 0, 6, 1, 349, DateTimeKind.Local).AddTicks(1794) },
                    { 12, 40, 100f, 3, "Screening 12", 0, new DateTime(2021, 10, 12, 19, 6, 1, 349, DateTimeKind.Local).AddTicks(1797) },
                    { 13, 40, 100f, 3, "Screening 13", 9, new DateTime(2021, 10, 12, 16, 6, 1, 349, DateTimeKind.Local).AddTicks(1799) },
                    { 14, 40, 100f, 3, "Screening 14", 0, new DateTime(2021, 10, 14, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(1801) },
                    { 15, 40, 100f, 3, "Screening 15", 0, new DateTime(2021, 10, 14, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(1804) }
                });

            migrationBuilder.InsertData(
                table: "Screenings",
                columns: new[] { "Id", "AvailableSeats", "Duration", "MediaId", "Name", "ReservedSeats", "StartTime" },
                values: new object[,]
                {
                    { 82, 40, 100f, 3, "Screening 82", 0, new DateTime(2021, 11, 4, 17, 6, 1, 349, DateTimeKind.Local).AddTicks(1986) },
                    { 48, 40, 100f, 58, "Screening 48", 0, new DateTime(2021, 10, 18, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(1908) },
                    { 97, 40, 100f, 31, "Screening 97", 0, new DateTime(2021, 11, 19, 19, 6, 1, 349, DateTimeKind.Local).AddTicks(2021) },
                    { 59, 40, 100f, 34, "Screening 59", 0, new DateTime(2021, 11, 3, 22, 6, 1, 349, DateTimeKind.Local).AddTicks(1933) },
                    { 66, 40, 100f, 47, "Screening 66", 0, new DateTime(2021, 10, 18, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(1948) },
                    { 92, 40, 100f, 49, "Screening 92", 0, new DateTime(2021, 10, 21, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(2008) },
                    { 93, 40, 100f, 49, "Screening 93", 0, new DateTime(2021, 11, 8, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(2011) },
                    { 98, 40, 100f, 49, "Screening 98", 0, new DateTime(2021, 10, 20, 23, 6, 1, 349, DateTimeKind.Local).AddTicks(2023) },
                    { 100, 40, 100f, 49, "Screening 100", 0, new DateTime(2021, 10, 23, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(2027) },
                    { 27, 40, 100f, 50, "Screening 27", 0, new DateTime(2021, 10, 10, 23, 6, 1, 349, DateTimeKind.Local).AddTicks(1831) },
                    { 28, 40, 100f, 50, "Screening 28", 0, new DateTime(2021, 10, 16, 23, 6, 1, 349, DateTimeKind.Local).AddTicks(1833) },
                    { 60, 40, 100f, 52, "Screening 60", 0, new DateTime(2021, 11, 17, 22, 6, 1, 349, DateTimeKind.Local).AddTicks(1935) },
                    { 90, 40, 100f, 53, "Screening 90", 0, new DateTime(2021, 11, 9, 19, 6, 1, 349, DateTimeKind.Local).AddTicks(2004) },
                    { 99, 40, 100f, 53, "Screening 99", 0, new DateTime(2021, 11, 13, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(2025) },
                    { 53, 40, 100f, 54, "Screening 53", 0, new DateTime(2021, 10, 13, 22, 6, 1, 349, DateTimeKind.Local).AddTicks(1919) },
                    { 61, 40, 100f, 54, "Screening 61", 0, new DateTime(2021, 10, 26, 19, 6, 1, 349, DateTimeKind.Local).AddTicks(1937) },
                    { 75, 40, 100f, 55, "Screening 75", 0, new DateTime(2021, 10, 8, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(1969) },
                    { 45, 40, 100f, 58, "Screening 45", 0, new DateTime(2021, 10, 28, 22, 6, 1, 349, DateTimeKind.Local).AddTicks(1901) },
                    { 46, 40, 100f, 58, "Screening 46", 0, new DateTime(2021, 10, 19, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(1903) },
                    { 106, 40, 100f, 106, "Screening 106", 2, new DateTime(2021, 10, 24, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(2041) },
                    { 31, 40, 100f, 33, "Screening 31", 7, new DateTime(2021, 10, 14, 23, 6, 1, 349, DateTimeKind.Local).AddTicks(1840) },
                    { 51, 40, 100f, 46, "Screening 51", 0, new DateTime(2021, 10, 12, 19, 6, 1, 349, DateTimeKind.Local).AddTicks(1915) },
                    { 41, 40, 100f, 45, "Screening 41", 0, new DateTime(2021, 10, 23, 19, 6, 1, 349, DateTimeKind.Local).AddTicks(1891) },
                    { 94, 40, 100f, 35, "Screening 94", 0, new DateTime(2021, 10, 21, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(2013) },
                    { 23, 40, 100f, 36, "Screening 23", 0, new DateTime(2021, 10, 10, 19, 6, 1, 349, DateTimeKind.Local).AddTicks(1822) },
                    { 24, 40, 100f, 36, "Screening 24", 0, new DateTime(2021, 10, 12, 19, 6, 1, 349, DateTimeKind.Local).AddTicks(1824) },
                    { 25, 40, 100f, 36, "Screening 25", 0, new DateTime(2021, 10, 12, 23, 6, 1, 349, DateTimeKind.Local).AddTicks(1827) },
                    { 26, 40, 100f, 36, "Screening 26", 0, new DateTime(2021, 10, 10, 19, 6, 1, 349, DateTimeKind.Local).AddTicks(1829) },
                    { 76, 40, 100f, 36, "Screening 76", 0, new DateTime(2021, 12, 2, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(1971) },
                    { 91, 40, 100f, 36, "Screening 91", 0, new DateTime(2021, 11, 7, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(2006) },
                    { 70, 40, 100f, 37, "Screening 70", 0, new DateTime(2021, 11, 8, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(1957) },
                    { 68, 40, 100f, 39, "Screening 68", 0, new DateTime(2021, 11, 25, 22, 6, 1, 349, DateTimeKind.Local).AddTicks(1953) },
                    { 69, 40, 100f, 39, "Screening 69", 0, new DateTime(2021, 11, 3, 19, 6, 1, 349, DateTimeKind.Local).AddTicks(1955) },
                    { 44, 40, 100f, 42, "Screening 44", 6, new DateTime(2021, 10, 23, 17, 6, 1, 349, DateTimeKind.Local).AddTicks(1898) },
                    { 87, 40, 100f, 43, "Screening 87", 0, new DateTime(2021, 10, 29, 19, 6, 1, 349, DateTimeKind.Local).AddTicks(1997) },
                    { 29, 40, 100f, 44, "Screening 29", 0, new DateTime(2021, 10, 14, 23, 6, 1, 349, DateTimeKind.Local).AddTicks(1836) },
                    { 30, 40, 100f, 44, "Screening 30", 0, new DateTime(2021, 10, 14, 20, 6, 1, 349, DateTimeKind.Local).AddTicks(1838) },
                    { 71, 40, 100f, 44, "Screening 71", 0, new DateTime(2021, 10, 20, 0, 6, 1, 349, DateTimeKind.Local).AddTicks(1960) },
                    { 42, 40, 100f, 45, "Screening 42", 0, new DateTime(2021, 10, 18, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(1893) },
                    { 107, 40, 100f, 107, "Screening 107", 1, new DateTime(2021, 10, 27, 21, 6, 1, 349, DateTimeKind.Local).AddTicks(2044) }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Price", "ScreeningId", "UserId" },
                values: new object[,]
                {
                    { 1, 7.9900000000000002, 1, 1 },
                    { 68, 7.9900000000000002, 62, 1 },
                    { 67, 7.9900000000000002, 62, 1 },
                    { 66, 7.9900000000000002, 62, 1 },
                    { 65, 7.9900000000000002, 62, 1 },
                    { 64, 7.9900000000000002, 62, 1 },
                    { 100, 7.9900000000000002, 61, 1 },
                    { 99, 7.9900000000000002, 61, 1 },
                    { 98, 7.9900000000000002, 61, 1 },
                    { 97, 7.9900000000000002, 61, 1 },
                    { 77, 7.9900000000000002, 44, 1 },
                    { 76, 7.9900000000000002, 44, 1 },
                    { 75, 7.9900000000000002, 44, 1 },
                    { 69, 7.9900000000000002, 62, 1 },
                    { 74, 7.9900000000000002, 44, 1 },
                    { 72, 7.9900000000000002, 44, 1 },
                    { 56, 7.9900000000000002, 25, 1 },
                    { 55, 7.9900000000000002, 25, 1 },
                    { 54, 7.9900000000000002, 25, 1 },
                    { 53, 7.9900000000000002, 25, 1 },
                    { 52, 7.9900000000000002, 25, 1 },
                    { 51, 7.9900000000000002, 25, 1 },
                    { 50, 7.9900000000000002, 25, 1 },
                    { 49, 7.9900000000000002, 25, 1 },
                    { 48, 7.9900000000000002, 25, 1 },
                    { 47, 7.9900000000000002, 25, 1 },
                    { 46, 7.9900000000000002, 25, 1 },
                    { 73, 7.9900000000000002, 44, 1 },
                    { 70, 7.9900000000000002, 62, 1 },
                    { 71, 7.9900000000000002, 62, 1 },
                    { 88, 7.9900000000000002, 74, 1 },
                    { 120, 7.9900000000000002, 106, 1 },
                    { 119, 7.9900000000000002, 105, 1 },
                    { 118, 7.9900000000000002, 105, 1 },
                    { 117, 7.9900000000000002, 104, 1 },
                    { 116, 7.9900000000000002, 104, 1 },
                    { 115, 7.9900000000000002, 104, 1 },
                    { 114, 7.9900000000000002, 104, 1 },
                    { 113, 7.9900000000000002, 104, 1 },
                    { 112, 7.9900000000000002, 103, 1 },
                    { 111, 7.9900000000000002, 103, 1 },
                    { 110, 7.9900000000000002, 103, 1 }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Price", "ScreeningId", "UserId" },
                values: new object[,]
                {
                    { 109, 7.9900000000000002, 103, 1 },
                    { 108, 7.9900000000000002, 102, 1 },
                    { 107, 7.9900000000000002, 102, 1 },
                    { 106, 7.9900000000000002, 102, 1 },
                    { 105, 7.9900000000000002, 102, 1 },
                    { 104, 7.9900000000000002, 102, 1 },
                    { 103, 7.9900000000000002, 102, 1 },
                    { 102, 7.9900000000000002, 102, 1 },
                    { 96, 7.9900000000000002, 74, 1 },
                    { 95, 7.9900000000000002, 74, 1 },
                    { 94, 7.9900000000000002, 74, 1 },
                    { 93, 7.9900000000000002, 74, 1 },
                    { 92, 7.9900000000000002, 74, 1 },
                    { 91, 7.9900000000000002, 74, 1 },
                    { 90, 7.9900000000000002, 74, 1 },
                    { 89, 7.9900000000000002, 74, 1 },
                    { 63, 7.9900000000000002, 31, 1 },
                    { 62, 7.9900000000000002, 31, 1 },
                    { 61, 7.9900000000000002, 31, 1 },
                    { 60, 7.9900000000000002, 31, 1 },
                    { 28, 7.9900000000000002, 3, 1 },
                    { 27, 7.9900000000000002, 3, 1 },
                    { 26, 7.9900000000000002, 3, 1 },
                    { 25, 7.9900000000000002, 3, 1 },
                    { 24, 7.9900000000000002, 3, 1 },
                    { 23, 7.9900000000000002, 1, 1 },
                    { 22, 7.9900000000000002, 1, 1 },
                    { 21, 7.9900000000000002, 1, 1 },
                    { 20, 7.9900000000000002, 1, 1 },
                    { 19, 7.9900000000000002, 1, 1 },
                    { 18, 7.9900000000000002, 1, 1 },
                    { 17, 7.9900000000000002, 1, 1 },
                    { 16, 7.9900000000000002, 1, 1 },
                    { 15, 7.9900000000000002, 1, 1 },
                    { 14, 7.9900000000000002, 1, 1 },
                    { 13, 7.9900000000000002, 1, 1 },
                    { 12, 7.9900000000000002, 1, 1 },
                    { 11, 7.9900000000000002, 1, 1 },
                    { 10, 7.9900000000000002, 1, 1 },
                    { 9, 7.9900000000000002, 1, 1 },
                    { 8, 7.9900000000000002, 1, 1 },
                    { 7, 7.9900000000000002, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Price", "ScreeningId", "UserId" },
                values: new object[,]
                {
                    { 6, 7.9900000000000002, 1, 1 },
                    { 5, 7.9900000000000002, 1, 1 },
                    { 4, 7.9900000000000002, 1, 1 },
                    { 3, 7.9900000000000002, 1, 1 },
                    { 2, 7.9900000000000002, 1, 1 },
                    { 29, 7.9900000000000002, 3, 1 },
                    { 121, 7.9900000000000002, 106, 1 },
                    { 30, 7.9900000000000002, 3, 1 },
                    { 32, 7.9900000000000002, 3, 1 },
                    { 59, 7.9900000000000002, 31, 1 },
                    { 58, 7.9900000000000002, 31, 1 },
                    { 57, 7.9900000000000002, 31, 1 },
                    { 101, 7.9900000000000002, 22, 1 },
                    { 87, 7.9900000000000002, 36, 1 },
                    { 86, 7.9900000000000002, 36, 1 },
                    { 85, 7.9900000000000002, 36, 1 },
                    { 84, 7.9900000000000002, 36, 1 },
                    { 83, 7.9900000000000002, 36, 1 },
                    { 82, 7.9900000000000002, 36, 1 },
                    { 81, 7.9900000000000002, 36, 1 },
                    { 80, 7.9900000000000002, 36, 1 },
                    { 79, 7.9900000000000002, 36, 1 },
                    { 78, 7.9900000000000002, 36, 1 },
                    { 45, 7.9900000000000002, 13, 1 },
                    { 44, 7.9900000000000002, 13, 1 },
                    { 43, 7.9900000000000002, 13, 1 },
                    { 42, 7.9900000000000002, 13, 1 },
                    { 41, 7.9900000000000002, 13, 1 },
                    { 40, 7.9900000000000002, 13, 1 },
                    { 39, 7.9900000000000002, 13, 1 },
                    { 38, 7.9900000000000002, 13, 1 },
                    { 37, 7.9900000000000002, 13, 1 },
                    { 36, 7.9900000000000002, 3, 1 },
                    { 35, 7.9900000000000002, 3, 1 },
                    { 34, 7.9900000000000002, 3, 1 },
                    { 33, 7.9900000000000002, 3, 1 },
                    { 31, 7.9900000000000002, 3, 1 },
                    { 122, 7.9900000000000002, 107, 1 }
                });

            migrationBuilder.InsertData(
               table: "CastMemberMedia",
               columns: new[] { "CastId", "StarredMediaId" },
               values: new object[,]
               {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 2 },
                    { 4, 3 },
                    { 5, 3 },
                    { 6, 3 },
                    { 7, 3 },
                    { 8, 4 },
                    { 9, 4 },
                    { 10, 4 },
                    { 11, 4 },
                    { 1, 5 },
                    { 2, 5 },
                    { 3, 5 },
                    { 1, 6 },
                    { 2, 6 },
                    { 12, 6 },
                    { 1, 7 },
                    { 2, 7 },
                    { 12, 7 },
                    { 1, 8 },
                    { 12, 8 },
                    { 13, 8 },
                    { 14, 9 },
                    { 15, 9 },
                    { 16, 9 },
                    { 17, 10 },
                    { 14, 10 },
                    { 15, 10 },
                    { 17, 11 },
                    { 14, 11 },
                    { 15, 11 },
                    { 58, 12 },
                    { 59, 12 },
                    { 60, 12 },
                    { 18, 13 },
                    { 19, 13 },
                    { 20, 13 },
                    { 21, 13 },
                    { 22, 14 },
                    { 23, 14 },
                    { 24, 14 },
                    { 25, 14 },
                    { 26, 15 },
                    { 27, 15 },
                    { 28, 15 },
                    { 29, 15 },
                    { 30, 16 },
                    { 31, 16 },
                    { 32, 16 },
                    { 33, 16 },
                    { 34, 17 },
                    { 35, 17 },
                    { 36, 17 },
                    { 34, 18 },
                    { 35, 18 },
                    { 36, 18 },
                    { 34, 19 },
                    { 35, 19 },
                    { 36, 19 },
                    { 37, 20 },
                    { 38, 20 },
                    { 39, 20 },
                    { 40, 21 },
                    { 41, 21 },
                    { 42, 21 },
                    { 43, 22 },
                    { 44, 22 },
                    { 45, 22 },
                    { 46, 23 },
                    { 47, 23 },
                    { 48, 23 },
                    { 49, 24 },
                    { 50, 24 },
                    { 51, 24 },
                    { 52, 25 },
                    { 53, 25 },
                    { 54, 25 },
                    { 55, 26 },
                    { 56, 26 },
                    { 57, 26 },

                    { 1, 27 },
                    { 2, 27 },
                    { 3, 27 },
                    { 1, 28 },
                    { 2, 28 },
                    { 3, 28 },
                    { 1, 29 },
                    { 2, 29 },
                    { 3, 29 },
                    { 1, 30 },
                    { 2, 30 },
                    { 3, 30 },
                    { 1, 31 },
                    { 2, 31 },
                    { 3, 31 },
                    { 1, 32 },
                    { 2, 32 },
                    { 3, 32 },
                    { 1, 33 },
                    { 2, 33 },
                    { 3, 33 },
                    { 1, 34 },
                    { 2, 34 },
                    { 3, 34 },
                    { 1, 35 },
                    { 2, 35 },
                    { 3, 35 },
                    { 1, 36 },
                    { 2, 36 },
                    { 3, 36 },
                    { 1, 37 },
                    { 2, 37 },
                    { 3, 37 },
                    { 1, 38 },
                    { 2, 38 },
                    { 3, 38 },
                    { 1, 39 },
                    { 2, 39 },
                    { 3, 39 },
                    { 1, 40 },
                    { 2, 40 },
                    { 3, 40 },
                    { 1, 41 },
                    { 2, 41 },
                    { 3, 41 },
                    { 1, 42 },
                    { 2, 42 },
                    { 3, 42 },
                    { 1, 43 },
                    { 2, 43 },
                    { 3, 43 },
                    { 1, 44 },
                    { 2, 44 },
                    { 3, 44 },
                    { 1, 45 },
                    { 2, 45 },
                    { 3, 45 },
                    { 1, 46 },
                    { 2, 46 },
                    { 3, 46 },
                    { 1, 47 },
                    { 2, 47 },
                    { 3, 47 },
                    { 1, 48 },
                    { 2, 48 },
                    { 3, 48 },
                    { 1, 49 },
                    { 2, 49 },
                    { 3, 49 },
                    { 1, 50 },
                    { 2, 50 },
                    { 3, 50 },
                    { 1, 51 },
                    { 2, 51 },
                    { 3, 51 },
                    { 1, 52 },
                    { 2, 52 },
                    { 3, 52 },

                    { 1, 53 },
                    { 2, 53 },
                    { 3, 53 },
                    { 1, 54 },
                    { 2, 54 },
                    { 3, 54 },
                    { 1, 55 },
                    { 2, 55 },
                    { 3, 55 },
                    { 1, 56 },
                    { 2, 56 },
                    { 3, 56 },
                    { 1, 57 },
                    { 2, 57 },
                    { 3, 57 },
                    { 1, 58 },
                    { 2, 58 },
                    { 3, 58 },
                    { 1, 59 },
                    { 2, 59 },
                    { 3, 59 },
                    { 1, 60 },
                    { 2, 60 },
                    { 3, 60 },
                    { 1, 61 },
                    { 2, 61 },
                    { 3, 61 },
                    { 1, 62 },
                    { 2, 62 },
                    { 3, 62 },
                    { 1, 63 },
                    { 2, 63 },
                    { 3, 63 },
                    { 1, 64 },
                    { 2, 64 },
                    { 3, 64 },
                    { 1, 65 },
                    { 2, 65 },
                    { 3, 65 },
                    { 1, 66 },
                    { 2, 66 },
                    { 3, 66 },
                    { 1, 67 },
                    { 2, 67 },
                    { 3, 67 },
                    { 1, 68 },
                    { 2, 68 },
                    { 3, 68 },
                    { 1, 69 },
                    { 2, 69 },
                    { 3, 69 },
                    { 1, 70 },
                    { 2, 70 },
                    { 3, 70 },
                    { 1, 71 },
                    { 2, 71 },
                    { 3, 71 },
                    { 1, 72 },
                    { 2, 72 },
                    { 3, 72 },
                    { 1, 73 },
                    { 2, 73 },
                    { 3, 73 },
                    { 1, 74 },
                    { 2, 74 },
                    { 3, 74 },
                    { 1, 75 },
                    { 2, 75 },
                    { 3, 75 },
                    { 1, 76 },
                    { 2, 76 },
                    { 3, 76 },
                    { 1, 77 },
                    { 2, 77 },
                    { 3, 77 },
                    { 1, 78 },
                    { 2, 78 },
                    { 3, 78 },

                    { 1, 79 },
                    { 2, 79 },
                    { 1, 80 },
                    { 2, 80 },
                    { 1, 81 },
                    { 2, 81 },
                    { 1, 82 },
                    { 2, 82 },
                    { 1, 83 },
                    { 2, 83 },
                    { 1, 84 },
                    { 2, 84 },
                    { 1, 85 },
                    { 2, 85 },
                    { 1, 86 },
                    { 2, 86 },
                    { 1, 87 },
                    { 2, 87 },
                    { 1, 88 },
                    { 2, 88 },
                    { 1, 89 },
                    { 2, 89 },
                    { 1, 90 },
                    { 2, 90 },
                    { 1, 91 },
                    { 2, 91 },
                    { 1, 92 },
                    { 2, 92 },
                    { 1, 93 },
                    { 2, 93 },
                    { 1, 94 },
                    { 2, 94 },
                    { 1, 95 },
                    { 2, 95 },
                    { 1, 96 },
                    { 2, 96 },
                    { 1, 97 },
                    { 2, 97 },
                    { 1, 98 },
                    { 2, 98 },
                    { 1, 99 },
                    { 2, 99 },
                    { 1, 100 },
                    { 2, 100 },
                    { 1, 101 },
                    { 2, 101 },
                    { 1, 102 },
                    { 2, 102 },
                    { 1, 103 },
                    { 2, 103 },
                    { 1, 104 },
                    { 2, 104 },
                    { 1, 105 },
                    { 2, 105 },
                    { 1, 106 },
                    { 2, 106 },
                    { 1, 107 },
                    { 2, 107 }
               });

            migrationBuilder.CreateIndex(
                name: "IX_CastMemberMedia_StarredMediaId",
                table: "CastMemberMedia",
                column: "StarredMediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RatedMediaId",
                table: "Ratings",
                column: "RatedMediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Screenings_MediaId",
                table: "Screenings",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ScreeningId",
                table: "Tickets",
                column: "ScreeningId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");

            string top10MoviesByRating = @"CREATE OR ALTER PROCEDURE Top10MoviesByRating
                                            AS
                                            SELECT TOP 10 m.id AS movieID, m.title AS movieTitle, COUNT(r.id) AS ratingsCount, m.rating AS movieRating
                                            FROM Medias m
                                            INNER JOIN Ratings r ON m.Id = r.RatedMediaId
                                            GROUP BY m.Id, m.title, m.rating
                                            ORDER BY m.Rating DESC;";

            string top10MoviesByScreeningsForPeriod = @"CREATE OR ALTER PROCEDURE Top10MoviesByScreeningsForPeriod 
                                                        @startDate datetime, @endDate datetime
                                                        AS 
                                                        SELECT TOP 10 m.Id AS movieID, m.Title AS movieTitle, COUNT(s.id) AS screeningsCount
                                                        FROM Medias m
                                                        INNER JOIN Screenings s ON m.Id = s.MediaId
                                                        WHERE s.StartTime BETWEEN @startDate AND @endDate
                                                        GROUP BY m.Id, m.Title
                                                        ORDER BY COUNT(s.id) DESC;";

            string mostSoldMoviesWithoutRatings = @"CREATE OR ALTER PROCEDURE MostSoldMoviesWithoutRatings
                                                    AS
                                                    SELECT m.Id AS movieID, m.Title AS movieTitle, s.Name AS screeningName, COUNT(t.id) AS ticketsSold
                                                    FROM Screenings s
                                                    INNER JOIN Tickets t ON s.Id = t.ScreeningId
                                                    INNER JOIN Medias m ON m.Id = s.MediaId
                                                    WHERE (SELECT COUNT(r.id) FROM Ratings r WHERE r.RatedMediaId = m.Id) = 0
                                                    GROUP BY s.Name, m.Id, m.Title
                                                    ORDER BY COUNT(t.id) DESC;";

            migrationBuilder.Sql(top10MoviesByRating);
            migrationBuilder.Sql(top10MoviesByScreeningsForPeriod);
            migrationBuilder.Sql(mostSoldMoviesWithoutRatings);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string top10MoviesByRating = @"DROP PROCEDURE top10MoviesByRating";
            string top10MoviesByScreeningsForPeriod = @"DROP PROCEDURE top10MoviesByScreeningsForPeriod";
            string mostSoldMoviesWithoutRatings = @"DROP PROCEDURE mostSoldMoviesWithoutRatings";

            migrationBuilder.Sql(top10MoviesByRating);
            migrationBuilder.Sql(top10MoviesByScreeningsForPeriod);
            migrationBuilder.Sql(mostSoldMoviesWithoutRatings);

            migrationBuilder.DropTable(
                name: "CastMemberMedia");

            migrationBuilder.DropTable(
                name: "MostSoldMoviesWithoutRatingsResults");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Top10MoviesByRatingResults");

            migrationBuilder.DropTable(
                name: "Top10MoviesByScreeningsForPeriodResults");

            migrationBuilder.DropTable(
                name: "CastMembers");

            migrationBuilder.DropTable(
                name: "Screenings");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Medias");
        }
    }
}
