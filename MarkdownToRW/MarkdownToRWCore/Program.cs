﻿using System;
using PowerArgs;

namespace MarkdownToRWCore
{
    // PowerArgs usage:
    // https://github.com/adamabdelhamed/PowerArgs

    // A class that describes the command line arguments for this program
    [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
    [TabCompletion]
    public class ConvertAndUploadArguments
    {
        [HelpHook]
        [ArgShortcut("-?")]
        [ArgShortcut("-help")]
        [ArgDescription("Shows this help")]
        public bool ShowHelp { get; set; }

        [ArgRequired(PromptIfMissing = true)]
        [ArgShortcut("i")]
        [ArgShortcut("input")]
        [ArgDescription("The path to the markdown file. (e.g. 'C:\\Users\\Me\\Desktop\\file.md')")]
        [PromptIfEmpty]
        [ArgPosition(0)]
        public string MarkdownPath { get; set; }

        [ArgRequired(PromptIfMissing = false)]
        [ArgDefaultValue(true)]
        [ArgShortcut("sameDir")]
        [ArgDescription("Put the html file in the same directory as the markdown file? (true/false)")]
        public bool SameFolderOutput { get; set; }

        [ArgShortcut("htmlFolder")]
        [ArgShortcut("o")]
        [ArgDescription("The path to the folder to save the html file. (e.g. 'C:\\Users\\Me\\Desktop\\')")]
        public string HtmlPath { get; set; }

        [ArgShortcut("upload")]
        [ArgDefaultValue("False")]
        [ArgDescription("Upload images to WordPress and replace links in files? (true/false)")]
        [ArgPosition(1)]
        public bool UploadImages { get; set; }

        //[ArgRequired(PromptIfMissing = true, If = "UploadImages")]
        [ArgDefaultValue(true)]
        [ArgShortcut("onlyHTML")]
        [ArgDescription(
            "After uploading the images, should only the html file be updated with new links? If false, the original markdown will be updated as well. (true/false)")]
        [ArgPosition(2)]
        public bool OnlyUpdateHtmlFile { get; set; }

        //[ArgRequired(PromptIfMissing = true, If = "UploadImages")]
        [ArgShortcut("user")]
        [ArgDescription("RW WordPress Username. Needs to be suuplied when uploading images.")]
        [ArgPosition(3)]
        public string Username { get; set; }

        //[ArgRequired(PromptIfMissing = true, If = "UploadImages")]
        [ArgShortcut("pw")]
        [ArgDescription("RW WordPress Password. Needs to be suuplied when uploading images.")]
        [ArgPosition(4)]
        public string Password { get; set; }

        // This non-static Main method will be called and it will be able to access the parsed and populated instance level properties.
        public void Main()
        {
            Console.WriteLine(
                "Markdown File Path: '{0}'\nSame Folder: '{1}'\nUpload Images: '{2}'\nHtml Ouput Folder: '{3}'\nOnly Update HTML: '{4}'",
                MarkdownPath, SameFolderOutput, UploadImages, HtmlPath, OnlyUpdateHtmlFile);

            Console.WriteLine((Username == null) + " " + (Password == null));

            if (UploadImages && (Username == null || Password == null))
            {
                Console.WriteLine(
                    "The upload flag was set to True, but the username and/or password wasn't provided (-user & -pw). Aborting.");
            }
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            CoreConsoleShared.WriteIntro();

            if (args.Length == 0)
            {
                InteractiveConsole.StartInteractive();
            }
            else
            {
                try
                {
                    Args.InvokeMain<ConvertAndUploadArguments>(args);
                }
                catch (ArgException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("");
                    Console.WriteLine("Enter -? for help on the proper usage.");
                }
            }
        }

    }

}