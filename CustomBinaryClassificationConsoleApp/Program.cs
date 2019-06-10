using System;
using System.IO;
using System.Linq;
using Microsoft.ML;
using SampleBinaryClassification.Model.DataModels;

namespace SampleBinaryClassification.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MLContext mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(GetAbsolutePath("MLModel.zip"), out DataViewSchema inputSchema);            
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            Console.WriteLine("=============== WELCOME TO HOSTILITY CHECKER ===============");
            
            while (true)
            {
                Console.WriteLine("Please enter a sentence to check, or 'q' to exit:");
                string userInput = Console.ReadLine();
                if (String.Equals(userInput, "q")) 
                {
                    return;
                }

                ModelInput inputData = new ModelInput();
                inputData.Comment = userInput;
                ModelOutput predictionResult = predEngine.Predict(inputData);
                string predictedLabel = predictionResult.Prediction?"hostile":"friendly";
                Console.WriteLine($"Input sentence: '{inputData.Comment}' is {predictedLabel} (score: {predictionResult.Score})");
                Console.WriteLine($"--------------------------------------------");
            }
        }

        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }
    }
}
