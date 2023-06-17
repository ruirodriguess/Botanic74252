using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using RuiRumos74252.Models;
using System.Reflection.Metadata;

namespace RuiRumos74252.Controllers
{
    [Authorize]
    public class DailyChallengeController : Controller
    {
        private readonly CloudBlobContainer _blobContainer;
        private readonly List<string> _availablePhotos;

        public DailyChallengeController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("StorageConnectionString");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            _blobContainer = blobClient.GetContainerReference("botanic74252");

            _availablePhotos = new List<string>
            {
                "cato",
                "malmequer",
                "rosa",
                "tulipa",
                "palmeira"
            };
        }

        private string GetRandomPhoto()
        {
            Random random = new Random();
            int randomIndex = random.Next(_availablePhotos.Count);
            string randomPhoto = _availablePhotos[randomIndex];
            return randomPhoto;
        }

        private string _currentImageName;

        public IActionResult ShowDailyChallenge()
        {
            string randomPhoto = GetRandomPhoto();

            // Retrieve the daily challenge image URL from Blob Storage
            CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(randomPhoto);
            string imageUrl = blob.Uri.ToString();

            HttpContext.Session.SetString("CurrentImageName", randomPhoto);

            //_currentImageName = randomPhoto; 

            DailyChallenge challenge = new DailyChallenge
            {
                Id = 1,
                ImageUrl = imageUrl
            };

            return View(challenge);
        }

        [HttpPost]
        public IActionResult SubmitAnswer(int challengeId, string answer)
        {

            string currentImageName = HttpContext.Session.GetString("CurrentImageName");

            // Retrieve the current image name from the session

            DailyChallenge challenge = new DailyChallenge
            {
                Id = challengeId,
                ImageUrl = "", // Passar se necessário
                CorrectAnswer = currentImageName // Use the provided currentImageName as the correct answer
            };

            if (string.Equals(answer, challenge.CorrectAnswer, StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.ResultMessage = "Congratulations! You guessed it correctly!";
            }
            else
            {
                ViewBag.ResultMessage = "Sorry, your answer is incorrect. Please try again.";
            }

            return View("AnswerResult");
        }
    }
}

