// See https://aka.ms/new-console-template for more information

using AzureSpeechServices.Client;
using AzureSpeechServices.Shared;
using AzureSpeechServices.SpeechToText;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");
var c = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

var settings = c.GetSection("SpeechServiceSettings").Get<SpeechServiceSettings>();

Console.WriteLine("Speech to text demo: ");
Console.WriteLine("Press enter to start: ");
Console.ReadLine();
var recognizedText = await SpeechToTextService.RecognitionWithMicrophoneAsync(settings);

Console.WriteLine("Press enter to try text to speech sample: ");
Console.ReadLine();
await TextToSpeech.SynthesisToSpeakerAsync(recognizedText, settings);

Console.WriteLine("Press enter to try language recognition sample: ");
Console.ReadLine();
string? input;
do
{
    await LanguageRecognition.DetectionWithMicrophoneAsync(settings);
    Console.WriteLine("Press f to _pay respects_ finish");
    input = Console.ReadLine();
} while (input != "f");

Console.WriteLine("Press enter to quit");
Console.ReadLine();