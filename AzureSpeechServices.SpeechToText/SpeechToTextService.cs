using AzureSpeechServices.Shared;
using Microsoft.CognitiveServices.Speech;

namespace AzureSpeechServices.SpeechToText
{
    public class SpeechToTextService
    {
        // Speech recognition from microphone.
        public static async Task<string> RecognitionWithMicrophoneAsync(SpeechServiceSettings settings)
        {
            var config = SpeechConfig.FromSubscription(settings.SubscriptionKey, settings.ServiceRegion);

            using var recognizer = new SpeechRecognizer(config);
            
            Console.WriteLine("Say something...");
                
            var result = await recognizer.RecognizeOnceAsync().ConfigureAwait(false);

            if (result.Reason == ResultReason.RecognizedSpeech)
            {
                Console.WriteLine($"RECOGNIZED: Text={result.Text}");
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

            return result.Text;
        }
    }
}