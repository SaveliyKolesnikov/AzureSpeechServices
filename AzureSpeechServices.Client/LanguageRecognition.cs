using AzureSpeechServices.Shared;
using Microsoft.CognitiveServices.Speech;

namespace AzureSpeechServices.Client;

public class LanguageRecognition
{
    private static AutoDetectSourceLanguageConfig autoDetectSourceLanguageConfig = AutoDetectSourceLanguageConfig.FromLanguages(new string[] { "en-US", "uk-UA", "ru-RU", "fr-FR" });
    
    public static async Task DetectionWithMicrophoneAsync(SpeechServiceSettings settings)
    {
        var config = SpeechConfig.FromSubscription(settings.SubscriptionKey, settings.ServiceRegion);
        
        config.SetProperty(PropertyId.SpeechServiceConnection_SingleLanguageIdPriority, "Latency");
        
        using (var recognizer = new SourceLanguageRecognizer(config, autoDetectSourceLanguageConfig))
        {
            Console.WriteLine("Say something...");
            
            var result = await recognizer.RecognizeOnceAsync().ConfigureAwait(false);

            // Checks result.
            if (result.Reason == ResultReason.RecognizedSpeech)
            {
                var lidResult = AutoDetectSourceLanguageResult.FromResult(result);
                Console.WriteLine($"DETECTED: Language={lidResult.Language}");
            }
            else if (result.Reason == ResultReason.NoMatch)
            {

                Console.WriteLine($"NOMATCH: Speech could not be recognized.");
            }
            else if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = CancellationDetails.FromResult(result);
                Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                if (cancellation.Reason == CancellationReason.Error)
                {
                    Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                    Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                    Console.WriteLine($"CANCELED: Did you update the subscription info?");
                }
            }
        }
    }
}