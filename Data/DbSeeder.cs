using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TestMakerFreeApi.Data.Models;

namespace TestMakerFreeApi.Data
{
    public static class DbSeeder
    {
        #region Public Methods

        public static void Seed(ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager
        )
        {
            if (!context.Users.Any())
                CreateUsers(context, roleManager, userManager)
                    .GetAwaiter()
                    .GetResult();

            if (!context.Quizzes.Any()) CreateQuizzes(context);
        }

        #endregion

        #region Seed Methods

        private static void CreateQuizzes(ApplicationDbContext context)
        {
            var createdDate = new DateTime(2016, 03, 01, 12, 30, 00);
            var lastModifiedDate = DateTime.Now;
            var authorId = context.Users.FirstOrDefault(u => u.UserName == "Admin")?.Id;

#if DEBUG
            const int num = 47;
            for (var i = 1; i <= num; i++)
            {
                CreateSampleQuiz(context, i, authorId, num - 1, 3, 3, 3, createdDate.AddDays(-num));
            }
#endif

            context.Quizzes.Add(new Quiz()
            {
                UserId = authorId,
                Title = "Are you more Light or Dark side of the Force?",
                Description = "Star Wars personality test",
                Text = @"Choose wisely you must, young padawan: " +
                       "this test will prove if your will is strong enough " +
                       "to adhere to the principles of the light side of the Force " +
                       "or if you're fated to embrace the dark side. " +
                       "No you want to become a true JEDI, you can't possibly miss this!",
                ViewCount = 2343,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });

            context.Quizzes.Add(new Quiz()
            {
                UserId = authorId,
                Title = "GenX, GenY or Genz?",
                Description = "Find out what decade most represents you",
                Text = @"Do you feel confortable in your generation? " +
                       "What year should you have been born in?" +
                       "Here's a bunch of questions that will help you to find out!",
                ViewCount = 4180,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });

            context.Quizzes.Add(new Quiz()
            {
                UserId = authorId,
                Title = "Which Shingeki No Kyojin character are you?",
                Description = "Attack On Titan personality test",
                Text = @"Do you relentlessly seek revenge like Eren? " +
                       "Are you willing to put your like on the stake to protect your friends like Mikasa? " +
                       "Would you trust your fighting skills like Levi " +
                       "or rely on your strategies and tactics like Arwin? " +
                       "Unveil your true self with this Attack On Titan personality test!",
                ViewCount = 5203,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });

            context.SaveChanges();
        }

        #endregion

        private static async Task CreateUsers(ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            var createdDate = new DateTime(2016, 03, 01, 12, 20, 00);
            var lastModifiedDate = DateTime.Now;
            const string roleAdministrator = "Administrator";
            const string roleRegisteredUser = "RegisteredUser";

            if (!await roleManager.RoleExistsAsync(roleAdministrator))
            {
                await roleManager.CreateAsync(new IdentityRole(roleAdministrator));
            }

            if (!await roleManager.RoleExistsAsync(roleRegisteredUser))
            {
                await roleManager.CreateAsync(new IdentityRole(roleRegisteredUser));
            }

            var userAdmin = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "Admin",
                Email = "admin@testmakerfree.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };

            if (await userManager.FindByNameAsync(userAdmin.UserName) == null)
            {
                await userManager.CreateAsync(userAdmin, "Pass4Admin");
                await userManager.AddToRoleAsync(userAdmin, roleRegisteredUser);
                await userManager.AddToRoleAsync(userAdmin, roleAdministrator);

                userAdmin.EmailConfirmed = true;
                userAdmin.LockoutEnabled = false;
            }

#if DEBUG
            var userRyan = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "Ryan",
                Email = "ryan@testmakerfree.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };
            var userSolice = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "Solice",
                Email = "solice@testmakerfree.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };
            var userVodan = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "Vodan",
                Email = "vodan@testmakerfree.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };

            if (await userManager.FindByNameAsync(userRyan.UserName) == null)
            {
                await userManager.CreateAsync(userRyan, "Pass4Ryan");
                await userManager.AddToRoleAsync(userRyan, roleRegisteredUser);
// Remove Lockout and E-Mail confirmation.
                userRyan.EmailConfirmed = true;
                userRyan.LockoutEnabled = false;
            }

            if (await userManager.FindByNameAsync(userSolice.UserName) == null)
            {
                await userManager.CreateAsync(userSolice, "Pass4Solice");
                await userManager.AddToRoleAsync(userSolice, roleRegisteredUser);
// Remove Lockout and E-Mail confirmation.
                userSolice.EmailConfirmed = true;
                userSolice.LockoutEnabled = false;
            }

            if (await userManager.FindByNameAsync(userVodan.UserName) == null)
            {
                await userManager.CreateAsync(userVodan, "Pass4Vodan");
                await userManager.AddToRoleAsync(userVodan, roleRegisteredUser);
// Remove Lockout and E-Mail confirmation.
                userVodan.EmailConfirmed = true;
                userVodan.LockoutEnabled = false;
            }

#endif
            await context.SaveChangesAsync();
        }

        #region Utility Methods

        private static void CreateSampleQuiz(
            ApplicationDbContext dbContext,
            int num,
            string authorId,
            int viewCount,
            int numberOfQuestions,
            int numberOfAnswersPerQuestion,
            int numberOfResults,
            DateTime createdDate)
        {
            var quiz = new Quiz()
            {
                UserId = authorId,
                Title = string.Format("Quiz {0} Title", num),
                Description = string.Format("This is a sample description for quiz {0}.", num),
                Text = "This is a sample quiz created by the DbSeeder class for testing purposes. " +
                       "All the questions, answers & results are auto-generated as well.",
                ViewCount = viewCount,
                CreatedDate = createdDate,
                LastModifiedDate = createdDate
            };

            dbContext.Quizzes.Add(quiz);
            dbContext.SaveChanges();

            for (var i = 0; i < numberOfQuestions; i++)
            {
                var question = new Question()
                {
                    QuizId = quiz.Id,
                    Text = "This is a sample question created by the DbSeeder class for testing purposes." +
                           "All the child answers are auto-generated as well.",
                    CreatedDate = createdDate,
                    LastModifiedDate = createdDate
                };

                dbContext.Questions.Add(question);
                dbContext.SaveChanges();

                for (var i2 = 0; i2 < numberOfAnswersPerQuestion; i2++)
                {
                    dbContext.Answers.Add(new Answer()
                    {
                        QuestionId = question.Id,
                        Text = "This is a sample answer created by the DbSeeder class for testing purposes.",
                        Value = i2,
                        CreatedDate = createdDate,
                        LastModifiedDate = createdDate
                    });
                }
            }

            for (var i = 0; i < numberOfResults; i++)
            {
                dbContext.Results.Add(new Result()
                {
                    QuizId = quiz.Id,
                    Text = "This is a sample result created by the DbSeeder class for testing purposes.",
                    MinValue = 0,
                    MaxValue = numberOfAnswersPerQuestion * 2,
                    CreatedDate = createdDate,
                    LastModifiedDate = createdDate
                });
            }

            dbContext.SaveChanges();
        }

        #endregion
    }
}