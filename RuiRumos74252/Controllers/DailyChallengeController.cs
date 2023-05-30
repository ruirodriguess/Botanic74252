using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using RuiRumos74252.Models;
using System.Reflection.Metadata;

namespace RuiRumos74252.Controllers
{
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
                "catos.jpg",
                "malmequer.jpg",
                "rosa.jpg",
                "tulipas.jpg"
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

            _currentImageName = randomPhoto; // Armazena o nome da imagem atual na variável

            DailyChallenge challenge = new DailyChallenge
            {
                Id = 1,
                ImageUrl = imageUrl,
                CorrectAnswer = _currentImageName
            };

            return View(challenge);
        }

        [HttpPost]
        public IActionResult SubmitAnswer(int challengeId, string answer)
        {
            // Retrieve the current image name from the session

            DailyChallenge challenge = new DailyChallenge
            {
                Id = challengeId,
                ImageUrl = "", // Set the image URL if needed
                CorrectAnswer = _currentImageName // Use the provided currentImageName as the correct answer
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

