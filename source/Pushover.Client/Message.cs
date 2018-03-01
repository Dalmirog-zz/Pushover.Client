using System.ComponentModel.DataAnnotations;
using Pushover.Client.Exceptions;

namespace Pushover.Client
{
    public class PushoverMessage
    {
        public PushoverMessage(string title = "", string message = "", string url = "", string url_title = "")
        {
            Title = title.Length <= PropertyMaxLength.Title 
                ? title 
                : throw new PropertyLengthTooLongException("Title",title.Length,PropertyMaxLength.Title);

            Message = message.Length <= PropertyMaxLength.Message
                ? title
                : throw new PropertyLengthTooLongException("Message", message.Length, PropertyMaxLength.Message);

            URL = url.Length <= PropertyMaxLength.URL
                ? title
                : throw new PropertyLengthTooLongException("URL", url.Length, PropertyMaxLength.URL);

            URL_Title = url_title.Length <= PropertyMaxLength.URL_Title
                ? title
                : throw new PropertyLengthTooLongException("URL_Title", url_title.Length, PropertyMaxLength.URL_Title);
        }
        
        public string Title { get;}

        public string Message { get;}

        public string URL { get; }

        public string URL_Title { get; }

        public string Device { get; set;}

        public MessagePriority Priority { get; set; }

        public string TimeStamp { get; set; }

        public MessageSound Sound { get; set; }
    }

    public static class PropertyMaxLength
    {
        public static int Title => 250;
        public static int Message => 1024;
        public static int URL => 512;
        public static int URL_Title => 100;

    }
}

