using AzureSpeechServices.Shared;
using Microsoft.CognitiveServices.Speech;

namespace AzureSpeechServices.Client;

public class TextToSpeech
{
    public static async Task SynthesisToSpeakerAsync(string text, SpeechServiceSettings settings)
    {
        var config = SpeechConfig.FromSubscription(settings.SubscriptionKey, settings.ServiceRegion);

        using var synthesizer = new SpeechSynthesizer(config);
        using var result = await synthesizer.SpeakTextAsync(text);

        if (result.Reason == ResultReason.SynthesizingAudioCompleted)
        {
            Console.WriteLine($"Speech synthesized to speaker for text [{text}]");
        }
        else if (result.Reason == ResultReason.Canceled)
        {
            var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
            Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

            if (cancellation.Reason == CancellationReason.Error)
            {
                Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                Console.WriteLine($"CANCELED: Did you update the subscription info?");
            }
        }
    }
}